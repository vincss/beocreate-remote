using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BeocreateRemote.Core
{
    public class SigmaTcpController : IRemoteController
    {
        const int DefaultPort = 8086;

        const int HeaderSize = 14;
        const int DecimalLenght = 4;

        const string AttributeVolumeControl = "volumeControlRegister";
        const string AttributeMuteRegister = "muteRegister";

        const int CommandReadMemory = 0x0a;
        const int CommandWriteMemory = 0x09;
        const int CommandGetMetaData = 0xf8;
        const int CommandMetaDataResponse = 0xf9;

        private TcpClient tcpClient;
        private readonly NetworkStream stream;

        public SigmaTcpController(string address, int port = DefaultPort)
        {
            tcpClient = new TcpClient(address, port);

            stream = tcpClient.GetStream();
        }

        public double GetVolume()
        {
            SendCommandGetMetaData(AttributeVolumeControl);
            var address = GetMetaDataAddress(); // ToDo Caching ?
            var volume = ReadDecimal(address, DecimalLenght);

            return volume;
        }

        public void SetVolume(double value)
        {
            SendCommandGetMetaData(AttributeVolumeControl);
            var address = GetMetaDataAddress();

            var valueToSend = DecimalRepr((float)value);
            WriteDecimal(address, valueToSend);
        }

        private int GetMetaDataAddress()
        {
            var rcvData = new Byte[256];
            var readNbr = stream.Read(rcvData, 0, rcvData.Length);

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

            stream.Write(packet, 0, packet.Length);

        }

        private double ReadDecimal(int addressToRead, int decimalLength)
        {
            var data = new byte[HeaderSize];
            var length = (byte)decimalLength;
            byte addr = (byte)addressToRead;
            data[0] = CommandReadMemory;
            data[4] = HeaderSize;
            data[9] = (byte)(length & 0xff);
            data[8] = (byte)((length >> 8) & 0xff);
            data[11] = (byte)(addr & 0xff);
            data[10] = (byte)((addr >> 8) & 0xff);

            stream.Write(data, 0, HeaderSize);

            var rcvData = new byte[HeaderSize + decimalLength];
            stream.Read(rcvData, 0, rcvData.Length);
            var decimalToParse = rcvData[new Range(HeaderSize, rcvData.Length)];

            return DecimalVal(decimalToParse);
        }

        private void WriteDecimal(int addressToRead, int value)
        {
            byte[] data = (IntData(value));
            int length = data.Length;
            byte addr = (byte)addressToRead;
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

            stream.Write(packet);
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

        private static readonly float LSB_SIGMA = 1.0f / (float)Math.Pow(2, 23);

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
            f = f * (1 << 24);
            return (int)f;
        }

        public int Volume { get { return (int)(GetVolume() * 100); } set { SetVolume((double)value / 100); } }

        public bool IsConnected => throw new NotImplementedException();

        public int GetTemperature()
        {
            throw new NotImplementedException();
        }

        public void Mute()
        {
            throw new NotImplementedException();
        }

        public void Unmute()
        {
            throw new NotImplementedException();
        }
    }
}
