using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace CecilsCallUITest
{
    [TestFixture(Platform.Android)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }
        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }
        [Test]
        public void TestCecilsCall()
        {
            CheckVersion();
            // PermitSMS(); NOT facilitated in AppCenter
            FillSettings("Noli",3);
            AddContact("Jon Anderson","0449271275");
            AddAlarms();

            WaitThenTap("Debug");
            app.Screenshot("Resulting debug");//*/
        }
        private void CheckVersion()
        {
            app.WaitForElement(c => c.Marked("OK"));
            app.Screenshot("Device version");
        }
        private void PermitSMS()
        {
            Task.Delay(3000);
            WaitThenTap("Debug");
            app.Screenshot("PermitSMS");
            //app.Tap(c => c.Marked("ALLOW"));
            app.Invoke("SendSMSBackdoor");
        }
        private void FillSettings(string ownersName, int numberRepeatitions)
        {
            // Tap edit to input alarm 
            WaitThenTap("Settings");

            // Input contact details
            app.WaitForElement(c => c.Marked("OwnerSettings"));
            app.ClearText(x => x.Marked("OwnerSettings"));
            app.EnterText((x => x.Marked("OwnerSettings")), ownersName);

            app.ClearText(x => x.Marked("RepeatitionsSettings"));
            app.EnterText((x => x.Marked("RepeatitionsSettings")), numberRepeatitions.ToString());

            // Tap save button
            app.Tap(c => c.Marked("Save"));
            app.Screenshot("Settings");
        }
        private void AddAlarms()
        {
            // Tap edit to input alarm
            WaitThenTap("Edit");

            // Set three alarms
            DateTime dateTime = DateTime.Now;

            // The additional time to 3 is enough for the rest of the SetAlarm s to be executed
            SetAlarm(dateTime, 3);
            SetAlarm(dateTime, 4);
            SetAlarm(dateTime, 5);

            // Go back to Alarm page
            WaitThenTap("Alarm");
            app.Screenshot("Back to alarm page");

            ClickAlarmButton(3, 20);
            ClickAlarmButton(1, 0);

            // Wait SMS in your phone

            //ClickAlarmButton(1, 0);
        }
        private void AddContact(string name, string number)
        {
            // Tap edit to input alarm
            WaitThenTap("Contacts");

            // Tap add
            WaitThenTap("Add");

            // Input contact details
            app.WaitForElement(c => c.Marked("ContactName"));
            app.ClearText(x => x.Marked("ContactName"));
            app.EnterText((x => x.Marked("ContactName")), name);

            app.ClearText(x => x.Marked("ContactNumber"));
            app.EnterText((x => x.Marked("ContactNumber")), number);
            app.Screenshot("Contact name and number");

            // Tap save button
            app.Tap(c => c.Marked("ContactSave"));

            // Wait for Add then take picture
            app.WaitForElement(c => c.Marked("Add"));
            app.Screenshot("Save tapped AddContact");
        }
        private void ClickAlarmButton(int waitingMinute, int waitingSeconds)
        {
            // Wait for alarm then tap off
            app.WaitForElement(c => c.Marked("AlarmOffBtnID"), "Didn't see the Put Alarm Off button.", new TimeSpan(0, 0, waitingMinute, waitingSeconds, 0));
            app.Screenshot("Alarm is On ");
            app.Tap(c => c.Marked("AlarmOffBtnID"));

            // Go back to alarm page after alarm is put off
            WaitThenTap("NextAlarmAtID");
            app.Screenshot("Back to alarm page after alarm off");
        }
        private void WaitThenTap(string searchText)
        {
            app.WaitForElement(c => c.Marked(searchText));
            app.Tap(c => c.Marked(searchText));
        }
        private void SetAlarm(DateTime dateTime, int additionalMinute)
        {
            // additionalMinute = additional minutes added to NOW.

            // Tap add
            WaitThenTap("Add");

            // Add minutes to NOW
            DateTime adjustedTime = dateTime.AddMinutes(additionalMinute);

            // Wait for Save then enter time
            app.WaitForElement(c => c.Marked("ButtonSave"));
            app.ClearText(x => x.Marked("EnterAlarmEditor"));
            string timeStr = adjustedTime.ToString("HH:mm:ss");
            app.EnterText((x => x.Marked("EnterAlarmEditor")), timeStr);
            app.Screenshot(" Editor alarm time " + timeStr);

            // Tap Save
            WaitThenTap("ButtonSave");

            // Wait for Edit then take a picture
            app.WaitForElement(c => c.Marked("Current time:"));
            app.Screenshot("Current time " + timeStr);
        }
    } // END class
}
