using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using CecilsCall.Droid.Services;
using CecilsCall.Services;
using CecilsCall.Views;
using Xamarin.Forms;
using AndroidX.Core.Content;
using Android;
using AndroidX.Core.App;
using System;
using Java.Interop;
using System.Threading.Tasks;
using Android.Telephony;
using Java.Nio.Channels;

namespace CecilsCall.Droid
{
    [System.Obsolete]
    [Activity(Label = "CecilsCall", Icon = "@drawable/LogoCecilsCall", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static bool isOnPause = false;
        public static AndroidCommWithServer commWithInternetServer = new AndroidCommWithServer();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Load application
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            // To avoid: "You must call Xamarin.Forms.Forms.Init(); prior to using this property."
            // An error fired due to: DependencyService.Get<ILockScreenAudio>().FireIntent("com.xamarin.action.PLAY");
            // called in AlarmReceiver.SoundAlarm
            // This SEEMS to solve the problem
            Xamarin.Forms.DependencyService.Register<ILockScreenAudio>();

            // Request SMS permission
            RequestSMSpermission();
        }
        protected override void OnPause()
        {
            base.OnPause();
            isOnPause = true;
            Debugger.Msg("MainActivity.OnPause()");

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
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);//
            if (requestCode == 1)
            {

                // Check if the only required permission has been granted
                if ((grantResults.Length == 1) && (grantResults[0] == Permission.Granted))
                {
                    // Location permission has been granted, okay to retrieve the location of the device.
                    Debugger.Msg("SMS permission is granted");
                }
                else
                {
                    Debugger.Msg("SMS permission is NOT granted");
                }
            }
            else
            {
                base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            }//*/
        }
        protected void RequestSMSpermission()
        {

            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.SendSms) == (int)Permission.Granted)
            {
                // We have permission.
                Debugger.Msg("Permission already granted");
                return;
            }

            Debugger.Msg("Permission is needed");
            ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.SendSms }, 1);
        }
        [Export("SendSMSBackdoor")]
        public void SendSMSBackdoor()
        {
            // SMS is NOT facilitated in AppCenter
            SmsManager.Default.SendTextMessage("0449271275", null, "Test sending SMS.", null, null);
        }
    } // CLASS ENDS
}