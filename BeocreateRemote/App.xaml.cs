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

        protected override Window CreateWindow(IActivationState activationState)
        {
            Window window = base.CreateWindow(activationState);
            window.Created += (sender, eventArgs) =>
            {
                // take actions here...
                Debug.WriteLine("Created");
                var configuration = Configuration.Load();
                if (configuration == null)
                {
                    Shell.Current.GoToAsync(nameof(ConfigurationPage));
                }
                else
                {
                    var controller = ControllerFactory.Create(configuration);
                    if (!controller.IsConnected)
                    {
                        Task.Run(async () =>
                        {

                            if (!controller.IsConnected)
                            {
                                await Application.Current.MainPage.DisplayAlert("Failed", "Failed to connect to the server.", "Ok");
                            }
                        });
                        Shell.Current.GoToAsync(nameof(ConfigurationPage));
                    }
                    else
                    {
                        controllerContainer.Controller = controller;
                        Shell.Current.GoToAsync(nameof(AudioControlPage));
                    }
                }
            };
            window.Activated += (sender, eventArgs) =>
            {
                // take actions here...
                Debug.WriteLine("Activated");
            };
            return window;
        }

    }
}
