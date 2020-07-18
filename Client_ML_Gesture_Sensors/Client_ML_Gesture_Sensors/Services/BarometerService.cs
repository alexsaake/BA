using Xamarin.Essentials;

using Client_ML_Gesture_Sensors.Models;

namespace Client_ML_Gesture_Sensors.Services
{
    public class BarometerService
    {
        private Models.Barometer Barometer;

        public void Subscribe(Models.Barometer barometer)
        {
            if (Xamarin.Essentials.Barometer.IsMonitoring)
                return;

            Xamarin.Essentials.Barometer.ReadingChanged += Barometer_DataUpdated;
            Xamarin.Essentials.Barometer.Start(SensorSpeed.UI);

            Barometer = barometer;
        }

        public void Unsubscribe(Models.Barometer barometer)
        {
            if (!Xamarin.Essentials.Barometer.IsMonitoring)
                return;

            Xamarin.Essentials.Barometer.ReadingChanged -= Barometer_DataUpdated;
            Xamarin.Essentials.Barometer.Stop();

            Barometer = null;
        }

        private void Barometer_DataUpdated(object sender, BarometerChangedEventArgs e)
        {
            Barometer.Pressure = e.Reading.PressureInHectopascals;
        }
    }
}
