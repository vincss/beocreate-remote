using BeocreateRemote.Helper;
using System.Diagnostics;

#if ANDROID
using Android.Views;
#endif

namespace BeocreateRemote.Pages;

public partial class AudioControlPage : ContentPage, IOnPageKeyDown
{
#pragma warning disable IDE0052 // Remove unread private members
    private readonly AudioControlViewModel audioVolumeViewModel;
#pragma warning restore IDE0052 // Remove unread private members

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
            case Keycode.VolumeUp:
                Debug.WriteLine("AudioVolumePage: DpadUp");
                audioVolumeViewModel.VolumeIncrease();

                // Your code here
                return true;

            case Keycode.VolumeDown:

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