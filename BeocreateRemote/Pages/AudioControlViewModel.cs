using BeocreateRemote.Core;
using BeocreateRemote.Helper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;

namespace BeocreateRemote.Pages
{
    public partial class AudioControlViewModel : ObservableObject, INotifyPropertyChanged
    {
        private static readonly double _increment = 1;

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
                //Debug.WriteLine("Volume get " + volume);
                return volume;
            }
            set
            {
                if (value == null) return;
                var volume = VolumeToStringConverter.ConvertBack(value);
                //Debug.WriteLine("Volume set " + volume);

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

        [RelayCommand]
        public void VolumeDecrease()
        {
            if (Volume == null) return;

            Volume = (int.Parse(Volume) - _increment).ToString();
        }


        [RelayCommand]
        public void VolumeIncrease()
        {
            if (Volume == null) return;

            Volume = (int.Parse(Volume) + _increment).ToString();
        }
    }
}
