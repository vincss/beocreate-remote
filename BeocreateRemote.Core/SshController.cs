using Renci.SshNet;
using System.Diagnostics;

namespace BeocreateRemote.Core
{
    public class SshController : IRemoteController

    {
        private readonly SshClient _sshClient;

        public SshController(String address, String user, String password)
        {
            _sshClient = new SshClient(address, user, password);
        }

        private void checkConnection()
        {

            if (!_sshClient.IsConnected)
            {
                _sshClient.Connect();
            }
        }

        public void mute()
        {
            checkConnection();
            var result = _sshClient.RunCommand("dsptoolkit mute");
            Debug.WriteLine(result);
        }
        public void unmute()
        {
            checkConnection();
            var result = _sshClient.RunCommand("dsptoolkit unmute");
            Debug.WriteLine(result);
        }

        public int getTemperature()
        {
            checkConnection();
            var result = _sshClient.RunCommand("cat /sys/class/thermal/thermal_zone0/temp");
            return (int.Parse(result.Result) / 1000);
        }
    }
}
