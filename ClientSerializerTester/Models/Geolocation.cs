using System.Text.Json.Serialization;

namespace ClientSerializerTester.Models
{
    public class Geolocation : BaseModel
    {
        public Geolocation()
        {
            latitude = 11;
            longitude = 12;
            altitude = 13;
        }

        private double latitude;

        [JsonPropertyName("latitude")]
        public double Latitude
        {
            get { return latitude; }
            set
            {
                latitude = value;
                OnPropertyChanged();
            }
        }

        private double longitude;

        [JsonPropertyName("longitude")]
        public double Longitude
        {
            get { return longitude; }
            set
            {
                longitude = value;
                OnPropertyChanged();
            }
        }

        private double altitude;

        [JsonPropertyName("altitude")]
        public double Altitude
        {
            get { return altitude; }
            set
            {
                altitude = value;
                OnPropertyChanged();
            }
        }
    }
}
