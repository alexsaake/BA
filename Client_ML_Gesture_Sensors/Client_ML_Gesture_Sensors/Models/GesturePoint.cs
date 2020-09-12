using System;

namespace Client_ML_Gesture_Sensors.Models
{
    public class GesturePoint : BaseModel
    {
        private string timeStamp;

        public string TimeStamp
        {
            get { return timeStamp; }
            set
            {
                timeStamp = value;
                OnPropertyChanged();
            }
        }

        private Accelerometer accelerometer;

        public Accelerometer Accelerometer
        {
            get { return accelerometer; }
            set
            {
                accelerometer = value;
                OnPropertyChanged();
            }
        }

        private Gyroscope gyroscope;

        public Gyroscope Gyroscope
        {
            get { return gyroscope; }
            set
            {
                gyroscope = value;
                OnPropertyChanged();
            }
        }

        public GesturePoint(Accelerometer accelerometer, Gyroscope gyroscope)
        {
            TimeSpan tsPOSIX = DateTime.Now - new DateTime(1970, 1, 1);
            TimeStamp = Math.Floor(tsPOSIX.TotalMilliseconds).ToString();

            Accelerometer = new Accelerometer()
            {
                X = accelerometer.X,
                Y = accelerometer.Y,
                Z = accelerometer.Z
            };

            Gyroscope = new Gyroscope()
            {
                X = gyroscope.X,
                Y = gyroscope.Y,
                Z = gyroscope.Z
            };
        }
    }
}