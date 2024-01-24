using BeocreateRemote.Core;
using BeocreateRemote.Helper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Diagnostics;

namespace BeocreateRemote.Pages
{
    public partial class AudioControlViewModel : ObservableObject, INotifyPropertyChanged
    {
        private readonly IRemoteController remoteController;
        bool? _muted;

        public AudioControlViewModel(IRemoteController remoteController)
        {
            this.remoteController = remoteController;
        }

        public string? Volume
        {
            get
            {
                var volume = VolumeToStringConverter.Convert(this.remoteController.Volume);
                Debug.WriteLine("Volume get " + volume);
                return volume;
            }
            set
            {
                if (value == null) return;
                var volume = VolumeToStringConverter.ConvertBack(value);
                Debug.WriteLine("Volume set " + volume);

                this.remoteController.Volume = volume;
                OnPropertyChanged(nameof(Volume));
            }
        }

        [RelayCommand]
        public void MuteOnOff()
        {

            if (_muted == true)
            {
                remoteController.Unmute();
                _muted = false;
            }
            else
            {
                remoteController.Mute();
                _muted = true;
            }

        }
    }
}
