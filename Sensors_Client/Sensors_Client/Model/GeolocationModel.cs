using System;
using System.Diagnostics;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Sensors_Client.Model
{
    public class GeolocationModel : BaseSensorModel
    {
        public GeolocationModel()
        {
            StartCommand = new Command(Start);
            StopCommand = new Command(Stop);
        }

        private double latitude;
        private double longitude;
        private double altitude;

        public double Latitude
        {
            get { return latitude; }
            set
            {
                latitude = value;
                OnPropertyChanged();
            }
        }

        public double Longitude
        {
            get { return longitude; }
            set
            {
                longitude = value;
                OnPropertyChanged();
            }
        }

        public double Altitude
        {
            get { return altitude; }
            set
            {
                altitude = value;
                OnPropertyChanged();
            }
        }

        public Command StartCommand { get; }

        void Start()
        {
            GetGeolocation();
        }

        async void GetGeolocation()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                if(location == null)
                {
                    location = await Geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Low,
                        Timeout = TimeSpan.FromSeconds(30)
                    });
                }

                if (location == null)
                {

                }
                else
                {
                    Latitude = location.Latitude;
                    Longitude = location.Longitude;
                    Altitude = (double)location.Altitude;
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Something is wrong: {ex.Message}");
            }
        }

        public Command StopCommand { get; }

        void Stop()
        {

        }
    }
}