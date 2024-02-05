using BeocreateRemote.Helper;
using BeocreateRemote.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeocreateRemote.Pages
{
    public partial class ConfigurationViewModel : ObservableObject
    {
        [ObservableProperty]
        RemoteType remoteType;

        [ObservableProperty]
        string? address;
        [ObservableProperty]
        string? user;
        [ObservableProperty]
        string? password;

        private readonly ControllerContainer controllerContainer;

        public ConfigurationViewModel(ControllerContainer controllerContainer)
        {
            this.controllerContainer = controllerContainer;
            LoadConfiguration();
        }

        private void LoadConfiguration()
        {
            var configuration = Configuration.Load();
            if (configuration != null)
            {
                RemoteType = configuration.RemoteType;
                if (configuration.GetType() == typeof(SshConfiguration))
                {
                    var sshConfiguration = (SshConfiguration)configuration;
                    Address = sshConfiguration.Address;
                    User = sshConfiguration.User;
                    Password = sshConfiguration.Password;
                }
            }
        }

        [RelayCommand]
        public async Task Save()
        {
            Configuration configuration;
            switch (RemoteType)
            {
                case RemoteType.SshController:
                    configuration = new SshConfiguration { Address = Address, User = User, Password = Password };
                    break;
                case RemoteType.MockController:
                    configuration = new MockConfiguration();
                    break;
                case RemoteType.SigmaTcpController:
                    configuration = new SigmaTcpConfiguration { Address = Address };
                    break;
                default:
                    throw new Exception("Unknown type " + RemoteType);

            }

            var controller = ControllerFactory.Create(configuration);
            if (controller.IsConnected)
            {
                Configuration.Save(configuration);
                controllerContainer.Controller = controller;
                await Application.Current.MainPage.DisplayAlert("Validation", "Connection established.", "Ok");
                await Shell.Current.GoToAsync(nameof(AudioControlPage));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Could not establish connection to server.", "Ok");
            }
        }
    }
}
