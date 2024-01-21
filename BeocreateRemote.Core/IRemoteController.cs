namespace BeocreateRemote.Core
{
    public interface IRemoteController
    {
        public void Mute();
        public void Unmute();
        public double Volume { get; set; }

        public int GetTemperature(); // ToDo put in a another interface

    }
}