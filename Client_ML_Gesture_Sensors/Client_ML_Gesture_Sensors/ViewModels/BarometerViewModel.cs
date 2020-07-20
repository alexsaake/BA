using System;

using Client_ML_Gesture_Sensors.Models;
using Client_ML_Gesture_Sensors.Services;
using Client_ML_Gesture_Sensors.Commands;

namespace Client_ML_Gesture_Sensors.ViewModels
{
    public class BarometerViewModel : BaseViewModel
    {
        BarometerService barometerService;

        private Barometer barometer;

        public Barometer Barometer
        {
            get { return barometer; }
            set { barometer = value; OnPropertyChanged(); }
        }

        public BarometerViewModel()
        {
            barometerService = new BarometerService();
            LoadData();
            startCommand = new RelayCommand(Start);
            stopCommand = new RelayCommand(Stop);
        }

        private void LoadData()
        {
            Barometer = barometerService.Get();
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
                barometerService.Subscribe();
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
                barometerService.Unsubscribe();
            }
            catch (Exception ex)
            {

            }
        }
    }
}