using CommunityToolkit.Mvvm.ComponentModel;

namespace BeocreateRemote.Content
{
    public partial class AudioViewModel : ObservableObject
    {
        [ObservableProperty]
        string? volume;
    }
}
