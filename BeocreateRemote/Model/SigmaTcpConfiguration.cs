namespace BeocreateRemote.Model
{
    public class SigmaTcpConfiguration : Configuration
    {
        public SigmaTcpConfiguration()
        {
            RemoteType = RemoteType.SigmaTcpController;
        }

        public string Address;
    }
}