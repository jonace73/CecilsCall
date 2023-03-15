using CecilsCall.Services;
using CecilsCall.iOS.Services;
using UIKit;
using Xamarin.Forms;

namespace CecilsCall.iOS.Services
{
    [Foundation.Preserve(AllMembers = true)]
    internal class SleepModeHandlerForiOS : ISleepModeHandler
    {
        public void BlockSleepMode(bool blockSleepMode)
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                UIApplication.SharedApplication.IdleTimerDisabled = blockSleepMode;
            });
        }
    }
}