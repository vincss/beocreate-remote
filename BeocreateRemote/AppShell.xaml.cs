using BeocreateRemote.Pages;

namespace BeocreateRemote
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(AudioVolumePage), typeof(AudioVolumePage));
        }
    }
}
