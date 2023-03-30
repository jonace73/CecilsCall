using CecilsCall.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CecilsCall.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DebugPage : ContentPage
    {
        public static int numberAlarmInterruptions = 0;
        static string debugText;
        public static string DebugText
        {
            get
            {
                if (debugText == null)
                {
                    debugText = "";
                }
                return debugText;
            }
            set
            {
                debugText = value;
            }
        }
        public DebugPage()
        {
            InitializeComponent();
        }
        public static void CheckForInterruptions()
        {
            // Sending SMS fails

            numberAlarmInterruptions++;
            if (numberAlarmInterruptions == 3)// Automate LATER
            {
                DependencyService.Get<ISMS>().SendSMStoAssociates("******* " + debugText + " *******");
                numberAlarmInterruptions = 0;// reset
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            DebuggingOutputs.Text = DebugText;
        }
        public static void AppendLine(string textToAppend)
        {
            DebugText = DebugText + textToAppend + Environment.NewLine;
        }
        void OnEraseMsgsClicked(object sender, EventArgs e)
        {
            DebugText = "";
            OnAppearing();
        }
        void OnSendMsgsClicked(object sender, EventArgs e)
        {
            try
            {
                DependencyService.Get<ISMS>().SendSMS(Settings.sellersContact, DebugText);
            }
            catch (Exception err)
            {
                AppendLine("DebugPage.OnSendMsgsClicked ERR: " + err.Message);
            }

        }
    }
}