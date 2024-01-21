using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeocreateRemote.Content
{
    public partial class AudioViewModel : ObservableObject
    {
        [ObservableProperty]
        string? volume;
    }
}
