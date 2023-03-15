using CecilsCall.Services;
using Xamarin.Forms;
using CecilsCall.Droid.Services;
using CecilsCall.Models;
using Android.App;
using Android.Content;
using System;
using CecilsCall.Views;
using Android.OS;
using static Android.App.ActivityManager;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Hardware.Display;
using Android.Views;
using Xamarin.Essentials;

[assembly: Dependency(typeof(AndroidAlarmClock))]
namespace CecilsCall.Droid.Services
{
    internal class AndroidAlarmClock : IAlarmClock
    {
        public static AlarmP oldAlarm = null;
        static DetectShake vDetectShake;
        public static DetectShake gDetectShake
        {
            get
            {
                if (vDetectShake == null)
                {
                    vDetectShake = new DetectShake();
                }
                return vDetectShake;
            }
        }
        public void SetAlarm(AlarmP alarm)
        {
            try
            {
                Debugger.Msg("AlarmClock.SetAlarm: " + alarm.ID + " alarm time: " + alarm.Text);

                var alarmIntent = new Intent(Android.App.Application.Context, typeof(AlarmReceiver));
                alarmIntent.SetFlags(ActivityFlags.IncludeStoppedPackages);
                alarmIntent.PutExtra("id", alarm.getIDasString());
                alarmIntent.PutExtra("requestCode", alarm.GetRequestCode().ToString());

                PendingIntent pendingIntent = PendingIntent.GetBroadcast(Android.App.Application.Context, alarm.GetRequestCode(), alarmIntent, PendingIntentFlags.UpdateCurrent);

                AlarmManager alarmManager = (AlarmManager)Android.App.Application.Context.GetSystemService(Android.Content.Context.AlarmService);
                alarmManager.SetExact(AlarmType.RtcWakeup, Java.Lang.JavaSystem.CurrentTimeMillis() + alarm.TimeToGoOffFromMidnight(), pendingIntent);

            }
            catch (Exception err)
            {
                Debugger.Msg("LSA.SetAlarm ERROR: " + err.Message);
            }
        }
        public void CancelAlarm(string ID, string requestCode)
        {
            var alarmIntent = new Intent(Android.App.Application.Context, typeof(AlarmReceiver));
            alarmIntent.SetFlags(ActivityFlags.IncludeStoppedPackages);
            alarmIntent.PutExtra("id", ID);

            int requestCodeINT = int.Parse(requestCode);
            var alarmManager = (AlarmManager)Android.App.Application.Context.GetSystemService(Context.AlarmService);
            var toDeletePendingIntent = PendingIntent.GetBroadcast(Android.App.Application.Context, requestCodeINT, alarmIntent, PendingIntentFlags.CancelCurrent);
            alarmManager.Cancel(toDeletePendingIntent);
        }
        public AlarmP NearestFutureAlarm(List<AlarmP> database)
        {
            // If there is no alarm, return
            AlarmP result = null;
            if (database.Count == 0)
            {
                return result;
            }

            // Find the nearest future alarm
            long timeDifference = long.MaxValue;
            foreach (AlarmP alarm in database)
            {
                long pTimeDiff = AlarmP.TimeSpanInMilliSeconds(alarm.TimeDiffToCurrent());
                if (pTimeDiff < 0)
                {
                    continue;
                }
                if (timeDifference > pTimeDiff)
                {
                    result = alarm;
                    timeDifference = pTimeDiff;
                }
            }
            if (result != null) return result;

            // If not found find the earliest
            long alarmTime = long.MaxValue;
            foreach (AlarmP alarm in database)
            {
                long currentAlarmTime = alarm.TimeInSeconds();
                if (currentAlarmTime < alarmTime)
                {
                    result = alarm;
                    alarmTime = currentAlarmTime;
                }
            }
            return result;
        }
        public void ResetAlarm()
        {
            Debugger.Msg("AndroidAlarmClock.ResetAlarm");

            // // ================ Cancel OLD alarm ================
            if (oldAlarm != null)
            {
                CancelAlarm(oldAlarm.getIDasString(), oldAlarm.GetRequestCodeAsString());
            }

            // ================ Set NEW alarm ================
            SetNearestInTimeAlarm();
        }
        async public Task<AlarmP> GetSoonestAlarm()
        {
            // Retreive database
            List<AlarmP> database = await AlarmPage.DBAlarms.GetAlarmsAsync();

            // Set the nearest future alarm
            return NearestFutureAlarm(database);
        }
        async public void SetNearestInTimeAlarm()
        {
            // Set the nearest future alarm
            AlarmP newAlarm = await GetSoonestAlarm();

            // Save new as old alarm
            oldAlarm = newAlarm;

            // Do nothing if null
            if (newAlarm == null) return;

            // Set new alarm
            SetAlarm(newAlarm);
        }
        public void ReleasePower()
        {
            PowerAwaker.Release();
        }
        public DetectShake GetDetectShake()
        {
            return gDetectShake;
        }
    }

