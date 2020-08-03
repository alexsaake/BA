using Xamarin.Essentials;

namespace Client_ML_Gesture_Sensors.Services
{
    public class AccelerometerService
    {
        private static Models.Accelerometer accelerometer;

        public AccelerometerService()
        {
            accelerometer = new Models.Accelerometer();
        }

        public Models.Accelerometer Get()
        {
            return accelerometer;
        }

        public void Subscribe()
        {
            if (Xamarin.Essentials.Accelerometer.IsMonitoring)
                return;

            Xamarin.Essentials.Accelerometer.ReadingChanged += Accelerometer_DataUpdated;
            Xamarin.Essentials.Accelerometer.Start(SensorSpeed.UI);
        }

        public void Unsubscribe()
        {
            if (!Xamarin.Essentials.Accelerometer.IsMonitoring)
                return;

            Xamarin.Essentials.Accelerometer.ReadingChanged -= Accelerometer_DataUpdated;
            Xamarin.Essentials.Accelerometer.Stop();
        }

        private void Accelerometer_DataUpdated(object sender, AccelerometerChangedEventArgs e)
        {
            accelerometer.X = e.Reading.Acceleration.X;
            accelerometer.Y = e.Reading.Acceleration.Y;
            accelerometer.Z = e.Reading.Acceleration.Z;
        }
    }
}
