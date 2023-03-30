using CecilsCall.Services;
using Xamarin.Forms;

namespace CecilsCall
{
    /* This class is the "MAIN" of the program */
    public partial class App : Application
    {
        public static bool isInDebug = true;

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();//AppShell.xaml as the main page

            // Set alarm
            DependencyService.Get<IAlarmClock>().SetNearestInTimeAlarm();
        }
        protected override void OnStart()
        {
        }
        protected override void OnSleep()
        {
        }
        protected override void OnResume()
        {
        }
    }
}