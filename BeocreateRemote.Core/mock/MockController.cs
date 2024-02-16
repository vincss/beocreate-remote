namespace BeocreateRemote.Core.mock
{
    public class MockController : IRemoteController
    {
        public MockController()
        {
            Volume = 33;
            _mute = false;
        }

        private bool _mute;
        public int Volume { get; set; }

        public bool IsConnected => true;

        public int GetTemperature()
        {
            return new Random().Next(40, 60);
        }

        public void Mute()
        {
            _mute = true;
        }

        public void Unmute()
        {
            _mute = false;
        }

        public void ToggleMute()
        {
            _mute = !_mute;
        }
    }
}
