using BeocreateRemote.Helper;
using BeocreateRemote.Model;
using BeocreateRemote.Pages;
using System.Diagnostics;

namespace BeocreateRemote
{
    public partial class App : Application
    {
        private readonly ControllerContainer controllerContainer;

        public App(ControllerContainer controllerContainer)
        {
            InitializeComponent();

            MainPage = new AppShell();
            this.controllerContainer = controllerContainer;
        }

        private async void OnRestore()
        {
            Debug.WriteLine("OnRestore");
            var configuration = Configuration.Load();
            if (configuration == null)
            {
                await Shell.Current.GoToAsync(nameof(ConfigurationPage));
            }
            else
            {
                try
                {
                    var controller = ControllerFactory.Create(configuration);
                    if (controller.IsConnected)
                    {
                        controllerContainer.Controller = controller;
                        await Shell.Current.GoToAsync(nameof(AudioControlPage));
                    }
                    else
                    {
                        await Current.MainPage.DisplayAlert("Failed", "Failed to connect to the server.", "Ok");
                        await Shell.Current.GoToAsync(nameof(ConfigurationPage));
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"OnRestore {e.Message}");
                    Shell.Current.GoToAsync(nameof(ConfigurationPage));
                }
            }
        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            Window window = base.CreateWindow(activationState);
            window.Created += (sender, eventArgs) =>
            {
                OnRestore();
            };
            window.Activated += (sender, eventArgs) =>
            {
                OnRestore();
            };
            return window;
        }

    }
}
