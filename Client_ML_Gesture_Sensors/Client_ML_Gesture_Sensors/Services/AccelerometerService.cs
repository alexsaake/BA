using Xamarin.Essentials;

using Client_ML_Gesture_Sensors.Models;

namespace Client_ML_Gesture_Sensors.Services
{
    public class AccelerometerService
    {
        private Models.Accelerometer Accelerometer;

        public void Subscribe(Models.Accelerometer accelerometer)
        {
            if (Xamarin.Essentials.Accelerometer.IsMonitoring)
                return;

            Xamarin.Essentials.Accelerometer.ReadingChanged += Accelerometer_DataUpdated;
            Xamarin.Essentials.Accelerometer.Start(SensorSpeed.UI);

            Accelerometer = accelerometer;
        }

        public void Unsubscribe(Models.Accelerometer accelerometer)
        {
            if (!Xamarin.Essentials.Accelerometer.IsMonitoring)
                return;

            Xamarin.Essentials.Accelerometer.ReadingChanged -= Accelerometer_DataUpdated;
            Xamarin.Essentials.Accelerometer.Stop();

            Accelerometer = null;
        }

        private void Accelerometer_DataUpdated(object sender, AccelerometerChangedEventArgs e)
        {
            Accelerometer.X = e.Reading.Acceleration.X;
            Accelerometer.Y = e.Reading.Acceleration.Y;
            Accelerometer.Z = e.Reading.Acceleration.Z;
        }
    }
}
