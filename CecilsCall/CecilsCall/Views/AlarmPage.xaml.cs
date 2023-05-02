using System;
using CecilsCall.Services;
using Xamarin.Forms;
using CecilsCall.Data;
using CecilsCall.Models;
using System.IO;

namespace CecilsCall.Views
{
    public partial class AlarmPage : ContentPage
    {
        static int mNumberOfTicks = 0;
        static bool mContinueTimer = true;
        static bool mSendSMS = true;
        public static bool isAlarming = false;
        static AlarmDatabase dbAlarms;
        public static AlarmDatabase DBAlarms
        {
            get
            {
                if (dbAlarms == null)
                {
                    dbAlarms = new
                    AlarmDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Alarms.db3"));
                }
                return dbAlarms;
            }
        }
        public AlarmPage()
        {
            InitializeComponent();
        }
        public AlarmPage(string activationStatus)
        {
            InitializeComponent();
            BeginRemainingAlarmTimeCountDown();
            DisplayButton(bool.Parse(activationStatus));
            SetSubscriberKillAboutPage();// The AboutPage instance initialized with String input is the ONLY one to be killed
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            DisplaySoonestAlarm();
        }
        async public void DisplaySoonestAlarm()
        {
            // Get the alarm soonest to ring. If there is none change text in AlarmLabel and NextAlarm
            AlarmP alarmP = await DependencyService.Get<IAlarmClock>().GetSoonestAlarm();
            if (alarmP != null)
            {
                AlarmLabel.Text = "Next alarm at:";
                NextAlarm.Text = alarmP.AlarmTime;
            }
            else
            {
                AlarmLabel.Text = "Please click 'Edit' to enter an alarm.";
                NextAlarm.Text = "";
            }
        }
        async void OnKillPageButtonClicked(object sender, EventArgs e)
        {
            // Remove and reset
            CleanUp();// OnKillPageButtonClicked

            // When user click to stop alarm
            // Send alarm message to server USING current time
            string localTime = DependencyService.Get<IAlarmClock>().GetCurrentLocalAlarmTime();
            string UTCtime = AlarmP.alarmTimeToUTC(localTime);
            string msg = await MessageToServer.JsonMsgToServer(UTCtime);
            await DependencyService.Get<ICommWithServer>().SendByDependency(msg, "ACKNextToInsertUser");

            // Close connections
            await DependencyService.Get<ICommWithServer>().CloseConnectionsByDependency();

            // Pop this page from the modal stack
            await Navigation.PopModalAsync();
        }
        private void CleanUp()
        {
            if (isAlarming)
            {
                isAlarming = false;
            }
            else
            {
                return;
            }

            DebugPage.AppendLine("***** CleanUp() *****");

            // Inactivate button
            DisplayButton(false);

            // Reset alarm
            DependencyService.Get<IAlarmClock>().ResetAlarm();

            // Stop monitoring shaking
            DependencyService.Get<IAlarmClock>().GetDetectShake().StopMonitoring();

            // Kill SMS timer
            StopSMSTimer();

            // Release power
            DependencyService.Get<IAlarmClock>().ReleasePower();

            // Stop ringing
            DependencyService.Get<ILockScreenAudio>().FireIntent("com.xamarin.action.STOP");
        }
        void DisplayButton(bool activationStatus)
        {
            // Make button visible
            this.FindByName<Button>("AlarmOffButton").IsVisible = activationStatus;

            // Erase/restore previous page
            AlarmLabel.IsVisible = !activationStatus;
            NextAlarm.IsVisible = !activationStatus;
        }
        async void KillRemainingPages(bool doKill)
        {
            // If button is to hide then kill all pages
            if (doKill && Navigation != null)
            {
                for (; Navigation.ModalStack.Count > 0;)
                {
                    await Navigation.PopModalAsync();
                }
            }
        }
        void SetSubscriberKillAboutPage()
        {
            MessagingCenter.Subscribe<string>(this, "KillAboutPage", async (sender) =>
            {
                DebugPage.AppendLine("SetSubscriberKillAboutPage");

                /// Remove and reset
                CleanUp(); //SetSubscriberKillAboutPage

                // Kill remaining AboutPages
                KillRemainingPages(true); // SetSubscriberKillAboutPage
            });
        }
        private void DisplayRemainingTime(int remainingTime)
        {
            TimeSpan ts = new TimeSpan(0, 0, remainingTime);
            remainingTimeLabel.IsVisible = true;
            this.remainingTime.IsVisible = true;
            this.remainingTime.Text = ts.ToString(@"hh\:mm\:ss");
        }
        private void StopSMSTimer()
        {
            // This method is called to stop the remaining timer

            // Stop alarm time counter
            mSendSMS = false;

            // Reset remaining time
            mNumberOfTicks = 0;
        }
        private void BeginRemainingAlarmTimeCountDown()
        {
            int alarmDuration = App.alarmDuration;
            DebugPage.AppendLine("AlarmPage.BeginRemainingAlarmTimeCountDown alarmDuration: " + alarmDuration);
            remainingTime.IsVisible = true;//Display remaining time
            mSendSMS = true;

            // Start timer
            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    int remainingTimeValue = alarmDuration - mNumberOfTicks++;
                    mContinueTimer = remainingTimeValue > 0;

                    if (mContinueTimer)
                    {
                        DisplayRemainingTime(remainingTimeValue);
                    }
                    else
                    {
                        if (!mSendSMS) return;

                        // Send SMS to associates
                        DependencyService.Get<ISMS>().SendSMStoAssociates(Settings.ownersName);

                        // Reset remaining seconds counter
                        mNumberOfTicks = 0;

                        // Clean system
                        CleanUp(); // When timer expires

                        // Kill AboutPages
                        KillRemainingPages(true);//BeginRemainingAlarmTimeCountDown
                    }
                });

                // runs again, or false to stop
                return mContinueTimer && mSendSMS;
            });
        }
    }
}