

using System;
using Xamarin.Forms;
using CecilsCall.Models;
using System.Diagnostics;

namespace CecilsCall.Views
{
    [QueryProperty(nameof(ContactId), nameof(ContactId))]
    public partial class ContactEntryPage : ContentPage
    {
        public string ContactId
        {
            set
            {
                LoadContact(value);
            }
        }
        public ContactEntryPage()
        {
            InitializeComponent();

            // Set the BindingContext of the page to a new Contact.
            BindingContext = new Contact();
        }
        async void LoadContact(string itemId)
        {
            try
            {
                int id = Convert.ToInt32(itemId);

                // Retrieve the contact.
                Contact contact = await ContactsPage.DBContacts.GetContactAsync(id);

                // Bind contact
                BindingContext = contact;
            }
            catch (Exception)
            {
                await DisplayAlert("Error:", "Failed to load contact.", "OK");
            }
        }
        async void OnSaveContactButtonClicked(object sender, EventArgs e)
        {
            var contact = (Contact)BindingContext;
            int i = 0;
            if (int.TryParse(contact.number, out i))
            {
                await ContactsPage.DBContacts.SaveContactAsync(contact);
            }
            else
            {
                await DisplayAlert("Not a number:", contact.number, "OK");
            }

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }
        async void OnDeleteContactButtonClicked(object sender, EventArgs e)
        {
            var contact = (Contact)BindingContext;
            await ContactsPage.DBContacts.DeleteContactAsync(contact);

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }

    }
}