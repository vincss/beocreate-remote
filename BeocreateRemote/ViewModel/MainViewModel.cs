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

        private readonly SshController sshController;

        public MainViewModel(SshController sshController)
        {
            this.sshController = sshController;
            temperature = sshController.getTemperature();
        }

        [RelayCommand]
        void Mute()
        {
            this.sshController.mute();
        }

        [RelayCommand]
        void Unmute()
        {
            this.sshController.unmute();
        }

        [RelayCommand]
        void Refresh()
        {
            this.Temperature = this.sshController.getTemperature();
        }
    }
}
