namespace BeocreateRemote.Model
{
    public class SshConfiguration : Configuration
    {
        public SshConfiguration()
        {
            RemoteType = RemoteType.SshController;
        }

        public string Address;
        public string User;
        public string Password;
    }
}
