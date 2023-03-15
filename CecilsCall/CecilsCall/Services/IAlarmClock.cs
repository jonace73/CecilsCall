
using CecilsCall.Models;
using System.Threading.Tasks;

namespace CecilsCall.Services
{
    public interface IAlarmClock
    {
        void SetAlarm(AlarmP alarm);
        void CancelAlarm(string ID, string requestCode);
        void ResetAlarm();
        Task<AlarmP> GetSoonestAlarm();
        void SetNearestInTimeAlarm();
        void ReleasePower();
        DetectShake GetDetectShake();
    }
}
