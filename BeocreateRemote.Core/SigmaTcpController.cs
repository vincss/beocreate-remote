﻿using System.Diagnostics;
using System.Net.Sockets;
using System.Text;


namespace BeocreateRemote.Core
{
    public class SigmaTcpController(string address, int port = SigmaTcpController.DefaultPort) : IRemoteController
    {
        const int DefaultPort = 8086;

        const int HeaderSize = 14;
        const int DecimalLenght = 4;

        const string AttributeVolumeControl = "volumeControlRegister";
        const string AttributeMuteRegister = "muteRegister";

        private int? volumeAddress;
        private int? muteAddress;

        const int CommandReadMemory = 0x0a;
        const int CommandWriteMemory = 0x09;
        const int CommandGetMetaData = 0xf8;
        const int CommandMetaDataResponse = 0xf9;

        private static readonly float LSB_SIGMA = 1.0f / (float)Math.Pow(2, 23);

        private int? volume;

        private readonly TcpClient tcpClient = new();

        public void Connect()
        {
            if (!tcpClient.ConnectAsync(address, port).Wait(1000))
            {
                throw new Exception("Failed to connect to " + address);
            };
        }

        private NetworkStream GetStream()
        {
            if (tcpClient.Connected) return tcpClient.GetStream();
            Connect();
            return tcpClient.GetStream();
        }

        public double GetVolume()
        {
            return ReadDecimal(GetVolumeAddress());
        }

        private int GetVolumeAddress()
        {
            if (!volumeAddress.HasValue)
            {
                SendCommandGetMetaData(AttributeVolumeControl);
                volumeAddress = ReceiveMetaDataAddress();
            }
            return volumeAddress.Value;
        }

        public void SetVolume(double value)
        {
            if (value < 0) { value = 0; } else if (value > 1) { value = 1; }
            WriteMemory(GetVolumeAddress(), (float)value);
        }

        public bool Mute
        {
            get {
                if (!muteAddress.HasValue)
                {
                    SendCommandGetMetaData(AttributeMuteRegister);
                    muteAddress = ReceiveMetaDataAddress();
                }
                int muted = ReadInt(muteAddress.Value);
                return muted == 1;
            }
            set {
                if (!muteAddress.HasValue)
                {
                    SendCommandGetMetaData(AttributeMuteRegister);
                    muteAddress = ReceiveMetaDataAddress();
                }
                WriteMemory(muteAddress.Value, value ? 1 : 0);

            }
        }

        public int Volume
        {
            get
            {
                if (!volume.HasValue)
                {
                    volume = (int)(GetVolume() * 100);
                }

                return volume.Value;
            }
            set
            {
                if (value < 0) { value = 0; } else if (value > 100) { value = 100; }
                volume = value;
                SetVolume((double)value / 100);
            }
        }

        public bool IsConnected
        {
            get
            {
                GetStream();
                return tcpClient.Connected;
            }
        }

        private int ReceiveMetaDataAddress()
        {
            var rcvData = new Byte[256];
            var readNbr = GetStream().Read(rcvData, 0, rcvData.Length);

            if (rcvData[0] != CommandMetaDataResponse)
            {
                Debug.WriteLine("WrongHeader");
            }

            var txt = Encoding.UTF8.GetString(rcvData, HeaderSize, readNbr);
            return int.Parse(txt);
        }

        private void SendCommandGetMetaData(string attribute)
        {
            var lenght = (byte)(HeaderSize + attribute.Length);

            byte[] header = new byte[HeaderSize];
            header[0] = CommandGetMetaData;
            header[3] = (byte)((lenght >> 8) & 0xff);
            header[4] = (byte)(lenght & 0xff);
            var attributeByte = Encoding.UTF8.GetBytes(attribute); ;
            byte[] packet = [.. header, .. attributeByte];

            GetStream().Write(packet, 0, packet.Length);
        }

        private double ReadDecimal(int addr, int decimalLength = DecimalLenght)
        {
            var bytesToParse = ReadMemory(addr, decimalLength);
            return DecimalVal(bytesToParse);
        }

        private int ReadInt(int addr, int decimalLength = DecimalLenght)
        {
            var bytesToParse = ReadMemory(addr, decimalLength);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytesToParse);

            var value = BitConverter.ToInt32(bytesToParse);
            return value;
        }

        private void WriteMemory(int addressToWrite, float value)
        {
            var valueToSend = DecimalRepr(value);
            WriteMemory(addressToWrite, valueToSend);
        }

        private byte[] ReadMemory(int addr, int decimalLength = DecimalLenght)
        {
            var data = new byte[HeaderSize];
            var length = (byte)decimalLength;
            data[0] = CommandReadMemory;
            data[4] = HeaderSize;
            data[9] = (byte)(length & 0xff);
            data[8] = (byte)((length >> 8) & 0xff);
            data[11] = (byte)(addr & 0xff);
            data[10] = (byte)((addr >> 8) & 0xff);

            GetStream().Write(data, 0, HeaderSize);

            var rcvData = new byte[HeaderSize + decimalLength];
            var read = GetStream().Read(rcvData, 0, rcvData.Length);
            var bytesToParse = rcvData[new Range(HeaderSize, read)];
            return bytesToParse;
        }

        private void WriteMemory(int addr, int value)
        {
            byte[] data = IntData(value);
            int length = data.Length;
            byte[] header = new byte[HeaderSize];
            header[0] = CommandWriteMemory;
            header[11] = (byte)(length & 0xff);
            header[10] = (byte)((length >> 8) & 0xff);
            header[13] = (byte)(addr & 0xff);
            header[12] = (byte)((addr >> 8) & 0xff);

            byte[] packet = [.. header, .. data];

            int packetLength = packet.Length;
            packet[6] = (byte)(packetLength & 0xff);
            packet[5] = (byte)((packetLength >> 8) & 0xff);

            GetStream().Write(packet);
        }

        public static byte[] IntData(int intval, int length = DecimalLenght)
        {
            byte[] octets = new byte[length];
            for (int i = length; i > 0; i--)
            {
                octets[length - i] = (byte)((intval >> (i - 1) * 8) & 0xff);
            }
            return octets;
        }

        public static double DecimalVal(object p)
        {
            /* Converts a 32-bit fixed point value used in SigmaDSP processors to a float value */

            if (p is byte[] byteArray)
            {
                int val = 0;
                foreach (byte octet in byteArray)
                {
                    val *= 256;
                    val += octet;
                }

                p = val;
            }

            float f = Convert.ToSingle((int)p) / (float)Math.Pow(2, 24);

            if (f >= 128)
            {
                f = -256 + f;
            }

            return Math.Round(f * 1000) / 1000;
        }

        public static int DecimalRepr(float f)
        {
            /*
            Converts a float to a 32-bit fixed point value used in
            ADAU154x SigmaDSP processors
            */

            if (f > 256 - LSB_SIGMA || f < -256)
            {
                throw new Exception($"Value {f} not in range [-16,16]");
            }

            // dual complement
            if (f < 0)
            {
                f = 256 + f;
            }

            // multiply by 2^24, then convert to integer
            f *= (1 << 24);
            return (int)f;
        }

    }
}
