using System;
using System.Linq;
using CecilsCall.Data;
using CecilsCall.Models;
using Xamarin.Forms;
using System.IO;

namespace CecilsCall.Views
{
    public partial class ContactsPage : ContentPage
    {

        static ContactDatabase dbContacts;
        public static ContactDatabase DBContacts
        {
            get
            {
                if (dbContacts == null)
                {
                    dbContacts = new
                    ContactDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Contacts.db3"));
                }
                return dbContacts;
            }
        }
        public ContactsPage()
        {
            InitializeComponent();
        }
        async protected override void OnAppearing()
        {
            base.OnAppearing();

            // Retrieve all the contacts from the database.
            contactsView.ItemsSource = await ContactsPage.DBContacts.GetContactsAsync();
        }
        async void OnAddContactClicked(object sender, EventArgs e)
        {
            // Navigate to the ContactEntryPage, without passing any data.             
            try
            {
                // ===============  ALWAYS declare route for GoToAsync in AppShell constructor ===============
                await Shell.Current.GoToAsync(nameof(ContactEntryPage));
            }
            catch (Exception err)
            {
                await DisplayAlert("Error:", err.Message, "OK");
            }
        }
        async void OnContactSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                // Navigate to the ContactEntryPage, passing the ID as a query parameter. 
                Contact contact = (Contact)e.CurrentSelection.FirstOrDefault();

                // ===============  ALWAYS declare route for GoToAsync in AppShell constructor ===============
                await Shell.Current.GoToAsync($"{nameof(ContactEntryPage)}?{nameof(ContactEntryPage.ContactId)}={contact.ID.ToString()}");
            }
        }

    }
}