    [BroadcastReceiver]
    public class AlarmReceiver : BroadcastReceiver
    {
        public static bool isScreenOff = false;
        public override void OnReceive(Android.Content.Context context, Intent intent)
        {            
            Debugger.Msg("AlarmReceiver.Received on: " + DateTime.Now.ToString("HH:mm:ss"));

            // Signal system that alarm is on. DON'T TRANSFER THIS CODE
            AlarmPage.isAlarming = true;
            isScreenOff = PowerAwaker.IsScreenOff();
            Debugger.Msg("AlarmReceiver.OnReceive() IsScreenOff: " + isScreenOff);

            // Do the alerting
            Vibrate(5000);
            WakeUp(context, intent);
            RingBell(context);
        }
        private void Vibrate(long duration)
        {
            // Vibrate for a maximum of 5000ms
            Xamarin.Essentials.Vibration.Vibrate(duration);
        }
        [System.Obsolete]
        private async void WakeUp(Android.Content.Context context, Intent intent)
        {
            try
            {
                Debugger.Msg("AlarmReceiver.WakeUp() IsScreenOff: " + PowerAwaker.IsScreenOff());

                // Wake phone up
                PowerAwaker.Acquire(context);

                // During screen lock App.Current != null. However, it will become null
                // when viewing the App after the screen lock activity.
                // Push new AboutPage("true") onto the modal stack
                if (App.Current != null)
                {
                    await App.Current.MainPage.Navigation.PushModalAsync(new AlarmPage("true"));
                }
                else
                {
                    Debugger.Msg("AlarmReceiver.WakeUp App.Current == null");
                }
            }
            catch (Exception err)
            {
                Debugger.Msg("AlarmReceiver.WakeUp ERROR: " + err.Message);
            }
        }
        [System.Obsolete]
        private void RingBell(Android.Content.Context context)
        {
            try
            {
                Debugger.Msg("<<<<< AlarmReceiver.RingBell >>>>>");

                // Respond to AudioManager keys on lock screen
                RemoteLockScreenControlReceiver.respondToKey = true;

                // Fire intent to start ringing BEFORE BringAppToForeground that is NECESSARY in MainActivity.OnPause
                DependencyService.Get<ILockScreenAudio>().FireIntent("com.xamarin.action.PLAY");

                // Start monitoring shake
                AndroidAlarmClock.gDetectShake.StartMonitoring();

                // If onPause, go to foreground
                if (MainActivity.isOnPause)
                {
                    BringAppToForeground();
                }
            }
            catch (Exception err)
            {
                Debugger.Msg("AlarmReceiver.RingBell ERROR: " + err.Message);
            }
        }
        void BringAppToForeground()
        {
            Debugger.Msg("AlarmReceiver.RingBell 2Fground()");

            string packageName = Android.App.Application.Context.PackageName;
            ActivityManager am = (ActivityManager)Android.App.Application.Context.GetSystemService(Context.ActivityService);
            IList<RunningTaskInfo> tasklist = (IList<RunningTaskInfo>)am.GetRunningTasks(10);
            foreach (RunningTaskInfo taskinfo in tasklist)
            {
                if (taskinfo.TopActivity.PackageName == packageName)
                {
                    am.MoveTaskToFront(taskinfo.Id, 0);
                }
            }
        }
    } // CLASS ENDS

    public abstract class PowerAwaker
    {
        private static PowerManager.WakeLock wakeLock;
        public static void Release()
        {
            if (wakeLock != null)
            {
                wakeLock.Release();
            }
            wakeLock = null;
        }
        public static void Acquire(Context context)
        {
            if (wakeLock != null) wakeLock.Release();

            PowerManager pm = (PowerManager)context.GetSystemService(Context.PowerService);
            wakeLock = pm.NewWakeLock(WakeLockFlags.Full | WakeLockFlags.AcquireCausesWakeup | WakeLockFlags.OnAfterRelease, "MainActivity");
            wakeLock.Acquire();
        }
        public static bool IsScreenOff()
        {
            Context context = Android.App.Application.Context;
            PowerManager pm = (PowerManager)context.GetSystemService(Context.PowerService);
            return !pm.IsScreenOn;
        }
    }
}