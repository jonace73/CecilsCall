using CecilsCall.Services;
using Xamarin.Forms;
using CecilsCall.Droid.Services;
using Xamarin.Essentials;
using System;
using Android.Telephony;
using CecilsCall.Views;
using System.Collections.Generic;

[assembly: Dependency(typeof(AndroidSMS))]
namespace CecilsCall.Droid.Services
{

    public class AndroidSMS : ISMS
    {
        [System.Obsolete]
        public void SendSMS(string phoneNumber, string messageText)
        {
            try
            {
                SmsManager.Default.SendTextMessage(phoneNumber, null, messageText, null, null);
            }
            catch (FeatureNotSupportedException err)
            {
                Debugger.Msg("SMS is not supported on this device. ERROR: " + err.Message);
            }
            catch (Exception err)
            {
                Debugger.Msg("SendSMS() ERROR: " + err.Message);
            }
        }
        public async void SendSMStoAssociates(string ownersName)
        {
            Debugger.Msg("LSA.SendSMS");

            string msg = "Please check on " + ownersName + ". Hopefully, nothing wrong happened at " + DateTime.Now.ToLocalTime() + ".";
            List<Models.Contact> DB = await ContactsPage.DBContacts.GetContactsAsync();
            foreach (Models.Contact contact in DB)
            {
                DependencyService.Get<ISMS>().SendSMS(contact.number, msg);
            }
        }
    }
}