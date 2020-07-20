namespace Backend_ML_Gesture_Common.Models
{
    public class Geolocation
    {
        private double latitude;

        public double Latitude
        {
            get { return latitude; }
            set
            {
                latitude = value;
            }
        }

        private double longitude;

        public double Longitude
        {
            get { return longitude; }
            set
            {
                longitude = value;
            }
        }

        private double altitude;

        public double Altitude
        {
            get { return altitude; }
            set
            {
                altitude = value;
            }
        }
    }
}