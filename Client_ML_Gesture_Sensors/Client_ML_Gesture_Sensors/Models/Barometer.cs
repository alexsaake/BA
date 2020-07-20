namespace Client_ML_Gesture_Sensors.Models
{
    public class Barometer : BaseModel
    {
        private double pressure;

        public double Pressure
        {
            get { return pressure; }
            set
            {
                pressure = value;
                OnPropertyChanged();
            }
        }
    }
}