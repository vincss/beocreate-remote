using BeocreateRemote.Core;
using BeocreateRemote.Helper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeocreateRemote.Pages
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        int temperature;

        private readonly ControllerContainer controllerContainer;

        [ObservableProperty]
        private bool isTemperatureAvailable;


        public MainViewModel(ControllerContainer controllerContainer)
        {
            this.controllerContainer = controllerContainer;

            temperatureTimer();
        }

        private async void temperatureTimer()
        {
            var timer = new PeriodicTimer(TimeSpan.FromSeconds(2));

            while (await timer.WaitForNextTickAsync())
            {
                Refresh();
            }
        }

        [RelayCommand]
        void Refresh()
        {

            if (controllerContainer.Controller == null)
            {
                IsTemperatureAvailable = false;
            }
            else
            {
                IsTemperatureAvailable = controllerContainer.Controller.GetType() != typeof(SigmaTcpController);
            }


            Temperature = controllerContainer.Controller?.GetTemperature() ?? -1;
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
