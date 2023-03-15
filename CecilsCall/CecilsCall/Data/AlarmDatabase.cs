using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using CecilsCall.Models;

namespace CecilsCall.Data
{
    /* This class is used to manage the database */
    public class AlarmDatabase
    {
        readonly SQLiteAsyncConnection database;
        public AlarmDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<AlarmP>().Wait();
        }
        public Task<List<AlarmP>> GetAlarmsAsync()
        {
            //Get all alarms.
            return database.Table<AlarmP>().ToListAsync();
        }
        public Task<AlarmP> GetAlarmAsync(int id)
        {
            // Get a specific Alarm.
            return database.Table<AlarmP>()
            .Where(i => i.ID == id)
            .FirstOrDefaultAsync();
        }
        public Task<int> SaveAlarmAsync(AlarmP alarm)
        {
            if (alarm.ID != 0)
            {
                // Update an existing Alarm.
                return database.UpdateAsync(alarm);
            }
            else
            {
                return database.InsertAsync(alarm);
            }
        }
        public Task<int> DeleteAlarmAsync(AlarmP alarm)
        {
            // Delete a Alarm.
            return database.DeleteAsync(alarm);
        }
    }
}