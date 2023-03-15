using CecilsCall.Services;
using Xamarin.Forms;
using CecilsCall.iOS.Services;
using CecilsCall.Models;
using System.Threading.Tasks;

[assembly: Dependency(typeof(IOSAlarmClock))]
namespace CecilsCall.iOS.Services
{
    internal class IOSAlarmClock : IAlarmClock
    {

        public void SetAlarm(AlarmP alarm)
        {

        }
        public void CancelAlarm(string ID, string requestCode)
        {

        }
        public void ReleasePower()
        {

        }
        public void ResetAlarm()
        {

        }
        public Task<AlarmP> GetSoonestAlarm()
        {
            return null;
        }
        public void SetNearestInTimeAlarm()
        {

        }
        public DetectShake GetDetectShake()
        {
            return null;
        }
    }
}