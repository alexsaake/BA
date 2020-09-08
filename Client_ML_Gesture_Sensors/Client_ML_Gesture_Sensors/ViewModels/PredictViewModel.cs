using System;
using System.Windows.Input;

using Xamarin.Forms;

using Client_ML_Gesture_Sensors.Models;
using Client_ML_Gesture_Sensors.Renderers;
using Client_ML_Gesture_Sensors.Services;

namespace Client_ML_Gesture_Sensors.ViewModels
{
    public class PredictViewModel : BaseViewModel
    {
        private GestureService GestureService;

        private Gesture gesture;

        public Gesture Gesture
        {
            get { return gesture; }
            set { gesture = value; OnPropertyChanged(); }
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
                    GestureService.ValuesMax = ValuesPerSecond * BufferForSeconds;
                    Renderer.ValuesMax = ValuesPerSecond * BufferForSeconds;
                }
                else
                {
                    ValuesPerSecond = 50;
                }
            }
        }

        private int bufferForSeconds;

        public int BufferForSeconds
        {
            get { return bufferForSeconds; }
            set
            {
                if (value > 0)
                {
                    bufferForSeconds = value;
                    OnPropertyChanged();
                    GestureService.ValuesMax = ValuesPerSecond * BufferForSeconds;
                    Renderer.ValuesMax = ValuesPerSecond * BufferForSeconds;
                }
                else
                {
                    BufferForSeconds = 12;
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

        public PredictViewModel()
        {
            GestureService = new GestureService();
            Gesture = GestureService.Gesture;

            Renderer = new GraphRenderer();
            Renderer.Gesture = Gesture;

            ValuesPerSecond = 50;
            BufferForSeconds = 12;

            APIConnectorService = new APIConnectorService();
            APIConnectorService.Gesture = Gesture;
            ServerURI = "http://192.168.178.30:5000/api/adl/";
            APIConnectorService.ServerURI = ServerURI;

            QueryEachSeconds = 5;

            StartCommand = new Command(Start);
            StopCommand = new Command(Stop);
            ChangeConfigurationCommand = new Command(ChangeConfiguration);

            ChangeConfigurationText = "Change Configuration";
            ChangeConfigurationIsEnabled = false;
        }

        public ICommand StartCommand { get; }

        public void Start()
        {
            try
            {
                GestureService.SubscribeSensors();
                GestureService.StartRecordingContinuously();
                APIConnectorService.StartQueryResult();
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
                APIConnectorService.StopQueryResult();
                GestureService.StopRecording();
                GestureService.UnsubscribeSensors();
            }
            catch (Exception ex)
            {

            }
        }

        private int queryEachSeconds;

        public int QueryEachSeconds
        {
            get { return queryEachSeconds; }
            set
            {
                queryEachSeconds = value;
                OnPropertyChanged();
                APIConnectorService.QueryEachSeconds = QueryEachSeconds;
            }
        }

        private string changeConfigurationText;

        public string ChangeConfigurationText
        {
            get { return changeConfigurationText; }
            set { changeConfigurationText = value; OnPropertyChanged(); }
        }

        private bool changeConfigurationIsEnabled;

        public bool ChangeConfigurationIsEnabled
        {
            get { return changeConfigurationIsEnabled; }
            set { changeConfigurationIsEnabled = value; OnPropertyChanged(); }
        }

        public ICommand ChangeConfigurationCommand { get; }

        void ChangeConfiguration()
        {
            if(!ChangeConfigurationIsEnabled)
            {
                ChangeConfigurationText = "Resume Tracking";
                ChangeConfigurationIsEnabled = true;

                GestureService.StopRecording();
                APIConnectorService.StopQueryResult();
            }
            else
            {
                ChangeConfigurationText = "Change Configuration";
                ChangeConfigurationIsEnabled = false;

                GestureService.StartRecordingContinuously();
                APIConnectorService.StartQueryResult();
            }
        }
    }
}