namespace BeocreateRemote.Core.mock
{
    public class ControllerMock : IRemoteController
    {
        public int Volume { get; set; } = 33;
        public bool Mute { get; set; } = false;
        public bool IsConnected => true;
    }
}
