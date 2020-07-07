using Xamarin.Essentials;
using Xamarin.Forms;

namespace Sensors_Client.Model
{
    public class BarometerModel : BaseSensorModel
    {
        public BarometerModel()
        {
            StartCommand = new Command(Start);
            StopCommand = new Command(Stop);
        }

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

        public Command StartCommand { get; }

        void Start()
        {
            if (Barometer.IsMonitoring)
                return;

            Barometer.ReadingChanged += Barometer_DataUpdated;
            Barometer.Start(SensorSpeed.UI);
        }

        private void Barometer_DataUpdated(object sender, BarometerChangedEventArgs e)
        {
            Pressure = e.Reading.PressureInHectopascals;
        }

        public Command StopCommand { get; }

        void Stop()
        {
            if (!Barometer.IsMonitoring)
                return;

            Barometer.ReadingChanged -= Barometer_DataUpdated;
            Barometer.Stop();
        }
    }
}