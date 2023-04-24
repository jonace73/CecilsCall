using CecilsCall.iOS.Services;
using CecilsCall.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(IOSAlarmClock))]
namespace CecilsCall.iOS.Services
{
    internal class IOSCommWithServer : ICommWithServer
    {
        public Task<bool> SendByDependency(string msg, string serverMsg) 
        {
            var tcs = new TaskCompletionSource<bool>();
            tcs.SetResult(true);
            return tcs.Task;
        }
        public Task<bool> CloseConnectionsByDependency()
        {
            var tcs = new TaskCompletionSource<bool>();
            tcs.SetResult(true);
            return tcs.Task;
        }
    }
}