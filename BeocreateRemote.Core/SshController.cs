using Renci.SshNet;

namespace BeocreateRemote.Core
{
    public class SshController : IRemoteController

    {
        private readonly SshClient _sshClient;

        public SshController(String address, String user, String password)
        {
            _sshClient = new SshClient(address, user, password);
        }

        private void checkConnection() { 
            
           if(!_sshClient.IsConnected) { 
                _sshClient.Connect();
            }
        }

        public void mute()
        {
            checkConnection();
            var result = _sshClient.RunCommand("dsptoolkit mute");
            Console.WriteLine(result);
        }
    }
}
