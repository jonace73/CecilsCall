using Android.App;
using Android.Content;
using Android.Media;
using Android.Net.Wifi;
using Android.OS;
using System;
using CecilsCall.Services;
using Xamarin.Forms;
using CecilsCall.Droid.Services;
using Intent = Android.Content.Intent;
using System.Collections.Generic;
using CecilsCall.Views;

[assembly: Dependency(typeof(AndroidLockScreenAudio))]
namespace CecilsCall.Droid.Services
{
    [System.Obsolete]
    [Service]
    public class AndroidLockScreenAudio : Service, ILockScreenAudio
    {
        //Actions
        public const string ActionPlay = "com.xamarin.action.PLAY";
        public const string ActionPause = "com.xamarin.action.PAUSE";
        public const string ActionStop = "com.xamarin.action.STOP";
        public const string ActionTogglePlayback = "com.xamarin.action.TOGGLEPLAYBACK";
        public const string ActionNext = "com.xamarin.action.NEXT";
        public const string ActionPrevious = "com.xamarin.action.PREVIOUS";

        private MediaPlayer player;
        private AudioManager audioManager;
        private WifiManager wifiManager;
        private WifiManager.WifiLock wifiLock;
        private RemoteControlClient remoteControlClient;
        private ComponentName remoteComponentName;
        private bool paused;

        // Called by StartService()
        public override void OnCreate()
        {
            try
            {
                base.OnCreate();

                //Extract audio and notificaton managers from system services
                audioManager = (AudioManager)Android.App.Application.Context.GetSystemService(Android.Content.Context.AudioService);
                wifiManager = (WifiManager)Android.App.Application.Context.GetSystemService(WifiService);

                // Initialize the receiver of actions from audioManager
                remoteComponentName = new ComponentName(PackageName, new RemoteLockScreenControlReceiver().ComponentName);
            }
            catch (Exception err)
            {
                Debugger.Msg("LSA.OnCreate ERROR: " + err.Message);
            }

        }
        private void RegisterRemoteClient()
        {
            try
            {
                if (remoteControlClient == null)
                {
                    // Register player's button event
                    audioManager.RegisterMediaButtonEventReceiver(remoteComponentName);

                    //Create a new intent that we want triggered by remote control client
                    var mediaButtonIntent = new Intent(Intent.ActionMediaButton);
                    mediaButtonIntent.SetComponent(remoteComponentName);

                    // Create new pending intent for the intent
                    // PendingIntentFlags MUST be = 0. Otherwise, lock screen alarm buttons will not work
                    var mediaPendingIntent = PendingIntent.GetBroadcast(Android.App.Application.Context, 0, mediaButtonIntent, 0);

                    // Create and register the remote control client
                    remoteControlClient = new RemoteControlClient(mediaPendingIntent);
                    audioManager.RegisterRemoteControlClient(remoteControlClient);
                }

                //add transport control flags we can to handle
                remoteControlClient.SetTransportControlFlags(RemoteControlFlags.Play | RemoteControlFlags.Pause |
                    RemoteControlFlags.PlayPause | RemoteControlFlags.Stop | RemoteControlFlags.Previous | RemoteControlFlags.Next);
            }
            catch (Exception err)
            {
                Debugger.Msg("LSA.Register ERROR: " + err.Message);
            }
        }
        private void UnregisterRemoteClient()
        {
            try
            {
                audioManager.UnregisterMediaButtonEventReceiver(remoteComponentName);
                audioManager.UnregisterRemoteControlClient(remoteControlClient);
                remoteControlClient.Dispose();
                remoteControlClient = null;
            }
            catch (Exception err)
            {
                Debugger.Msg("LSA.Unregister ERROR: " + err.Message);
            }
        }
        private void UpdateMetadata()
        {
            if (remoteControlClient == null)
                return;

            var metadataEditor = remoteControlClient.EditMetadata(true);
            metadataEditor.PutString(MetadataKey.Title, "Press buttons below to stop the alarm.");
            metadataEditor.Apply();
        }
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }
        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            // This method is called by OnCreate() OR StartService()

            Debugger.Msg("LSA.OnStartCom(): " + intent.Action);

            try
            {

                switch (intent.Action)
                {
                    case ActionPlay: Play(); break;
                    case ActionStop: Stop(); break;
                    default: ElliminateAudio(); break;
                }
            }
            catch (Exception err)
            {
                Debugger.Msg("LSA.OnStartCom ERROR: " + err.Message);
            }

