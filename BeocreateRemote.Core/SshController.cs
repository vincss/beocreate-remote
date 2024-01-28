using Renci.SshNet;
using System.Diagnostics;
using ThrottleDebounce;

namespace BeocreateRemote.Core
{
    public class SshController : IRemoteController

    {
        private int? _volume;
        private int _lastAppliedValue = int.MinValue;
        private readonly SshClient _sshClient;
        private readonly Func<int, Task?> _throttler;
        private static readonly TimeSpan cadence = TimeSpan.FromMilliseconds(250);


        public SshController(string address, string user, string password)
        {
            _sshClient = new SshClient(address, user, password);
            _throttler = Throttler.Throttle((int value) => setVolume(value), cadence, true, true).Invoke;
        }

        private void CheckConnection()
        {

            if (!_sshClient.IsConnected)
            {
                _sshClient.Connect();
            }
        }

        public void Mute()
        {
            CheckConnection();
            var result = _sshClient.RunCommand("dsptoolkit mute");
            Debug.WriteLine(result);
        }
        public void Unmute()
        {
            CheckConnection();
            var result = _sshClient.RunCommand("dsptoolkit unmute");
            Debug.WriteLine(result);
        }

        public int GetTemperature()
        {
            CheckConnection();
            var result = _sshClient.RunCommand("cat /sys/class/thermal/thermal_zone0/temp");
            return (int.Parse(result.Result) / 1000);
        }

        public int Volume
        {
            get
            {
                if (_volume.HasValue)
                {
                    return (int)_volume;
                }

                CheckConnection();
                var result = _sshClient.RunCommand("dsptoolkit get-volume");
                Debug.WriteLine("Ssh get " + result.Result);
                return ConvertVolume(result.Result);
            }
            set
            {
                _volume = value;
                _throttler(value);
            }
        }

        private Task setVolume(int volume)
        {
            if (volume == _lastAppliedValue)
            {
                return Task.CompletedTask;
            }

            CheckConnection();
            var result = _sshClient.RunCommand("dsptoolkit set-volume " + ConvertBackVolume(volume));
            Debug.WriteLine("Ssh setVolume " + result.Result);
            _lastAppliedValue = volume;
            return Task.CompletedTask;
        }

        public static int ConvertVolume(String volume)
        {
            var fragment = volume.Split(' ')[1].Replace('.', ',');
            return (int)(double.Parse(fragment) * 100);

        }

        public static string ConvertBackVolume(int volume)
        {
            return ((double)volume / 100).ToString().Replace(',', '.');

        }
    }
}
