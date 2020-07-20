namespace Client_ML_Gesture_Sensors.Models
{
    public class HeartRateMonitor : BaseModel
    {
        private int heartRate;

        public int HeartRate
        {
            get { return heartRate; }
            set
            {
                heartRate = value;
                OnPropertyChanged();
            }
        }
    }
}