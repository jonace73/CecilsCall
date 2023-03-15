using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using CecilsCall.Models;
using CecilsCall.Services;
using Xamarin.Forms;

namespace CecilsCall.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]

    /* This class is used when a particular database entry is processed */
    public partial class AlarmEntryPage : ContentPage
    {
        AlarmP alarm;
        public string ItemId
        {
            set
            {
                LoadAlarm(value);
            }
        }
        // This constructor is called when the QueryProperty is not supplied with ItemId in GoToAsync
        public AlarmEntryPage()
        {
            InitializeComponent();
            alarm = new AlarmP();
        }
        async void LoadAlarm(string itemId)
        {
            try
            {
                int id = Convert.ToInt32(itemId);

                // Retrieve the Alarm.
                alarm = await AlarmPage.DBAlarms.GetAlarmAsync(id);

                // Display time in timePicker
                timePicked.Time = TimeSpan.Parse(alarm.Text);
            }
            catch (Exception)
            {
                await DisplayAlert("Error:", "Failed to load Alarm.", "OK");
            }
        }
        void OnTimePickerPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Time")
            {
                alarm.Text = timePicked.Time.ToString(@"hh\:mm\:ss");
            }
        }
        async void OnSaveButtonClicked(object sender, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(alarm.Text))
            {
                // If alarm time is unique then save else no
                bool shouldSave = await ShouldSaveAlarm(alarm);
                if (shouldSave)
                {
                    alarm.Date = DateTime.UtcNow;
                    await AlarmPage.DBAlarms.SaveAlarmAsync(alarm);
                }
                else
                {
                    await DisplayAlert("Error:", TrimAlarmTime(alarm.Text) + " is already present.", "OK");
                }

                // Reset Alarm
                DependencyService.Get<IAlarmClock>().ResetAlarm();
            }

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }
        async Task<bool> ShouldSaveAlarm(AlarmP alarm)
        {
            List<AlarmP> DB = await AlarmPage.DBAlarms.GetAlarmsAsync();
            foreach (AlarmP pAlarm in DB)
            {
                if (CompareAlarmTimes(pAlarm, alarm)) return false;
            }
            return true;
        }
        bool CompareAlarmTimes(AlarmP alarmFromDB, AlarmP inputAlarm)
        {
            string fromDB = TrimAlarmTime(alarmFromDB.Text);
            string inAlarm = TrimAlarmTime(inputAlarm.Text);
            return fromDB == inAlarm;
        }
        string TrimAlarmTime(string givenTime)
        {
            // Remove seconds
            string[] words = givenTime.Split(':');
            return words[0] + ":" + words[1];
        }
        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            // Delete alarm from database
            await AlarmPage.DBAlarms.DeleteAlarmAsync(alarm);

            // Reset Alarm
            DependencyService.Get<IAlarmClock>().ResetAlarm();

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }
    }
}