namespace BeocreateRemote.Pages;

public partial class AudioVolumePage : ContentPage
{
    public AudioVolumePage(AudioControlViewModel audioVolumeViewModel)
    {
        InitializeComponent();
        BindingContext = audioVolumeViewModel;
    }
}