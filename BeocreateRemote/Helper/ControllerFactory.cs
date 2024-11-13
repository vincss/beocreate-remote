using BeocreateRemote.Core;
using BeocreateRemote.Core.mock;
using BeocreateRemote.Model;

namespace BeocreateRemote.Helper
{
    public static class ControllerFactory
    {
        public static IRemoteController Create(Configuration configuration)
        {
            switch (configuration.RemoteType)
            {
                case RemoteType.SshController:
                    var sshConfig = (SshConfiguration)configuration;
                    return new SshController(sshConfig.Address, sshConfig.User, sshConfig.Password);
                case RemoteType.MockController:
                    return new ControllerMock();
                case RemoteType.SigmaTcpController:
                    var sigmaTcpConfiguration = (SigmaTcpConfiguration)configuration;
                    return new SigmaTcpController(sigmaTcpConfiguration.Address);
                default:
                    throw new Exception("Unknown type " + configuration.RemoteType);
            }
        }
    }
}
