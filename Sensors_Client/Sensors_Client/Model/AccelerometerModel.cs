using Xamarin.Essentials;
using Xamarin.Forms;

namespace Sensors_Client.Model
{
    public class AccelerometerModel : BaseSensorModel
    {
        public AccelerometerModel()
        {
            StartCommand = new Command(Start);
            StopCommand = new Command(Stop);
        }

        private float x;
        private float y;
        private float z;

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

        public Command StartCommand { get; }

        void Start()
        {
            if (Accelerometer.IsMonitoring)
                return;

            Accelerometer.ReadingChanged += Accelerometer_DataUpdated;
            Accelerometer.Start(SensorSpeed.UI);
        }

        private void Accelerometer_DataUpdated(object sender, AccelerometerChangedEventArgs e)
        {
            X = e.Reading.Acceleration.X;
            Y = e.Reading.Acceleration.Y;
            Z = e.Reading.Acceleration.Z;
        }

        public Command StopCommand { get; }

        void Stop()
        {
            if (!Accelerometer.IsMonitoring)
                return;

            Accelerometer.ReadingChanged -= Accelerometer_DataUpdated;
            Accelerometer.Stop();
        }
    }
}