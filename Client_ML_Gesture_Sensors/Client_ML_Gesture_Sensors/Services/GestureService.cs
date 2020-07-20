using System;
using System.Threading.Tasks;
using Xamarin.Forms;

using Client_ML_Gesture_Sensors.Models;

namespace Client_ML_Gesture_Sensors.Services
{
    public class GestureService : BaseModel
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

        public async Task<bool> Record()
        {
            if (!gesture.CanRecord)
                throw new ArgumentException("A gesture is already being recorded.");

            gesture = new Gesture();
            gesture.IsAvailable = false;
            gesture.CanRecord = false;
            gesture.IsNotSaved = false;
            Device.StartTimer(TimeSpan.FromMilliseconds(500), OnTimerTick);
            return true;
        }

        private bool OnTimerTick()
        {
            GesturePoint gesturePoint = new GesturePoint(
                AccelerometerService.Get(),
                GyroscopeService.Get());

            gesture.GesturePointList.Add(gesturePoint);

            while (gesture.GesturePointList.Count > 20)
            {
                gesture.GesturePointList.RemoveAt(0);
            }

            if (gesture.GesturePointList.Count == 20)
            {
                gesture.CanRecord = true;
                gesture.IsAvailable = true;
                gesture.IsNotSaved = true;
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<string> GetClusteringResult()
        {
            if (gesture.GesturePointList.Count == 20 && gesture.CanRecord)
            {
                APIConnectorService aPIConnectorService = new APIConnectorService();

                string clustering = await aPIConnectorService.GetClusteringResult(gesture);

                return "AAAOOOAAAOOOAAAOOO";
            }
            else
            {
                return "";
            }
        }

        public async Task<bool> SaveToBackend()
        {
            if (gesture.GesturePointList.Count == 20 && gesture.IsNotSaved && gesture.IsAvailable)
            {
                //APIConnectorService aPIConnectorService = new APIConnectorService();

                //bool IsSaved = await aPIConnectorService.Save(gesture);

                gesture.IsNotSaved = false;

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
