using Client_ML_Gesture_Sensors.Commands;
using Client_ML_Gesture_Sensors.Models;
using Client_ML_Gesture_Sensors.Renderers;
using Client_ML_Gesture_Sensors.Services;
using System;
using Xamarin.Forms;

namespace Client_ML_Gesture_Sensors.ViewModels
{
    public class GestureViewModel : BaseViewModel
    {
        GestureService gestureService;

        Gesture gesture;

        APIConnectorService APIConnection;

        public GraphRenderer Renderer { get; set; }

        private bool isRecording;

        private int valuesPerSecond;

        public int ValuesPerSecond
        {
            get { return valuesPerSecond; }
            set { valuesPerSecond = value; OnPropertyChanged(); }
        }

        private int queryEachSeconds;

        public int QueryEachSeconds
        {
            get { return  queryEachSeconds; }
            set {  queryEachSeconds = value; OnPropertyChanged(); }
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

        public GestureViewModel()
        {
            gestureService = new GestureService();
            gesture = gestureService.Get();

            startCommand = new RelayCommand(Start);
            stopCommand = new RelayCommand(Stop);

            Renderer = new Renderers.GraphRenderer();

            ValuesPerSecond = 5;
            CollectForSeconds = 12;

            isRecording = false;

            APIConnection = new APIConnectorService();

            QueryEachSeconds = 5;
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

                isRecording = true;
                Device.StartTimer(TimeSpan.FromMilliseconds(1000 / ValuesPerSecond), OnTimerTick);
                Device.StartTimer(TimeSpan.FromSeconds(1), OnTimerTickCollect);
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

        private string queryResult;

        public string QueryResult
        {
            get { return queryResult; }
            set { queryResult = value; OnPropertyChanged(); }
        }


        private bool OnTimerTickCollect()
        {
            QueryResult = APIConnection.GetClusteringResult(gesture).ToString();

            return isRecording;
        }
    }
}