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
<<<<<<< HEAD
            AppCenter.Start("android=863a0dd5-4321-469b-a2ed-ffc9f14ab077;" +
=======
            AppCenter.Start("android={1d761f5e-34f5-4a7e-9083-0675f7eaf823};" +
>>>>>>> e2cb2dcecffefe26b25662a4275c9018e08aad71
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
