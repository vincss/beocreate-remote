using BeocreateRemote.Helper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;

namespace BeocreateRemote.Pages
{
    public partial class AudioControlViewModel : ObservableObject
    {
        private static readonly double _increment = 1;

        private readonly ControllerContainer controllerContainer;

        public bool Mute
        {
            get => controllerContainer.Controller.Mute; set
            {
                controllerContainer.Controller.Mute = value;
                OnPropertyChanged(nameof(Mute));
                OnPropertyChanged(nameof(MuteText));
            }
        }
        public string MuteText { get { return Mute ? "🔈" : "🔇"; } }

        public AudioControlViewModel(ControllerContainer controllerContainer)
        {
            this.controllerContainer = controllerContainer;
        }

        public string? Volume
        {
            get
            {
                var volume = VolumeToStringConverter.Convert(controllerContainer.Controller.Volume);
                //Debug.WriteLine("Volume get " + volume);
                return volume;
            }
            set
            {
                if (value == null) return;
                var volume = VolumeToStringConverter.ConvertBack(value);
                //Debug.WriteLine("Volume set " + volume);

                controllerContainer.Controller.Volume = volume;
                OnPropertyChanged(nameof(Volume));
            }
        }

        [RelayCommand]
        public Task MuteOnOff()
        {
            Mute = !Mute;
            return Task.CompletedTask;
        }

        [RelayCommand]
        public Task VolumeDecrease()
        {
            if (Volume == null) return Task.CompletedTask;

            Volume = (int.Parse(Volume) - _increment).ToString();
            return Task.CompletedTask;
        }


        [RelayCommand]
        public Task VolumeIncrease()
        {
            if (Volume == null) return Task.CompletedTask; ;

            Volume = (int.Parse(Volume) + _increment).ToString();
            return Task.CompletedTask;
        }
    }
}
