using BeocreateRemote.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeocreateRemote.ViewModel
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
        void Mute()
        {
            sshController.Mute();
        }

        [RelayCommand]
        void Unmute()
        {
            sshController.Unmute();
        }

        [RelayCommand]
        void Refresh()
        {
            Temperature = sshController.GetTemperature();
        }
    }
}
