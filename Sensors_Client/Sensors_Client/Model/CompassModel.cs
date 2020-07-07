using Xamarin.Essentials;
using Xamarin.Forms;

namespace Sensors_Client.Model
{
    public class CompassModel : BaseSensorModel
    {
        public CompassModel()
        {
            StartCommand = new Command(Start);
            StopCommand = new Command(Stop);
        }

        private double degrees;

        public double Degrees
        {
            get { return degrees; }
            set
            {
                degrees = value;
                OnPropertyChanged();
            }
        }

        public Command StartCommand { get; }

        void Start()
        {
            if (Compass.IsMonitoring)
                return;

            Compass.ReadingChanged += Compass_DataUpdated;
            Compass.Start(SensorSpeed.UI);
        }

        private void Compass_DataUpdated(object sender, CompassChangedEventArgs e)
        {
            Degrees = e.Reading.HeadingMagneticNorth;
        }

        public Command StopCommand { get; }

        void Stop()
        {
            if (!Compass.IsMonitoring)
                return;

            Compass.ReadingChanged -= Compass_DataUpdated;
            Compass.Stop();
        }
    }
}