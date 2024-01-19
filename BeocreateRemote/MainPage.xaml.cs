using BeocreateRemote.Core;
using BeocreateRemote.ViewModel;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BeocreateRemote
{
    public partial class MainPage : ContentPage
    {

        public MainPage(MainViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }

    }

}
