using System;

using Client_ML_Gesture_Sensors.Models;
using Client_ML_Gesture_Sensors.Commands;
using Client_ML_Gesture_Sensors.Services;

namespace Client_ML_Gesture_Sensors.ViewModels
{
    public class AccelerometerViewModel : BaseViewModel
    {
        AccelerometerService AccelerometerService;
        public AccelerometerViewModel()
        {
            AccelerometerService = new AccelerometerService();
            AccelerometerData = new Accelerometer();
            startCommand = new RelayCommand(Start);
            stopCommand = new RelayCommand(Stop);
        }

        private Accelerometer accelerometerData;

        public Accelerometer AccelerometerData
        {
            get { return accelerometerData; }
            set { accelerometerData = value; OnPropertyChanged(); }
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
                AccelerometerService.Subscribe(AccelerometerData);
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
                AccelerometerService.Unsubscribe(AccelerometerData);
            }
            catch(Exception ex)
            {

            }
        }
    }
}