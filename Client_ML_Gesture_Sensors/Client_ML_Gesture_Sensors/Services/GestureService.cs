using Client_ML_Gesture_Sensors.Models;

namespace Client_ML_Gesture_Sensors.Services
{
    public class GestureService
    {
        AccelerometerService AccelerometerService;
        GyroscopeService GyroscopeService;

        private static Gesture gesture;


        public GestureService()
        {
            gesture = new Gesture();

            AccelerometerService = new AccelerometerService();
            GyroscopeService = new GyroscopeService();
        }

        public void SubscribeAll()
        {
            AccelerometerService.Subscribe();
            GyroscopeService.Subscribe();
        }

        public void UnsubscribeAll()
        {
            AccelerometerService.Unsubscribe();
            GyroscopeService.Unsubscribe();
        }

        public Gesture Get()
        {
            return gesture;
        }

        public void AddPoint(int sampling)
        {
            GesturePoint gesturePoint = new GesturePoint(
                AccelerometerService.Get(),
                GyroscopeService.Get());

            while (gesture.GesturePointList.Count > sampling)
            {
                gesture.GesturePointList.RemoveAt(0);
            }

            gesture.GesturePointList.Add(gesturePoint);
        }
    }
}
