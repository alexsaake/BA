using Xamarin.Essentials;
using Xamarin.Forms;

namespace Sensors_Client.Model
{
    public class MagnetometerModel : BaseSensorModel
    {
        public MagnetometerModel()
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
            if (Magnetometer.IsMonitoring)
                return;

            Magnetometer.ReadingChanged += Accelerometer_DataUpdated;
            Magnetometer.Start(SensorSpeed.UI);
        }

        private void Accelerometer_DataUpdated(object sender, MagnetometerChangedEventArgs e)
        {
            X = e.Reading.MagneticField.X;
            Y = e.Reading.MagneticField.Y;
            Z = e.Reading.MagneticField.Z;
        }

        public Command StopCommand { get; }

        void Stop()
        {
            if (!Magnetometer.IsMonitoring)
                return;

            Magnetometer.ReadingChanged -= Accelerometer_DataUpdated;
            Magnetometer.Stop();
        }
    }
}