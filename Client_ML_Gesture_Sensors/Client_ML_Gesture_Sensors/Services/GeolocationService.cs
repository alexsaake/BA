using Xamarin.Essentials;
using System;
using System.Threading.Tasks;

using Client_ML_Gesture_Sensors.Models;

namespace Client_ML_Gesture_Sensors.Services
{
    public class GeolocationService
    {
        public async Task GetGeolocationAsync(Models.Geolocation geolocation)
        {
            try
            {
                var location = await Xamarin.Essentials.Geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Low,
                    Timeout = TimeSpan.FromSeconds(30)
                });

                if (location != null)
                {
                    geolocation.Latitude = location.Latitude;
                    geolocation.Longitude = location.Longitude;
                    geolocation.Altitude = (double)location.Altitude;
                }
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
