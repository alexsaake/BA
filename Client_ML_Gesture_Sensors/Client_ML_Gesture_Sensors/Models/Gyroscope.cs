namespace Client_ML_Gesture_Sensors.Models
{
    public class Gyroscope : BaseModel
    {
        private float x;

        public float X
        {
            get { return x; }
            set
            {
                x = value;
                OnPropertyChanged();
            }
        }

        private float y;

        public float Y
        {
            get { return y; }
            set
            {
                y = value;
                OnPropertyChanged();
            }
        }

        private float z;

        public float Z
        {
            get { return z; }
            set
            {
                z = value;
                OnPropertyChanged();
            }
        }
    }
}
