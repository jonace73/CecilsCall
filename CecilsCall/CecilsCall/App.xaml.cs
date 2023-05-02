using CecilsCall.Services;
using Xamarin.Forms;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;

namespace CecilsCall
{
    /* This class is the "MAIN" of the program */
    public partial class App : Application
    {
        public static bool isInDebug = true;
        public static int alarmDuration;

        public App()
        {
            InitializeComponent();

            // Initialize main page
            MainPage = new AppShell();//AppShell.xaml as the main page

            // Set alarm duration with default repeatition of one
            SetAlarmDuration();

            // Set alarm
            DependencyService.Get<IAlarmClock>().SetNearestInTimeAlarm();
        }
        protected override void OnStart()
        {
            AppCenter.Start("android=dd3469e0-5bd3-4973-9f1b-6975738f6bd2;" +
                              "uwp={Your UWP App secret here};" +
                              "ios={Your iOS App secret here};" +
                              "macos={Your macOS App secret here};",
                              typeof(Analytics), typeof(Crashes));
        }
        protected override void OnSleep()
        {
        }
        protected override void OnResume()
        {
        }
        async void SetAlarmDuration()
        {   
            alarmDuration = await DependencyService.Get<IAudioDuration>().GetAudioDuration();
        }
    }
}
