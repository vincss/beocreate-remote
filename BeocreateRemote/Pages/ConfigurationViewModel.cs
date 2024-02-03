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

        [RelayCommand]
        public Task Save() {
            Configuration configuration;
            switch (RemoteType)
            {
                case RemoteType.SshController:
                    configuration = new SshConfiguration { RemoteType = RemoteType, Address = Address, User = User, Password = Password };
                    break;
                case RemoteType.MockController:
                    configuration = new MockConfiguration();
                    break;
                default:
                    throw new Exception("Unknown type " + RemoteType);
                    
            }

            // ToDo Test config

            Configuration.Save(configuration);
            return Task.CompletedTask;
        }
    }
}
