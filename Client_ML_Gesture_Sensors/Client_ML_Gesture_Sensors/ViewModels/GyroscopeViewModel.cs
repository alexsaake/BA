using Client_ML_Gesture_Sensors.Commands;
using Client_ML_Gesture_Sensors.Models;
using Client_ML_Gesture_Sensors.Services;
using System;

namespace Client_ML_Gesture_Sensors.ViewModels
{
    public class GyroscopeViewModel : BaseViewModel
    {
        GyroscopeService gyroscopeService;

        private Gyroscope gyroscope;

        public Gyroscope Gyroscope
        {
            get { return gyroscope; }
            set { gyroscope = value; OnPropertyChanged(); }
        }

        public GyroscopeViewModel()
        {
            gyroscopeService = new GyroscopeService();
            LoadData();
            startCommand = new RelayCommand(Start);
            stopCommand = new RelayCommand(Stop);
        }

        private void LoadData()
        {
            Gyroscope = gyroscopeService.Get();
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
                gyroscopeService.Subscribe();
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
                gyroscopeService.Unsubscribe();
            }
            catch (Exception ex)
            {

            }
        }
    }
}