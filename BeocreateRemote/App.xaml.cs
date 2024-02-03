using BeocreateRemote.Model;
using BeocreateRemote.Pages;
using System.Diagnostics;

namespace BeocreateRemote
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
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
                } else
                {
                    Shell.Current.GoToAsync(nameof(AudioControlPage));

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
