using Android.Views;
using CecilsCall.Services;
using Xamarin.Essentials;

namespace CecilsCall.Droid.Services
{
    internal class SleepModeHandlerForDroid : ISleepModeHandler
    {
        public void BlockSleepMode(bool blockSleepMode)
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                MainActivity activity = (MainActivity)Platform.CurrentActivity;

                if (blockSleepMode)
                {
                    activity.Window.AddFlags(WindowManagerFlags.ShowWhenLocked);
                }
                else
                {
                    activity.Window.ClearFlags(WindowManagerFlags.KeepScreenOn);
                }
            });
        }
    }
}