using BeocreateRemote.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeocreateRemote.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        int temperature;

        private readonly IRemoteController sshController;

        public MainViewModel(IRemoteController remoteController)
        {
            this.sshController = remoteController;
            temperature = remoteController.GetTemperature();
        }

        [RelayCommand]
        void Mute()
        {
            this.sshController.Mute();
        }

        [RelayCommand]
        void Unmute()
        {
            this.sshController.Unmute();
        }

        [RelayCommand]
        void Refresh()
        {
            this.Temperature = this.sshController.GetTemperature();
        }
    }
}
