using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using CecilsCall.Models;

namespace CecilsCall.Data
{
    public class ContactDatabase
    {
        readonly SQLiteAsyncConnection contactDB;
        public ContactDatabase(string dbPath)
        {
            contactDB = new SQLiteAsyncConnection(dbPath);
            contactDB.CreateTableAsync<Contact>().Wait();
        }
        public Task<List<Contact>> GetContactsAsync()
        {
            //Get all Contacts.
            return contactDB.Table<Contact>().ToListAsync();
        }
        public Task<Contact> GetContactAsync(int id)
        {
            // Get a specific Contact.
            return contactDB.Table<Contact>()
            .Where(i => i.ID == id)
            .FirstOrDefaultAsync();
        }
        public Task<int> SaveContactAsync(Contact contact)
        {
            if (contact.ID != 0)
            {
                // Update an existing contact.
                return contactDB.UpdateAsync(contact);
            }
            else
            {
                return contactDB.InsertAsync(contact);
            }
        }
        public Task<int> DeleteContactAsync(Contact contact)
        {
            // Delete a Contact.
            return contactDB.DeleteAsync(contact);
        }
        public Task<Contact> GetContactByNonIDAsync(string name, string number)
        {
            // Get a specific Contact.
            return contactDB.Table<Contact>()
            .Where(i => i.name == name && i.number == number)
            .FirstOrDefaultAsync();
        }
    } // END OF CLASS
}
