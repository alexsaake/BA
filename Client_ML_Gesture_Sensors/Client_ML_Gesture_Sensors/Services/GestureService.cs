using System;
using System.Threading.Tasks;

using Xamarin.Forms;

using Client_ML_Gesture_Sensors.Models;
using Client_ML_Gesture_Sensors.Renderers;

namespace Client_ML_Gesture_Sensors.Services
{
    public class GestureService
    {
        private AccelerometerService AccelerometerService;
        private GyroscopeService GyroscopeService;

        public Gesture Gesture { get; set; }

        private bool isRecording;

        public bool IsRecording
        {
            get { return isRecording; }
        }

        private int valuesMax;

        public int ValuesMax
        {
            set { valuesMax = value; }
        }

        private int valuesPerSecond;

        public int ValuesPerSecond
        {
            set { valuesPerSecond = value; }
        }

        public GestureService()
        {
            Gesture = new Gesture();

            AccelerometerService = new AccelerometerService();
            GyroscopeService = new GyroscopeService();

            isRecording = false;
        }

        public void SubscribeSensors()
        {
            AccelerometerService.Subscribe();
            GyroscopeService.Subscribe();
        }

        public void UnsubscribeSensors()
        {
            AccelerometerService.Unsubscribe();
            GyroscopeService.Unsubscribe();
        }

        private void AddPoint()
        {
            GesturePoint gesturePoint = new GesturePoint(
                AccelerometerService.Get(),
                GyroscopeService.Get());

            Gesture.GesturePointList.Add(gesturePoint);

            while (Gesture.GesturePointList.Count > valuesMax)
            {
                Gesture.GesturePointList.RemoveAt(0);
            }
        }

        public async void StartRecordingContinuously()
        {
            if (!isRecording)
            {
                isRecording = true;
                while(isRecording)
                {
                    AddPoint();
                    await Task.Delay(TimeSpan.FromMilliseconds(1000 / valuesPerSecond));
                }
            }
        }

        public async void StartRecordingValuesMax()
        {
            if (!isRecording)
            {
                Gesture.GesturePointList.Clear();
                isRecording = true;
                while(Gesture.GesturePointList.Count < valuesMax)
                {
                    AddPoint();
                    await Task.Delay(TimeSpan.FromMilliseconds(1000 / valuesPerSecond));
                }
                isRecording = false;
            }
        }

        public void StopRecording()
        {
            isRecording = false;
        }
    }
}
