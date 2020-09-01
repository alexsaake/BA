using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Client_ML_Gesture_Sensors.Services
{
    public class GeolocationService
    {
        private bool TimerTick;
        private static Models.Geolocation geolocation;

        public GeolocationService()
        {
            geolocation = new Models.Geolocation();
            TimerTick = false;
        }

        public Models.Geolocation Get()
        {
            return geolocation;
        }

        private async Task GetGeolocationAsync()
        {
            try
            {
                var location = await Xamarin.Essentials.Geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Best,
                    Timeout = TimeSpan.FromSeconds(30),

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

        private bool OnTimerTick()
        {
            _ = GetGeolocationAsync();

            return TimerTick;
        }

        public void Subscribe()
        {
            TimerTick = true;
            Device.StartTimer(TimeSpan.FromSeconds(5), OnTimerTick);
        }

        public void Unsubscribe()
        {
            TimerTick = false;
        }
    }
}
