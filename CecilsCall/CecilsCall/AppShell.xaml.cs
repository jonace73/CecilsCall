using CecilsCall.Views;
using Xamarin.Forms;

namespace CecilsCall
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // To navigate to, first register, AlarmEntryPage and AboutPage
            Routing.RegisterRoute(nameof(AlarmEntryPage), typeof(AlarmEntryPage));
            Routing.RegisterRoute(nameof(AlarmPage), typeof(AlarmPage));
            Routing.RegisterRoute(nameof(ContactEntryPage), typeof(ContactEntryPage));
            if (App.isInDebug)
            {
                DebugInAppShell.IsVisible = true;
            }
        }
    }
}