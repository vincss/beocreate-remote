using BeocreateRemote.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeocreateRemote.Pages
{
    public partial class AudioControlViewModel : ObservableObject
    {
        private readonly IRemoteController remoteController;

        public AudioControlViewModel(IRemoteController remoteController) {
            this.remoteController = remoteController;
        }

        [ObservableProperty]
        string? volume;

        [RelayCommand]
        public void MuteOnOff()
        {
            Debug.WriteLine("MuteOnOff");
        }
    }
}
