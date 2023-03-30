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
            Routing.RegisterRoute(nameof(ContactEntryPage), typeof(ContactEntryPage));;

            // Set debug tab based on debug status
            DebugInAppShell.IsVisible = App.isInDebug;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            DebugInAppShell.IsVisible = App.isInDebug;

        }
    } // Class END
}