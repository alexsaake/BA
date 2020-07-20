using System;

namespace Backend_ML_Gesture_Common.Models
{
    public class GesturePoint
    {
        private DateTime timeStamp;

        public DateTime TimeStamp
        {
            get { return timeStamp; }
            set
            {
                timeStamp = value;
            }
        }

        private Accelerometer accelerometer;

        public Accelerometer Accelerometer
        {
            get { return accelerometer; }
            set
            {
                accelerometer = value;
            }
        }

        private Gyroscope gyroscope;

        public Gyroscope Gyroscope
        {
            get { return gyroscope; }
            set
            {
                gyroscope = value;
            }
        }

        public GesturePoint(Accelerometer accelerometer,
            Gyroscope gyroscope)
        {
            TimeStamp = DateTime.Now;
            Accelerometer = accelerometer;
            Gyroscope = gyroscope;
        }
    }
}