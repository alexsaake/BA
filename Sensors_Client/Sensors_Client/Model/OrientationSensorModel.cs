using Xamarin.Essentials;
using Xamarin.Forms;

namespace Sensors_Client.Model
{
    public class OrientationSensorModel : BaseSensorModel
    {
        public OrientationSensorModel()
        {
            StartCommand = new Command(Start);
            StopCommand = new Command(Stop);
        }

        private float x;
        private float y;
        private float z;
        private float w;

        public float X
        {
            get { return x; }
            set
            {
                x = value;
                OnPropertyChanged();
            }
        }

        public float Y
        {
            get { return y; }
            set
            {
                y = value;
                OnPropertyChanged();
            }
        }

        public float Z
        {
            get { return z; }
            set
            {
                z = value;
                OnPropertyChanged();
            }
        }

        public float W
        {
            get { return w; }
            set
            {
                w = value;
                OnPropertyChanged();
            }
        }

        public Command StartCommand { get; }

        void Start()
        {
            if (OrientationSensor.IsMonitoring)
                return;

            OrientationSensor.ReadingChanged += OrientationSensor_DataUpdated;
            OrientationSensor.Start(SensorSpeed.UI);
        }

        private void OrientationSensor_DataUpdated(object sender, OrientationSensorChangedEventArgs e)
        {
            X = e.Reading.Orientation.X;
            Y = e.Reading.Orientation.Y;
            Z = e.Reading.Orientation.Z;
            W = e.Reading.Orientation.W;
        }

        public Command StopCommand { get; }

        void Stop()
        {
            if (!OrientationSensor.IsMonitoring)
                return;

            OrientationSensor.ReadingChanged -= OrientationSensor_DataUpdated;
            OrientationSensor.Stop();
        }
    }
}