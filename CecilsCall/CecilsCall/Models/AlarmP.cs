using System;
using CecilsCall.Views;
using SQLite;
namespace CecilsCall.Models
{
    /* The instance of this class is used as entry to the database */
    public class AlarmP
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string AlarmTime { get; set; }
        public string AlarmTimeUTC { get; set; }
        public DateTime Date { get; set; }
        public AlarmP()
        {
        }
        public static long twentyFourHoursInMilliseconds = 24 * 60 * 60 * 1000;
        public string getIDasString()
        {
            return ID.ToString();
        }
        public int GetRequestCode()
        {
            // ASSUME: All IDs are unique AFTER installation of this app
            return ID;
        }
        public string GetRequestCodeAsString()
        {
            // ASSUME: All IDs are unique AFTER installation of this app
            return ID.ToString();
        }
        public long TimeToGoOffFromMidnight()
        {

            TimeSpan alarmTime = ToTimeSpan(AlarmTime);
            TimeSpan currentTime = DateTime.Now.ToLocalTime().TimeOfDay;
            TimeSpan timeDiff = alarmTime - currentTime;
            long timeAdjustment = alarmTime < currentTime ? twentyFourHoursInMilliseconds : 0;
            return TimeSpanInMilliSeconds(timeDiff) + timeAdjustment;
        }
        public TimeSpan TimeDiffToCurrent()
        {
            TimeSpan alarmTime = ToTimeSpan(AlarmTime);
            TimeSpan currentTime = DateTime.Now.ToLocalTime().TimeOfDay;
            return alarmTime - currentTime;
        }
        public static string alarmTimeToUTC(string alarmTime)
        {
            DateTime localDate = DateTime.Now.ToLocalTime();
            DateTime univDateTime = localDate.ToUniversalTime();
            TimeSpan timeDiff = localDate - univDateTime;

            TimeSpan localAlarmTime = ToTimeSpan(alarmTime);

            // Adjust for localAlarmTime < timeDiff
            localAlarmTime = localAlarmTime < timeDiff ? localAlarmTime + (new TimeSpan(24, 0, 0)) : localAlarmTime;

            // Convert to UTC
            TimeSpan UTCAlarmTime = localAlarmTime - timeDiff;
            //DebugPage.AppendLine("UTCAlarmTime: " + UTCAlarmTime.ToString(@"hh\:mm\:ss"));
            return UTCAlarmTime.ToString(@"hh\:mm\:ss"); // 24-hour
        }
        public static TimeSpan ToTimeSpan(string alarmTime)
        {
            string[] timeNumbers = alarmTime.Split(':');
            int hour = Int16.Parse(timeNumbers[0]);
            int minute = Int16.Parse(timeNumbers[1]);
            int seconds = Int16.Parse(timeNumbers[2]);

            return new TimeSpan(hour, minute, seconds);
        }
        public long TimeInSeconds()
        {
            string[] timeNumbers = AlarmTime.Split(':');
            int hour = Int16.Parse(timeNumbers[0]);
            int minute = Int16.Parse(timeNumbers[1]);
            int seconds = Int16.Parse(timeNumbers[2]);
            return hour * 60 * 60 + minute * 60 + seconds;
        }
        public static long TimeSpanInMilliSeconds(TimeSpan timeDiff)
        {
            return timeDiff.Hours * 60 * 60 * 1000 + timeDiff.Minutes * 60 * 1000 + timeDiff.Seconds * 1000;
        }

    }
}