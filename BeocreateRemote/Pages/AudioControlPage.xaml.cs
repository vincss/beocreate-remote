using BeocreateRemote.Helper;
using System.Diagnostics;

#if ANDROID
using Android.Views;
#endif

namespace BeocreateRemote.Pages;

public partial class AudioControlPage : ContentPage, IOnPageKeyDown
{
    private readonly AudioControlViewModel audioVolumeViewModel;

    public AudioControlPage(AudioControlViewModel audioVolumeViewModel)
    {
        InitializeComponent();
        BindingContext = audioVolumeViewModel;
        this.audioVolumeViewModel = audioVolumeViewModel;
    }

#if ANDROID
    public bool OnPageKeyDown(Keycode keyCode, KeyEvent e)
    {
        switch (keyCode)
        {
            case Android.Views.Keycode.VolumeUp:
                Debug.WriteLine("AudioVolumePage: DpadUp");
                audioVolumeViewModel.VolumeIncrease();

                // Your code here
                return true;

            case Android.Views.Keycode.VolumeDown:

                Debug.WriteLine("AudioVolumePage: DpadDown");
                // Your code here
                audioVolumeViewModel.VolumeDecrease();
                return true;

            default:
                return false;
        }
    }
#endif

}