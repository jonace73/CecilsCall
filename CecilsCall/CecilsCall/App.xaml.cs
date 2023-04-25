using CecilsCall.Services;
using Xamarin.Forms;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace CecilsCall
{
    /* This class is the "MAIN" of the program */
    public partial class App : Application
    {
        public static bool isInDebug = false;

        public App()
        {
            InitializeComponent();

            // Initialize main page
            MainPage = new AppShell();//AppShell.xaml as the main page

            // Set alarm
            DependencyService.Get<IAlarmClock>().SetNearestInTimeAlarm();
        }
        protected override void OnStart()
        {
            AppCenter.Start("android=1d761f5e-34f5-4a7e-9083-0675f7eaf823;" +
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
    }
}
