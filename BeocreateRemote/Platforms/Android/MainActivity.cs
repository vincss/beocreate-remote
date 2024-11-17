using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using BeocreateRemote.Helper;
using System.Diagnostics;

namespace BeocreateRemote
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        public override bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent? e)
        {
            Debug.WriteLine("MainActivity: " + keyCode);
            Page p = Shell.Current.CurrentPage;

            if (p is IOnPageKeyDown &&  e != null)
            {
                bool handled = (p as IOnPageKeyDown).OnPageKeyDown(keyCode, e);

                if (handled) return true;
                else return base.OnKeyDown(keyCode, e);
            }
            else return base.OnKeyDown(keyCode, e);
        }
    }
}
