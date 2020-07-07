using Xamarin.Essentials;
using Xamarin.Forms;

namespace Sensors_Client.Model
{
    public class GyroscopeModel : BaseSensorModel
    {
        public GyroscopeModel()
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
            if (Gyroscope.IsMonitoring)
                return;

            Gyroscope.ReadingChanged += Gyroscope_DataUpdated;
            Gyroscope.Start(SensorSpeed.UI);
        }

        private void Gyroscope_DataUpdated(object sender, GyroscopeChangedEventArgs e)
        {
            X = e.Reading.AngularVelocity.X;
            Y = e.Reading.AngularVelocity.Y;
            Z = e.Reading.AngularVelocity.Z;
        }

        public Command StopCommand { get; }

        void Stop()
        {
            if (!Gyroscope.IsMonitoring)
                return;

            Gyroscope.ReadingChanged -= Gyroscope_DataUpdated;
            Gyroscope.Stop();
        }
    }
}