using CecilsCall.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

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
        protected override void OnAppearing()
        {
            base.OnAppearing();
            DebuggingOutputs.Text = DebugText;
        }
        public static void AppendLine(string textToAppend)
        {
            if (App.isInDebug)
                DebugText = DebugText + textToAppend + Environment.NewLine;
        }
        void OnEraseMsgsClicked(object sender, EventArgs e)
        {
            DebugText = "";
            OnAppearing();
        }
        async void OnCopyMsgsClicked(object sender, EventArgs e)
        {
            try
            {
                await Clipboard.SetTextAsync(debugText);
            }
            catch (Exception err)
            {
                AppendLine("DebugPage.OnSendMsgsClicked ERR: " + err.Message);
            }

        }
    }
}