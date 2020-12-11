using Android.App;
using Android.OS;
using Android.Views;

namespace AppCentreXamTest.Droid
{
    [Activity(Theme = "@style/Theme.Splash",
        MainLauncher = false,
        NoHistory = true, Icon = "@drawable/Icon")]
    public class SplashScreenActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Window.DecorView.SystemUiVisibility =
                (StatusBarVisibility) ((int) Window.DecorView.SystemUiVisibility ^ (int) SystemUiFlags.LayoutStable ^
                                       (int) SystemUiFlags.LayoutFullscreen);
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);
            base.OnCreate(bundle);
            StartActivity(typeof(MainActivity));
        }
    }
}