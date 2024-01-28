namespace BeocreateRemote.Core
{
    public interface IRemoteController
    {
        public void Mute();
        public void Unmute();
        public int Volume { get; set; }

        public int GetTemperature(); // ToDo put in a another interface

    }
}