            //Set sticky as we are a long running operation
            return StartCommandResult.Sticky;
        }
        private void IntializePlayer()
        {
            Debugger.Msg("LSA.IntializePlayer");
            try
            {
                player = new MediaPlayer();

                //Tell our player to sream music
                player.SetAudioStreamType(Stream.Music);

                //Wake mode will be partial to keep the CPU still running under lock screen
                player.SetWakeMode(Android.App.Application.Context, WakeLockFlags.Partial);

                //When we have reached the end of the song, this means the user is unable to stop the song.
                //He/she might be in trouble.
                player.Completion += (sender, args) =>
                {
                    Debugger.Msg("LSA.Completion");

                    // Play more. The playing will be stopped by 
                    DependencyService.Get<ILockScreenAudio>().FireIntent("com.xamarin.action.PLAY");
                };
            }
            catch (Exception err)
            {
                Debugger.Msg("LSA.IntPlayer ERROR: " + err.Message);
            }

            //When we have prepared the song start playback
            player.Prepared += (sender, args) =>
            {
                if (remoteControlClient != null)
                    remoteControlClient.SetPlaybackState(RemoteControlPlayState.Playing);
                UpdateMetadata();
                player.Start();
            };

            // When player gets into error
            player.Error += (sender, args) =>
            {
                if (remoteControlClient != null)
                    remoteControlClient.SetPlaybackState(RemoteControlPlayState.Error);

                Debugger.Msg("LSA.InitPlayer player.Error playback resetting: " + args.What);

                //playback error: Stop will clean up and reset properly.
                Stop();
            };
        }
        public async void Play()
        {
            Debugger.Msg("LSA.Play()");

            try
            {                
                // If first time playing
                if (player == null)
                {
                    IntializePlayer();
                }
                else
                {
                    paused = false;

                    // Play alarm                
                    player.Start();

                    //Update remote client now that we are playing
                    RegisterRemoteClient();
                    remoteControlClient.SetPlaybackState(RemoteControlPlayState.Playing);
                    UpdateMetadata();
                    return;
                }

                if (player.IsPlaying)
                {
                    Debugger.Msg("LSA.Play IsPlaying");
                    return;
                }
            }
            catch (Exception err)
            {
                Debugger.Msg("LSA.Play ERROR: " + err.Message);
            }

            try
            {
                Debugger.Msg("LSA.Play !=null but silent");

                // If not playing AND not first time playing
                var fd = Android.App.Application.Context.Assets.OpenFd("FireAlarm.wav");
                await player.SetDataSourceAsync(fd.FileDescriptor, fd.StartOffset, fd.Length);

                player.PrepareAsync();
                AquireWifiLock();

                //Update the remote control client that we are buffering
                RegisterRemoteClient();
                remoteControlClient.SetPlaybackState(RemoteControlPlayState.Buffering);
                UpdateMetadata();
            }
            catch (Exception err)
            {
                //unable to start playback log error
                Debugger.Msg("LSA.Play ERROR playback resetting: " + err.Message);
            }
        }
        private void Pause()
        {
            try
            {
                if (player == null) return;

                if (player.IsPlaying) player.Pause();

                remoteControlClient.SetPlaybackState(RemoteControlPlayState.Paused);

                paused = true;
            }
            catch (Exception err)
            {
                Debugger.Msg("LSA.Pause ERROR: " + err.Message);
            }
        }
        private void Stop()
        {

            try
            {
                Debugger.Msg("LSA.Stop()");

                if (player == null)
                {
                    Debugger.Msg("LSA.Stop player == null");
                    return;
                }

                if (player.IsPlaying)
                {
                    Debugger.Msg("LSA.Stop IsPlaying");
                    player.Stop();
                    if (remoteControlClient != null)
                    {
                        remoteControlClient.SetPlaybackState(RemoteControlPlayState.Stopped);
                    }
                }

                Debugger.Msg("LSA.Stop != null");

                player.Reset();
                paused = false;
                ReleaseWifiLock();
                UnregisterRemoteClient();

                // =================== DESTROY PLAYER ======================
                // Reset as though the app restarts from the begining
                player.Release();
                player = null;
            }
            catch (Exception err)
            {
                Debugger.Msg("LSA.Stop ERROR: " + err.Message);
            }
        }
        private void AquireWifiLock()
        {
            if (wifiLock == null)
            {
                wifiLock = wifiManager.CreateWifiLock(Android.Net.WifiMode.Full, "xamarin_wifi_lock");
            }
            wifiLock.Acquire();
        }
        private void ReleaseWifiLock()
        {
            if (wifiLock == null) return;

            wifiLock.Release();
            wifiLock = null;
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
            if (player != null)
            {
                player.Release();
                player = null;
            }
        }
        public void FireIntent(string action)
        {
            try
            {
                Debugger.Msg("LSA.FireIntent: " + action);

                Context context = Android.App.Application.Context;
                var intent = new Intent(context, typeof(AndroidLockScreenAudio));
                intent.SetAction(action);
                context.StartService(intent);
            }
            catch (Exception err)
            {
                Debugger.Msg("LSA.FireIntent ERROR: " + err.Message);
            }
        }
        private void ElliminateAudio()
        {
            Debugger.Msg("LSA.ElliminateAudio");

            // Say bye
            UpdateMetadata("Thanks. Bye for now.");

            // Kill any AboutPage who subscribes to this message
            MessagingCenter.Send("AnythingString", "KillAboutPage");
        }
        private void UpdateMetadata(string text)
        {
            if (remoteControlClient == null)
                return;

            var metadataEditor = remoteControlClient.EditMetadata(true);
            metadataEditor.PutString(MetadataKey.Title, text);
            metadataEditor.Apply();
        }
    } // END CLASS    
}