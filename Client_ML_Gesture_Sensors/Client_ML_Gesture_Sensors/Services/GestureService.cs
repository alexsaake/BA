using System;

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

        public void StartRecordingContinuously()
        {
            if (!isRecording)
            {
                isRecording = true;
                Device.StartTimer(TimeSpan.FromMilliseconds(1000 / valuesPerSecond), OnTimerTickContinuously);
            }
        }

        private bool OnTimerTickContinuously()
        {
            AddPoint();
            return isRecording;
        }

        public void StartRecordingValuesMax()
        {
            if (!isRecording)
            {
                isRecording = true;
                Gesture.GesturePointList.Clear();
                Device.StartTimer(TimeSpan.FromMilliseconds(1000 / valuesPerSecond), OnTimerTickValuesMax);
            }
        }

        private bool OnTimerTickValuesMax()
        {
            if(Gesture.GesturePointList.Count < valuesMax)
            {
                AddPoint();
            }
            else
            {
                StopRecording();
            }
            return isRecording;
        }

        public void StopRecording()
        {
            isRecording = false;
        }
    }
}
