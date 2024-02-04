using BeocreateRemote.Model;
using BeocreateRemote.Pages;

namespace BeocreateRemote
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ConfigurationPage), typeof(ConfigurationPage));
            Routing.RegisterRoute(nameof(AudioControlPage), typeof(AudioControlPage));
        }

    }
}
