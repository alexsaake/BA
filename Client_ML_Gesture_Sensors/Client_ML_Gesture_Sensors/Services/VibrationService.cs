using Xamarin.Essentials;

namespace Client_ML_Gesture_Sensors.Services
{
    public class VibrationService
    {
        public void Vibrate(double duration)
        {
            Vibration.Vibrate(duration);
        }
    }
}
