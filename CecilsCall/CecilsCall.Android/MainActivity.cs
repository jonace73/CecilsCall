using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using CecilsCall.Droid.Services;
using CecilsCall.Services;
using CecilsCall.Views;
using Xamarin.Forms;

namespace CecilsCall.Droid
{
    [System.Obsolete]
    [Activity(Label = "CecilsCall", Icon = "@drawable/LogoCecilsCall", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static bool isOnPause = false;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        protected override void OnPause()
        {
            base.OnPause();
            isOnPause = true;

            // If from lock screen then return
            if (AlarmReceiver.isScreenOff)
            {
                AlarmReceiver.isScreenOff = false;
                return;
            }

            // This checks on home button being pushed
            if (AlarmPage.isAlarming)
            {
                Debugger.Msg("MainActivity.OnPause() isAlarming");
                DependencyService.Get<ILockScreenAudio>().FireIntent("com.xamarin.action.PAUSE");
            }
        }
        protected override void OnResume()
        {
            base.OnResume();
            isOnPause = false;
            Debugger.Msg("MainActivity.OnResume()");
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    } // CLASS ENDS
}