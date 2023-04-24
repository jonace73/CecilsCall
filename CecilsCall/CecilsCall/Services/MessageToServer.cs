using CecilsCall.Views;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace CecilsCall.Services
{
    public class MessageToServer
    {
        public async static Task<string> JsonMsgToServer(string alarmTime)
        {
            string type = "AlarmMsg";
            string userName = Settings.ownersName;
            Location location = await GetCurrentLocation();
            string latitude = location.Latitude.ToString();
            string longitude = location.Longitude.ToString();
            return JsonConvert.SerializeObject(new { Type = type, UserName = userName, Latitude = latitude, Longitude = longitude, AlarmTime = alarmTime });
        }
        public async static Task<Location> GetCurrentLocation()
        {            
            try
            {
                CancellationTokenSource cts;
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                cts = new CancellationTokenSource();
                return await Geolocation.GetLocationAsync(request, cts.Token);
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                DebugPage.AppendLine("Handle not supported on device exception: " + fnsEx.Message);
            }
            catch (FeatureNotEnabledException fneEx)
            {
                DebugPage.AppendLine("Handle not enabled on device exception: " + fneEx.Message);
            }
            catch (PermissionException pEx)
            {
                DebugPage.AppendLine("Handle permission exception: " + pEx.Message);
            }
            catch (Exception ex)
            {
                DebugPage.AppendLine("Unable to get location: " + ex.Message);
            }
            return null;
        }
    } // END OF CLASS
}