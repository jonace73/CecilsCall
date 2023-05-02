﻿using CecilsCall.Services;
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

[assembly: Dependency(typeof(AndroidAlarmClock))]
namespace CecilsCall.Droid.Services
{
    internal class AndroidAlarmClock : IAlarmClock
    {
        public static string currentLocalAlarmTime;
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
            Debugger.Msg("AlarmClock.SetAlarm: " + alarm.ID + " alarm time: " + alarm.AlarmTime);

            try
            {
                // Save current Local Alarm Time
                currentLocalAlarmTime = alarm.AlarmTime;

                Intent alarmIntent = new Intent(Android.App.Application.Context, typeof(AlarmReceiver));
                alarmIntent.SetFlags(ActivityFlags.IncludeStoppedPackages);
                alarmIntent.PutExtra("id", alarm.getIDasString());
                alarmIntent.PutExtra("requestCode", alarm.GetRequestCode().ToString());

                PendingIntent pendingIntent = PendingIntent.GetBroadcast(Android.App.Application.Context, alarm.GetRequestCode(), alarmIntent, PendingIntentFlags.Immutable);//UpdateCurrent

                AlarmManager alarmManager = (AlarmManager)Android.App.Application.Context.GetSystemService(Android.Content.Context.AlarmService);
                alarmManager.SetExact(AlarmType.RtcWakeup, Java.Lang.JavaSystem.CurrentTimeMillis() + alarm.TimeToGoOffFromMidnight(), pendingIntent);
            }
            catch (Exception err)
            {
                Debugger.Msg("LSA.SetAlarm ERROR: " + err.Message);
            }
        }
        public string GetCurrentLocalAlarmTime()
        {
            return currentLocalAlarmTime;
        }
        public void CancelAlarm(string ID, string requestCode)
        {
            var alarmIntent = new Intent(Android.App.Application.Context, typeof(AlarmReceiver));
            alarmIntent.SetFlags(ActivityFlags.IncludeStoppedPackages);
            alarmIntent.PutExtra("id", ID);

            int requestCodeINT = int.Parse(requestCode);
            var alarmManager = (AlarmManager)Android.App.Application.Context.GetSystemService(Context.AlarmService);
            var toDeletePendingIntent = PendingIntent.GetBroadcast(Android.App.Application.Context, requestCodeINT, alarmIntent, PendingIntentFlags.Immutable);//CancelCurrent
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

            // If no alarm set 
            if (newAlarm == null)
            {
                return;
            }

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
        public async override void OnReceive(Android.Content.Context context, Intent intent)
        {

            // Signal system that alarm is on. DON'T TRANSFER THIS CODE
            AlarmPage.isAlarming = true;
            isScreenOff = !PowerAwaker.IsScreenOn();
            Debugger.Msg("AlarmReceiver.Received on: " + DateTime.Now.ToString("HH:mm:ss") + " IsScreenOff: " + isScreenOff);

            // Vibrate
            Vibrate(5000);

            // Sound the alarm. MUST be BEFORE WakeUp for it to be BEFORE BringAppToForeground
            SoundAlarm(context);

            // Wake up screen
            WakeUp(context, intent);

            // Start monitoring shake
            AndroidAlarmClock.gDetectShake.StartMonitoring();
        }
        private async static Task<string> GetAlarmTime(Intent intent)
        {
            // Get Alarm ID
            var idString = (string)intent.Extras.Get("id");
            int id = Convert.ToInt32(idString);

            // Retreive database
            AlarmP alarm = await AlarmPage.DBAlarms.GetAlarmAsync(id);

            // NOTE: UTC time is sent to the SERVER
            return alarm.AlarmTimeUTC;
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
                Debugger.Msg("AlarmReceiver.WakeUp() IsScreenOff: " + !PowerAwaker.IsScreenOn());

                // Wake phone up if asleep. Not screen on also means the phone is NOT INTERACTIVE
                if (!PowerAwaker.IsScreenOn())
                {
                    PowerAwaker.Acquire(context);
                }

                // EVEN IF the app is hidden by lockscreen, STILL the app must be brought to the foreground, and push AlarmPage
                // WHERE the ringing can be stopped using AlarmPage.BeginRemainingAlarmTimeCountDown 

                // If app is not visible (i.e., another app is in the foreground) then bring it to foregroung
                if (MainActivity.isOnPause)
                {
                    BringAppToForeground();
                }

                // Once in the foreground, push new AboutPage onto the modal stack
                Debugger.Msg("AlarmReceiver.WakeUp() App.Current == null: " + (App.Current == null));
                await App.Current.MainPage.Navigation.PushModalAsync(new AlarmPage("true"));
            }
            catch (Exception err)
            {
                Debugger.Msg("AlarmReceiver.WakeUp ERROR: " + err.Message);
            }
        }
        [System.Obsolete]
        private void SoundAlarm(Android.Content.Context context)
        {
            Debugger.Msg("<<<<< AlarmReceiver.SoundAlarm >>>>>");

            // Respond to AudioManager keys on lock screen
            RemoteLockScreenControlReceiver.respondToKey = true;

            try
            {
                // Fire intent to start ringing BEFORE BringAppToForeground that is NECESSARY in MainActivity.OnPause
                DependencyService.Get<ILockScreenAudio>().FireIntent("com.xamarin.action.PLAY");
            }
            catch (Exception err)
            {
                Debugger.Msg("RingBell ERROR FireIntent: " + err.Message);
                // You must call Xamarin.Forms.Forms.Init(); prior to using this property
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
                    break;
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
        public static bool IsScreenOn()
        {

            // Checks if device is not in active state, i.e., the phone is OFF or asleep
            Context context = Android.App.Application.Context;
            PowerManager pm = (PowerManager)context.GetSystemService(Context.PowerService);
            return pm.IsScreenOn;
        }
    }
}