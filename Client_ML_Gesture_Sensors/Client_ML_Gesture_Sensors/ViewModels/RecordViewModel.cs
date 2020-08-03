using System;

using Xamarin.Forms;

using Client_ML_Gesture_Sensors.Commands;
using Client_ML_Gesture_Sensors.Models;
using Client_ML_Gesture_Sensors.Renderers;
using Client_ML_Gesture_Sensors.Services;

namespace Client_ML_Gesture_Sensors.ViewModels
{
    public class RecordViewModel : BaseViewModel
    {
        private GestureService gestureService;

        private Gesture gesture;

        public GraphRenderer Renderer { get; set; }

        private VibrationService vibrationService;

        private bool isRecording;

        private int valuesPerSecond;

        public int ValuesPerSecond
        {
            get { return valuesPerSecond; }
            set { valuesPerSecond = value; OnPropertyChanged(); }
        }

        private int collectForSeconds;

        public int CollectForSeconds
        {
            get { return collectForSeconds; }
            set
            {
                if (value > 0)
                {
                    collectForSeconds = value;
                    OnPropertyChanged();
                }
                else
                {
                    CollectForSeconds = 12;
                }
            }
        }

        public RecordViewModel()
        {
            gestureService = new GestureService();
            gesture = gestureService.Get();

            startCommand = new RelayCommand(Start);
            stopCommand = new RelayCommand(Stop);

            Renderer = new Renderers.GraphRenderer();

            ValuesPerSecond = 5;
            CollectForSeconds = 12;
            StartAfterSeconds = 3;

            isRecording = false;

            StartStopText = "Start Collecting";
            StartStopBackgroundColor = Color.Default;
            startCollectCommand = new RelayCommand(StartCollect);

            StartStopIsEnabled = true;

            vibrationService = new VibrationService();
        }

        private RelayCommand startCommand;

        public RelayCommand StartCommand
        {
            get { return startCommand; }
        }

        public void Start()
        {
            try
            {
                gestureService.SubscribeAll();
            }
            catch (Exception ex)
            {

            }
        }

        private RelayCommand stopCommand;

        public RelayCommand StopCommand
        {
            get { return stopCommand; }
        }

        void Stop()
        {
            try
            {
                gestureService.UnsubscribeAll();

                isRecording = false;
            }
            catch (Exception ex)
            {

            }
        }

        private int startAfterSeconds;

        public int StartAfterSeconds
        {
            get { return startAfterSeconds; }
            set
            {
                if (value > 0)
                {
                    startAfterSeconds = value;
                    OnPropertyChanged();
                }
                else
                {
                    StartAfterSeconds = 3;
                }
            }
        }

        private string startStopText;

        public string StartStopText
        {
            get { return startStopText; }
            set { startStopText = value; OnPropertyChanged(); }
        }

        private Color startStopBackgroundColor;

        public Color StartStopBackgroundColor
        {
            get { return startStopBackgroundColor; }
            set { startStopBackgroundColor = value; OnPropertyChanged(); }
        }

        private bool startStopIsEnabled;

        public bool StartStopIsEnabled
        {
            get { return startStopIsEnabled; }
            set { startStopIsEnabled = value; OnPropertyChanged(); }
        }


        private int startSecondsRemaining;
        private int collectSecondsRemaining;

        private RelayCommand startCollectCommand;

        public RelayCommand StartCollectCommand
        {
            get { return startCollectCommand; }
        }

        void StartCollect()
        {
            try
            {
                if (StartStopIsEnabled)
                {
                    StartStopIsEnabled = false;
                    Device.StartTimer(TimeSpan.FromSeconds(1), OnTimerTickCollect);
                    startSecondsRemaining = StartAfterSeconds;
                    collectSecondsRemaining = CollectForSeconds;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private bool OnTimerTick()
        {
            gestureService.AddPoint(ValuesPerSecond * CollectForSeconds);

            Renderer.DrawGraph(gesture, ValuesPerSecond * CollectForSeconds);

            return isRecording;
        }

        private bool OnTimerTickCollect()
        {
            if (startSecondsRemaining > 0)
            {
                StartStopText = startSecondsRemaining.ToString();
                StartStopBackgroundColor = Color.Red;
                startSecondsRemaining--;
            }
            else if (collectSecondsRemaining > 0)
            {
                if(!isRecording)
                {
                    isRecording = true;
                    Device.StartTimer(TimeSpan.FromMilliseconds(1000 / ValuesPerSecond), OnTimerTick);

                    vibrationService.Vibrate(500);
                }
                StartStopText = collectSecondsRemaining.ToString();
                StartStopBackgroundColor = Color.Green;
                collectSecondsRemaining--;
            }
            else
            {
                StartStopText = "Start Collecting";
                StartStopBackgroundColor = Color.Default;
                StartStopIsEnabled = true;
                isRecording = false;

                gestureService.ToString();

                vibrationService.Vibrate(500);
                vibrationService.Vibrate(500);
            }

            return !StartStopIsEnabled;
        }

    }
}