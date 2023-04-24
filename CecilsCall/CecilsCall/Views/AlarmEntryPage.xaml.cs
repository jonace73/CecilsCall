//#define IS_UNDER_UITEST

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

#if !IS_UNDER_UITEST
        AlarmP alarm;
#endif
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

            // Make Editor visible when TESTING
#if IS_UNDER_UITEST
            timePicked.IsVisible = false;
            timeEditor.IsVisible = true;
            // Set the BindingContext of the page to a new Note.
            BindingContext = new AlarmP();
#else
            timePicked.IsVisible = true;
            timeEditor.IsVisible = false;
            alarm = new AlarmP();
#endif
        }
        async void LoadAlarm(string itemId)
        {
            try
            {
                int id = Convert.ToInt32(itemId);

                // Retrieve the Alarm.
#if IS_UNDER_UITEST
                AlarmP alarm = await AlarmPage.DBAlarms.GetAlarmAsync(id);
                BindingContext = alarm;
#else
                alarm = await AlarmPage.DBAlarms.GetAlarmAsync(id);
                // Display time in timePicker
                timePicked.Time = TimeSpan.Parse(alarm.AlarmTime);
#endif
            }
            catch (Exception)
            {
                await DisplayAlert("Error:", "Failed to load Alarm.", "OK");
            }
        }
        void OnTimePickerPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
#if !IS_UNDER_UITEST
            if (args.PropertyName == "Time")
            {
                alarm.AlarmTime = timePicked.Time.ToString(@"hh\:mm\:ss");
            }
#endif
        }
        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
#if IS_UNDER_UITEST
                var alarm = (AlarmP)BindingContext;
#endif
                if (!string.IsNullOrWhiteSpace(alarm.AlarmTime))
                {
                    // If alarm time is unique then save else no
                    bool shouldSave = await ShouldSaveAlarm(alarm);
                    if (shouldSave)
                    {
                        alarm.Date = DateTime.UtcNow;

                        // RECORD: UTC time
                        alarm.AlarmTimeUTC = AlarmP.alarmTimeToUTC(alarm.AlarmTime);

                        // Checking alarm saving

                        await AlarmPage.DBAlarms.SaveAlarmAsync(alarm);
                    }
                    else
                    {
                        await DisplayAlert("Error:", TrimAlarmTime(alarm.AlarmTime) + " is already present.", "OK");
                    }

                    // Reset Alarm
                    DependencyService.Get<IAlarmClock>().ResetAlarm();
                }
                else
                {
                    await DisplayAlert("Bug:", "IsNullOrWhiteSpace", "OK");
                }

                // Navigate backwards
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception err)
            {
                await DisplayAlert("OnSaveButtonClicked:", "Error " + err.Message, "OK");
            }
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
            string fromDB = TrimAlarmTime(alarmFromDB.AlarmTime);
            string inAlarm = TrimAlarmTime(inputAlarm.AlarmTime);
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
#if IS_UNDER_UITEST
            var alarm = (AlarmP)BindingContext;
#endif
            // Delete alarm from database
            await AlarmPage.DBAlarms.DeleteAlarmAsync(alarm);

            // Reset Alarm
            DependencyService.Get<IAlarmClock>().ResetAlarm();

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }
    }
}