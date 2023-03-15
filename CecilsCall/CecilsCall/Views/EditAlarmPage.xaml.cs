using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CecilsCall.Models;
using CecilsCall.Services;
using Xamarin.Forms;

namespace CecilsCall.Views
{

    /* This class is used for the MAIN view page */
    public partial class EditAlarmPage : ContentPage
    {
        public EditAlarmPage()
        {
            InitializeComponent();
            if (App.isInDebug)
            {
                autoTimeSet.IsVisible = true;
            }
        }
        async protected override void OnAppearing()
        {
            base.OnAppearing();
            // Retrieve all the Alarms from the database EXCEPT the reference alarm,
            // and set them as the data source for the CollectionView.
            collectionView.ItemsSource = await AlarmPage.DBAlarms.GetAlarmsAsync();
            LocalTimer();
#if MY_DEBUG
            autoTimeSet.IsVisible = true;
#endif
        }
        async void OnSettingButtonClicked(object sender, EventArgs e)
        {
            // This is used for SETTING purpose ONLY
            // Use current time then add ten second multiple for each DB entry
            int increment = 0;
            List<AlarmP> DB = await AlarmPage.DBAlarms.GetAlarmsAsync();
            DateTime dateTime = DateTime.Now;
            foreach (AlarmP pAlarm in DB)
            {
                var adjustedTime = dateTime.AddSeconds(60 * (increment++) + 10);
                pAlarm.Text = adjustedTime.ToString("HH:mm:ss");
                await AlarmPage.DBAlarms.SaveAlarmAsync(pAlarm);
            }

            // Refresh page
            OnAppearing();

            // Reset Alarm
            DependencyService.Get<IAlarmClock>().ResetAlarm();
        }
        private void LocalTimer()
        {
            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {
                timeCurrent.Text = DateTime.Now.ToString("HH:mm:ss");
                // runs again, or false to stop
                return true;
            });
        }
        async void OnAddClicked(object sender, EventArgs e)
        {
            // Navigate to the AlarmEntryPage, without passing any data.
            // ===============  ALWAYS declare route for GoToAsync in AppShell constructor ===============
            await Shell.Current.GoToAsync(nameof(AlarmEntryPage));
        }
        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                // Navigate to the AlarmEntryPage, passing the ID as a query parameter.
                AlarmP pAlarm = (AlarmP)e.CurrentSelection.FirstOrDefault();

                // ===============  ALWAYS declare route for GoToAsync in AppShell constructor ===============
                await Shell.Current.GoToAsync($"{nameof(AlarmEntryPage)}?{nameof(AlarmEntryPage.ItemId)}={pAlarm.ID.ToString()}");
            }
        }
    }
}