using System;
using System.Threading.Tasks;

using Client_ML_Gesture_Sensors.Models;
using Client_ML_Gesture_Sensors.Services;
using Client_ML_Gesture_Sensors.Commands;

namespace Client_ML_Gesture_Sensors.ViewModels
{
    public class GestureViewModel : BaseViewModel
    {
        GestureService gestureService;

        private Gesture gesture;

        public Gesture Gesture
        {
            get { return gesture; }
            set { gesture = value; OnPropertyChanged(); }
        }

        public GestureViewModel()
        {
            gestureService = new GestureService();
            LoadData();
            startCommand = new RelayCommand(Start);
            stopCommand = new RelayCommand(Stop);
            recordCommand = new RelayCommand(async () => await RecordAsync());
            saveCommand = new RelayCommand(async () => await SaveAsync());
        }

        private void LoadData()
        {
            Gesture = gestureService.Get();
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
            }
            catch (Exception ex)
            {

            }
        }

        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged(); }
        }

        private RelayCommand recordCommand;

        public RelayCommand RecordCommand
        {
            get { return recordCommand; }
        }

        private async Task RecordAsync()
        {
            try
            {
                await gestureService.Record();
                LoadData();
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }

        private RelayCommand getClusteringCommand;

        public RelayCommand GetClusteringCommand
        {
            get { return getClusteringCommand; }
        }

        private async Task ClusteringCommandAsync()
        {
            try
            {
                Message = await gestureService.GetClusteringResult();
            }
            catch (Exception ex)
            {

            }
        }

        private string label;

        public string Label
        {
            get { return label; }
            set { label = value; OnPropertyChanged(); }
        }

        private RelayCommand saveCommand;

        public RelayCommand SaveCommand
        {
            get { return saveCommand; }
        }

        private async Task SaveAsync()
        {
            try
            {
                if(Label != "")
                {
                    Gesture.Label = Label;
                    await gestureService.SaveToBackend();
                    LoadData();
                }
                else
                {
                    Message = "Please enter a label";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
    }
}