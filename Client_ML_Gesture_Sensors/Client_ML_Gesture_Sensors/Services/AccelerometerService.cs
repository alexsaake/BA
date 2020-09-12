using Xamarin.Essentials;

namespace Client_ML_Gesture_Sensors.Services
{
    public class AccelerometerService
    {
        private static Models.Accelerometer accelerometer;

        private const double ConstantG = 9.80665;
        private readonly float MultiplyBy = 1;

        public AccelerometerService()
        {
            accelerometer = new Models.Accelerometer();

            if(DeviceInfo.Platform == DevicePlatform.Android)
            {
                MultiplyBy = (float)ConstantG;
            }
        }

        public Models.Accelerometer Get()
        {
            return accelerometer;
        }

        public void Subscribe()
        {
            //Subscribe to the update event and start the sensor
            if (Xamarin.Essentials.Accelerometer.IsMonitoring)
                return;

            Xamarin.Essentials.Accelerometer.ReadingChanged += Accelerometer_DataUpdated;
            Xamarin.Essentials.Accelerometer.Start(SensorSpeed.UI);
        }

        public void Unsubscribe()
        {
            //Unsubscribe from the update event and stop the sensor
            if (!Xamarin.Essentials.Accelerometer.IsMonitoring)
                return;

            Xamarin.Essentials.Accelerometer.ReadingChanged -= Accelerometer_DataUpdated;
            Xamarin.Essentials.Accelerometer.Stop();
        }

        private void Accelerometer_DataUpdated(object sender, AccelerometerChangedEventArgs e)
        {
            //Update the X, Y, Z values of the current object
            accelerometer.X = e.Reading.Acceleration.X * MultiplyBy;
            accelerometer.Y = e.Reading.Acceleration.Y * MultiplyBy;
            accelerometer.Z = e.Reading.Acceleration.Z * MultiplyBy;
        }
    }
}
