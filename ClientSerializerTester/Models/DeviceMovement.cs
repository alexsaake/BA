using System;
using System.Text.Json.Serialization;

namespace ClientSerializerTester.Models
{
    public class DeviceMovement : BaseModel
    {
        public DeviceMovement(Accelerometer accelerometer,
            Barometer barometer,
            Geolocation geolocation,
            Gyroscope gyroscope)
        {
            timeStamp = DateTime.Now;
            Accelerometer = accelerometer;
            Barometer = barometer;
            Geolocation = geolocation;
            Gyroscope = gyroscope;
        }

        private DateTime timeStamp;

        [JsonPropertyName("timestamp")]
        public DateTime TimeStamp
        {
            get { return timeStamp; }
        }

        private Accelerometer accelerometer;

        [JsonPropertyName("accelerometer")]
        public Accelerometer Accelerometer
        {
            get { return accelerometer; }
            set { accelerometer = value; }
        }

        private Barometer barometer;

        [JsonPropertyName("barometer")]
        public Barometer Barometer 
        {
            get { return barometer; }
            set { barometer = value; }
        }

        private Geolocation geolocation;

        [JsonPropertyName("geolocation")]
        public Geolocation Geolocation
        {
            get { return geolocation; }
            set { geolocation = value; }
        }

        private Gyroscope gyroscope;

        [JsonPropertyName("gyroscope")]
        public Gyroscope Gyroscope
        {
            get { return gyroscope; }
            set { gyroscope = value; }
        }
    }
}