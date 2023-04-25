using CecilsCall.Models;
using CecilsCall.Services;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace CecilsCall.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Settings : ContentPage
	{
		public static string ownersName = "Jon Anderson";// change to "" for the release version
        public static int maxNumberRepeatitions = 1;// change to 7 for the release version
        public static string sellersContact = "0449271275";
        public Settings ()
		{
			InitializeComponent ();
		}
        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Refresh buttons
            ChangeButtonLooks();
        }
        async void OnSaveSettingsButtonClicked(object sender, EventArgs e)
		{

            Crashes.GenerateTestCrash();

            // Copy input info
            ownersName = OwnersName.Text;
            sellersContact = SellersContact.Text;

            // Check for Maximum number of repeatitions as integer
            if (int.TryParse(MaxNumberRepeatitions.Text, out int value))
			{
				maxNumberRepeatitions = value;

            } else
			{
                await DisplayAlert("Error:", "Maximum number of repeatition is not an integer.", "OK");
            }
        }
        public async void OnCrashButtonClicked(object sender, EventArgs e)
        {
            bool isEnabled = await Analytics.IsEnabledAsync();
            DebugPage.AppendLine("Analytics isEnabled: " + isEnabled);// Analytics.TrackEvent("OnAlarmsToServer clicked");
            if (!isEnabled) return;

            bool didAppCrash = await Crashes.HasCrashedInLastSessionAsync();
            if (didAppCrash)
            {
                ErrorReport crashReport = await Crashes.GetLastSessionCrashReportAsync();
                DebugPage.AppendLine("Last CrashReport: " + crashReport.StackTrace);
                // Send this same report to WhizKod server
            }
        }
        public async void OnAlarmsToServer(object sender, EventArgs e)
        {
            // Send Dummy alarmTime
            string dummyAlarmTime = await MessageToServer.JsonMsgToServer("07:30:00");
            bool IsThereEcho = await DependencyService.Get<ICommWithServer>().SendByDependency(dummyAlarmTime, "ACKNextToInsertUser");
            if (!IsThereEcho) return;

            // Send all alarm times
            string msg = await ExtractAllAlarms();
            await DependencyService.Get<ICommWithServer>().SendByDependency(msg, "ACKinsertAfterDeleteAllAlarms");

            // Close Internet and server connections
            DependencyService.Get<ICommWithServer>().CloseConnectionsByDependency();
        }
        async Task<string> ExtractAllAlarms()
        {
            string alarmTimes = "";
            List<AlarmP> DB = await AlarmPage.DBAlarms.GetAlarmsAsync();
            int numberOfAlarmsLessOne = DB.Count - 1;
            // NOTE: UTC time is sent to the SERVER
            for (int ii = 0; ii < numberOfAlarmsLessOne; ii++)
            {
                alarmTimes += DB[ii].AlarmTimeUTC + " ";
            }
            alarmTimes += DB[numberOfAlarmsLessOne].AlarmTimeUTC;

            return JsonConvert.SerializeObject(new { Type = "AllAlarms", UserName = ownersName, AlarmTimes = alarmTimes });
        }
        //
        public async void OnDebugButtonClicked(object sender, EventArgs e)
		{
            // Toggle debug button 
            App.isInDebug = !App.isInDebug;

            // Change button looks
            ChangeButtonLooks();

            Contact contact = new Contact();
            contact.name= "Manuel Blanco Abello";
            contact.number = "0449271275";
            if (App.isInDebug)
            {
                await ContactsPage.DBContacts.SaveContactAsync(contact);
            } else
            {
                Contact contactToDelete = await ContactsPage.DBContacts.GetContactByNonIDAsync(contact.name, contact.number);
                await ContactsPage.DBContacts.DeleteContactAsync(contactToDelete);
            }

            // Change debug tab's status async
            var AppShellInstance = Xamarin.Forms.Shell.Current as AppShell;
            AppShellInstance.RefreshAppShell();
        }
        private void ChangeButtonLooks()
        {
            if (App.isInDebug)
            {
                DebugButton.Text = "NoDebug";
                // ALWAYS use hex
                DebugButton.BackgroundColor = Color.FromHex("03989E");
            }
            else
            {
                DebugButton.Text = "Debug";
                // ALWAYS use hex 
                DebugButton.BackgroundColor = Color.FromHex("C0C0C0");
            }

        }//*/
    }
}