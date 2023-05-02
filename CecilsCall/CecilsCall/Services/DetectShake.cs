using System;
using CecilsCall.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CecilsCall.Services
{
    public class DetectShake
    {
        // Set speed delay for monitoring changes.
        public static SensorSpeed speed = SensorSpeed.Game;
        public bool isShaken = false;
        public DetectShake()
        {
            // Register for reading changes, be sure to unsubscribe when finished
            Accelerometer.ShakeDetected += Accelerometer_ShakeDetected;
        }

        void Accelerometer_ShakeDetected(object sender, EventArgs e)
        {
            if (isShaken) return;
            isShaken = true;// signal that the phone is shaken already

            DebugPage.AppendLine("DetectShake.Accelerometer_ShakeDetected");

            // Kill any AboutPage who subscribes to this message
            MessagingCenter.Send("AnythingString", "KillAboutPage");
        }

        public void StopMonitoring()
        {
            Accelerometer.Stop();
        }
        public void StartMonitoring()
        {
            Accelerometer.Start(speed);
        }
        public void ToggleAccelerometer()
        {
            try
            {
                if (Accelerometer.IsMonitoring)
                    Accelerometer.Stop();
                else
                    Accelerometer.Start(speed);
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature not supported on device
            }
            catch (Exception ex)
            {
                // Other error has occurred.
            }
        }
    }
}
