namespace Client_ML_Gesture_Sensors.Models
{
    public class LightSensor : BaseModel
    {
        private float level;

        public float Level
        {
            get { return level; }
            set
            {
                level = value;
                OnPropertyChanged();
            }
        }
    }
}