using Android.App;
using AppCentreXamTest.Droid.DependencyService;
using ShoppingCart.DependencyServices;
using Xamarin.Forms;

[assembly: Dependency(typeof(CloseApplication))]

namespace AppCentreXamTest.Droid.DependencyService
{
    public class CloseApplication : ICloseApplication
    {
        public void CloseApp()
        {
            var activity = (Activity) Forms.Context;
            activity.FinishAffinity();
        }
    }
}