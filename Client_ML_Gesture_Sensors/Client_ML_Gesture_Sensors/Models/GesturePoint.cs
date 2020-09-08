using System;

namespace Client_ML_Gesture_Sensors.Models
{
    public class GesturePoint : BaseModel
    {
        private int timeStamp;

        public int TimeStamp
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

        public GesturePoint(Accelerometer accelerometer,
            Gyroscope gyroscope)
        {
            TimeSpan t = (DateTime.Now - new DateTime(1970, 1, 1));

            TimeStamp = (int)t.TotalSeconds;

            Accelerometer = new Accelerometer();
            Gyroscope = new Gyroscope();

            Accelerometer.X = accelerometer.X;
            Accelerometer.Y = accelerometer.Y;
            Accelerometer.Z = accelerometer.Z;
            Gyroscope.X = gyroscope.X;
            Gyroscope.Y = gyroscope.Y;
            Gyroscope.Z = gyroscope.Z;
        }
    }
}