using Renci.SshNet;
using System.Diagnostics;

namespace BeocreateRemote.Core
{
    public class SshController : IRemoteController

    {
        private readonly SshClient _sshClient;

        public SshController(string address, string user, string password)
        {
            _sshClient = new SshClient(address, user, password);
        }

        private void CheckConnection()
        {

            if (!_sshClient.IsConnected)
            {
                _sshClient.Connect();
            }
        }

        public void Mute()
        {
            CheckConnection();
            var result = _sshClient.RunCommand("dsptoolkit mute");
            Debug.WriteLine(result);
        }
        public void Unmute()
        {
            CheckConnection();
            var result = _sshClient.RunCommand("dsptoolkit unmute");
            Debug.WriteLine(result);
        }

        public int GetTemperature()
        {
            CheckConnection();
            var result = _sshClient.RunCommand("cat /sys/class/thermal/thermal_zone0/temp");
            return (int.Parse(result.Result) / 1000);
        }

        public double Volume
        {
            get
            {

                CheckConnection();
                var result = _sshClient.RunCommand("dsptoolkit get-volume");
                Debug.WriteLine("Ssh get " + result.Result);
                return ConvertVolume(result.Result);
            }
            set
            {

                CheckConnection();
                var result = _sshClient.RunCommand("dsptoolkit set-volume " + value.ToString().Replace(',', '.'));
                Debug.WriteLine("Ssh setVolume " + result.Result);
            }
        }

        public static double ConvertVolume(String volume)
        {
            var fragment = volume.Split(' ')[1].Replace('.', ',');
            Debug.WriteLine("fargment " + fragment);
            return double.Parse(fragment);

        }
    }
}
