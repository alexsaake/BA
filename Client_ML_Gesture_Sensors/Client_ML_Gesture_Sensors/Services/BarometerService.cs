using Xamarin.Essentials;

namespace Client_ML_Gesture_Sensors.Services
{
    public class BarometerService
    {
        private static Models.Barometer barometer;

        public BarometerService()
        {
            barometer = new Models.Barometer();
        }

        public Models.Barometer Get()
        {
            return barometer;
        }

        public void Subscribe()
        {
            if (Xamarin.Essentials.Barometer.IsMonitoring)
                return;

            Xamarin.Essentials.Barometer.ReadingChanged += Barometer_DataUpdated;
            Xamarin.Essentials.Barometer.Start(SensorSpeed.UI);
        }

        public void Unsubscribe()
        {
            if (!Xamarin.Essentials.Barometer.IsMonitoring)
                return;

            Xamarin.Essentials.Barometer.ReadingChanged -= Barometer_DataUpdated;
            Xamarin.Essentials.Barometer.Stop();
        }

        private void Barometer_DataUpdated(object sender, BarometerChangedEventArgs e)
        {
            barometer.Pressure = e.Reading.PressureInHectopascals;
        }
    }
}
