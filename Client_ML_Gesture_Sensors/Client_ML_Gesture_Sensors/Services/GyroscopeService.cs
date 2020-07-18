using Xamarin.Essentials;

using Client_ML_Gesture_Sensors.Models;

namespace Client_ML_Gesture_Sensors.Services
{
    class GyroscopeService
    {
        private Models.Gyroscope Gyroscope;

        public void Subscribe(Models.Gyroscope gyroscope)
        {
            if (Xamarin.Essentials.Gyroscope.IsMonitoring)
                return;

            Xamarin.Essentials.Gyroscope.ReadingChanged += Gyroscope_DataUpdated;
            Xamarin.Essentials.Gyroscope.Start(SensorSpeed.UI);

            Gyroscope = gyroscope;
        }

        public void Unsubscribe(Models.Gyroscope gyroscope)
        {
            if (!Xamarin.Essentials.Gyroscope.IsMonitoring)
                return;

            Xamarin.Essentials.Gyroscope.ReadingChanged -= Gyroscope_DataUpdated;
            Xamarin.Essentials.Gyroscope.Stop();

            Gyroscope = null;
        }

        private void Gyroscope_DataUpdated(object sender, GyroscopeChangedEventArgs e)
        {
            Gyroscope.X = e.Reading.AngularVelocity.X;
            Gyroscope.Y = e.Reading.AngularVelocity.Y;
            Gyroscope.Z = e.Reading.AngularVelocity.Z;
        }
    }
}
