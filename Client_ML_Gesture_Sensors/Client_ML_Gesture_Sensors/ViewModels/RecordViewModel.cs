using System;
using System.Windows.Input;

using Xamarin.Forms;

using Client_ML_Gesture_Sensors.Models;
using Client_ML_Gesture_Sensors.Renderers;
using Client_ML_Gesture_Sensors.Services;

namespace Client_ML_Gesture_Sensors.ViewModels
{
    public class RecordViewModel : BaseViewModel
    {
        private GestureService GestureService;

        private Gesture Gesture;

        public GraphRenderer Renderer { get; set; }

        private VibrationService VibrationService;

        private int valuesPerSecond;

        public int ValuesPerSecond
        {
            get { return valuesPerSecond; }
            set
            {
                if (value > 0)
                {
                    valuesPerSecond = value;
                    OnPropertyChanged();
                    GestureService.ValuesPerSecond = ValuesPerSecond;
                    GestureService.ValuesMax = ValuesPerSecond * CollectForSeconds;
                    Renderer.ValuesMax = ValuesPerSecond * CollectForSeconds;
                }
                else
                {
                    ValuesPerSecond = 5;
                }
            }
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
                    GestureService.ValuesMax = ValuesPerSecond * CollectForSeconds;
                    Renderer.ValuesMax = ValuesPerSecond * CollectForSeconds;
                }
                else
                {
                    CollectForSeconds = 12;
                }
            }
        }

        public RecordViewModel()
        {
            GestureService = new GestureService();
            Gesture = GestureService.Gesture;

            Renderer = new GraphRenderer();
            Renderer.Gesture = Gesture;

            valuesPerSecond = 5;
            ValuesPerSecond = 5;
            CollectForSeconds = 12;
            StartAfterSeconds = 3;

            VibrationService = new VibrationService();

            StartCommand = new Command(Start);
            StopCommand = new Command(Stop);
            StartCollectCommand = new Command(StartCollect);

            StartStopText = "Start Collecting";
            StartStopBackgroundColor = Color.Default;
            StartStopIsEnabled = true;
        }

        public ICommand StartCommand { get; }

        public void Start()
        {
            try
            {
                GestureService.SubscribeSensors();
            }
            catch (Exception ex)
            {

            }
        }

        public ICommand StopCommand { get; }

        void Stop()
        {
            try
            {
                GestureService.UnsubscribeSensors();
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
                if (value >= 0)
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

        public ICommand StartCollectCommand { get; }

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
                if(!GestureService.IsRecording)
                {
                    GestureService.StartRecordingValuesMax();
                    VibrationService.Vibrate(500);
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

                VibrationService.Vibrate(300);
                VibrationService.Vibrate(300);
            }

            return !StartStopIsEnabled;
        }

    }
}