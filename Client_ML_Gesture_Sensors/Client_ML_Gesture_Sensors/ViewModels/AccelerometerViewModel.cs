using Client_ML_Gesture_Sensors.Commands;
using Client_ML_Gesture_Sensors.Models;
using Client_ML_Gesture_Sensors.Services;
using System;

namespace Client_ML_Gesture_Sensors.ViewModels
{
    public class AccelerometerViewModel : BaseViewModel
    {
        AccelerometerService accelerometerService;

        private Accelerometer accelerometer;

        public Accelerometer Accelerometer
        {
            get { return accelerometer; }
            set { accelerometer = value; OnPropertyChanged(); }
        }

        public AccelerometerViewModel()
        {
            accelerometerService = new AccelerometerService();
            LoadData();
            startCommand = new RelayCommand(Start);
            stopCommand = new RelayCommand(Stop);
        }

        private void LoadData()
        {
            Accelerometer = accelerometerService.Get();
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
                accelerometerService.Subscribe();
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
                accelerometerService.Unsubscribe();
            }
            catch (Exception ex)
            {

            }
        }
    }
}