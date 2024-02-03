using BeocreateRemote.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeocreateRemote.Pages
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        int temperature;

        private readonly IRemoteController sshController;

        public MainViewModel(IRemoteController remoteController)
        {
            sshController = remoteController;
            temperature = remoteController.GetTemperature();
        }

        [RelayCommand]
        void Refresh()
        {
            Temperature = sshController.GetTemperature();
        }

        [RelayCommand]
        void NavigateToAudioControl()
        {
            Shell.Current.GoToAsync($"{nameof(AudioControlPage)}");
        }

        [RelayCommand]
        void NavigateToConfiguration()
        {
            Shell.Current.GoToAsync($"{nameof(ConfigurationPage)}");
        }
    }
}
