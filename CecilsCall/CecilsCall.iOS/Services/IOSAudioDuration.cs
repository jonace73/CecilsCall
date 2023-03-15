using CecilsCall.Services;
using Xamarin.Forms;
using CecilsCall.iOS.Services;
using System.Threading.Tasks;
using static SQLite.SQLite3;

[assembly: Dependency(typeof(IOSAudioDuration))]
namespace CecilsCall.iOS.Services
{
    internal class IOSAudioDuration : IAudioDuration
    {
        public Task<int> GetAudioDuration()
        {
            // Just to make VS happy
            var tcs = new TaskCompletionSource<int>();
            tcs.SetResult(0);
            return tcs.Task;
        }
    }
}