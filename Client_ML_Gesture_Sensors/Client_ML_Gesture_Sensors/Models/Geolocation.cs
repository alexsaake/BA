namespace Client_ML_Gesture_Sensors.Models
{
    public class Geolocation : BaseModel
    {
        private double latitude;

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