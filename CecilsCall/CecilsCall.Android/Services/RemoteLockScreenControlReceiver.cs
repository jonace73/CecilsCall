using Android.Content;
using Android.Views;
using Java.Security;
using System;
using CecilsCall.Services;
using Xamarin.Forms;

namespace CecilsCall.Droid.Services
{
    [System.Obsolete]
    [BroadcastReceiver]
    public class RemoteLockScreenControlReceiver : BroadcastReceiver
    {
        public string ComponentName { get { return this.Class.Name; } }
        public static bool respondToKey = false;

        public override void OnReceive(Context context, Intent intent)
        {
            // This method will act on the action on the media player button on LOCK SCREEN

            try
            {
                // admitKey is set true upon ringing in AlarmReceiver.RingBell()
                if (respondToKey)
                {
                    // This prevents responding to succeeding key presses after the first
                    respondToKey = false;
                }
                else
                {
                    return;
                }

                if (intent.Action != Intent.ActionMediaButton) return;

                //The event will fire twice, up and down.
                // we only want to handle the down event though.
                var key = (KeyEvent)intent.GetParcelableExtra(Intent.ExtraKeyEvent);
                if (key.Action != KeyEventActions.Down) return;

                var action = AndroidLockScreenAudio.ActionPlay;

                switch (key.KeyCode)
                {
                    case Keycode.Headsethook:
                    case Keycode.MediaPlayPause:
                        action = AndroidLockScreenAudio.ActionPause;
                        break;
                    case Keycode.MediaPlay:
                        action = AndroidLockScreenAudio.ActionPlay;
                        break;
                    case Keycode.MediaPause:
                        action = AndroidLockScreenAudio.ActionPause;
                        break;
                    case Keycode.MediaStop:
                        action = AndroidLockScreenAudio.ActionPause;
                        break;
                    case Keycode.MediaNext:
                        action = AndroidLockScreenAudio.ActionPause;
                        break;
                    case Keycode.MediaPrevious:
                        action = AndroidLockScreenAudio.ActionPause;
                        break;
                    default:
                        return;
                }

                Debugger.Msg("<<<<< RLSCR.OnReceive action: " + action + " >>>>>");

                DependencyService.Get<ILockScreenAudio>().FireIntent(action);
            }
            catch (Exception err)
            {
                Debugger.Msg("RLSCR.OnReceive ERROR: " + err.Message);
            }
        }
    } // END CLASS
}