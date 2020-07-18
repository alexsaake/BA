using System;

using Client_ML_Gesture_Sensors.Models;
using Client_ML_Gesture_Sensors.Commands;
using Client_ML_Gesture_Sensors.Services;

namespace Client_ML_Gesture_Sensors.ViewModels
{
    public class GyroscopeViewModel : BaseViewModel
    {
        GyroscopeService GyroscopeService;
        public GyroscopeViewModel()
        {
            GyroscopeService = new GyroscopeService();
            GyroscopeData = new Gyroscope();
            startCommand = new RelayCommand(Start);
            stopCommand = new RelayCommand(Stop);
        }

        private Gyroscope gyroscopeData;

        public Gyroscope GyroscopeData
        {
            get { return gyroscopeData; }
            set { gyroscopeData = value; OnPropertyChanged(); }
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
                GyroscopeService.Subscribe(GyroscopeData);
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
                GyroscopeService.Unsubscribe(GyroscopeData);
            }
            catch (Exception ex)
            {

            }
        }
    }
}