namespace BeocreateRemote.Core.mock
{
    public class MockController : IRemoteController
    {
        public MockController()
        {
            Volume = 33;
            Mute = false;
        }

        public int Volume { get; set; }
        public bool Mute { get; set; }

        public bool IsConnected => true;

        public int GetTemperature()
        {
            return new Random().Next(40, 60);
        }

    }
}
