using CecilsCall.Services;
using Xamarin.Forms;
using CecilsCall.Droid.Services;
using Android.Media;
using System.Threading.Tasks;
using System;
using CecilsCall.Views;

[assembly: Dependency(typeof(AndroidAudioDuration))]
namespace CecilsCall.Droid.Services
{
    public class AndroidAudioDuration : IAudioDuration
    {
        [System.Obsolete]
        public async Task<int> GetAudioDuration()
        {
            int audioDuration = 0;
            try
            {
                MediaPlayer player = new MediaPlayer();
                player.SetAudioStreamType(Stream.Music);

                var fd = Android.App.Application.Context.Assets.OpenFd("FireAlarm.wav");
                await player.SetDataSourceAsync(fd.FileDescriptor, fd.StartOffset, fd.Length);
                player.Prepare();
                player.Start();

                audioDuration = ((int)(player.Duration)) / 1000;
                audioDuration *= Settings.maxNumberRepeatitions;

                player.Stop();
                player.Release();
                player = null;

            }
            catch (Exception err)
            {
                Debugger.Msg("AD.GetAD ERROR: " + err.Message);
            }

            return audioDuration;
        }
    }
}