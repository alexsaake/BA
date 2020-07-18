using System;

using Client_ML_Gesture_Sensors.Models;
using Client_ML_Gesture_Sensors.Commands;
using Client_ML_Gesture_Sensors.Services;

namespace Client_ML_Gesture_Sensors.ViewModels
{
    public class BarometerViewModel : BaseViewModel
    {
        BarometerService BarometerService;
        public BarometerViewModel()
        {
            BarometerService = new BarometerService();
            BarometerData = new Barometer();
            startCommand = new RelayCommand(Start);
            stopCommand = new RelayCommand(Stop);
        }

        private Barometer barometerData;

        public Barometer BarometerData
        {
            get { return barometerData; }
            set { barometerData = value; OnPropertyChanged(); }
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
                BarometerService.Subscribe(BarometerData);
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
                BarometerService.Unsubscribe(BarometerData);
            }
            catch (Exception ex)
            {

            }
        }
    }
}