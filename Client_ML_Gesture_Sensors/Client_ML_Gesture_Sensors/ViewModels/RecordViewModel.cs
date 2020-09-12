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

        private Gesture gesture;

        public Gesture Gesture
        {
            get { return gesture; }
            set
            {
                gesture = value;
                OnPropertyChanged();
            }
        }

        private APIConnectorService APIConnectorService;

        public GraphRenderer Renderer { get; set; }

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
                    Renderer.ValuesPerSecond = ValuesPerSecond;
                }
                else
                {
                    ValuesPerSecond = 50;
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
                    Renderer.CollectForSeconds = CollectForSeconds;
                }
                else
                {
                    CollectForSeconds = 12;
                }
            }
        }

        private string serverURI;

        public string ServerURI
        {
            get { return serverURI; }
            set
            {
                serverURI = value;
                OnPropertyChanged();
            }
        }

        public RecordViewModel()
        {
            GestureService = new GestureService();
            Gesture = GestureService.Gesture;

            Renderer = new GraphRenderer()
            {
                Gesture = Gesture
            };

            valuesPerSecond = 50;
            ValuesPerSecond = 50;
            CollectForSeconds = 12;
            StartAfterSeconds = 1;

            APIConnectorService = new APIConnectorService()
            {
                Gesture = Gesture
            };
            ServerURI = "http://192.168.178.30:55000/api/adl/";
            APIConnectorService.ServerURI = ServerURI;

            StartCommand = new Command(Start);
            StopCommand = new Command(Stop);
            StartCollectCommand = new Command(StartCollect);
            SendToServerCommand = new Command(SendToServer);

            StartStopText = "Start Recording";
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
                throw ex;
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
                throw ex;
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
                    StartAfterSeconds = 1;
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
                throw ex;
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
                }
                StartStopText = collectSecondsRemaining.ToString();
                StartStopBackgroundColor = Color.Green;
                collectSecondsRemaining--;
            }
            else
            {
                GestureService.StopRecording();
                StartStopText = "Start Recording";
                StartStopBackgroundColor = Color.Default;
                StartStopIsEnabled = true;
            }

            return !StartStopIsEnabled;
        }

        public ICommand SendToServerCommand { get; }

        void SendToServer()
        {
            try
            {
                _ = APIConnectorService.PutSave();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}