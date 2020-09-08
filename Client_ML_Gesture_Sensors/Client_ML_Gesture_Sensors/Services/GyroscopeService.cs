using Xamarin.Essentials;

namespace Client_ML_Gesture_Sensors.Services
{
    public class GyroscopeService
    {
        private static Models.Gyroscope gyroscope;

        private const double Constant = 180 / System.Math.PI;
        private float MultiplyBy = 1;

        public GyroscopeService()
        {
            gyroscope = new Models.Gyroscope();

            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                MultiplyBy = (float)Constant;
            }
        }

        public Models.Gyroscope Get()
        {
            return gyroscope;
        }

        public void Subscribe()
        {
            if (Xamarin.Essentials.Gyroscope.IsMonitoring)
                return;

            Xamarin.Essentials.Gyroscope.ReadingChanged += Gyroscope_DataUpdated;
            Xamarin.Essentials.Gyroscope.Start(SensorSpeed.UI);
        }

        public void Unsubscribe()
        {
            if (!Xamarin.Essentials.Gyroscope.IsMonitoring)
                return;

            Xamarin.Essentials.Gyroscope.ReadingChanged -= Gyroscope_DataUpdated;
            Xamarin.Essentials.Gyroscope.Stop();
        }

        private void Gyroscope_DataUpdated(object sender, GyroscopeChangedEventArgs e)
        {
            gyroscope.X = e.Reading.AngularVelocity.X * MultiplyBy;
            gyroscope.Y = e.Reading.AngularVelocity.Y * MultiplyBy;
            gyroscope.Z = e.Reading.AngularVelocity.Z * MultiplyBy;
        }
    }
}