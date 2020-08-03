using Client_ML_Gesture_Sensors.Commands;
using Client_ML_Gesture_Sensors.Models;
using Client_ML_Gesture_Sensors.Tizen.Wearable.lib.Services;
using Client_ML_Gesture_Sensors.ViewModels;
using System;

namespace Client_ML_Gesture_Sensors.Tizen.Wearable.lib.ViewModels
{
    public class LightSensorViewModel : BaseViewModel
    {
        LightSensorService lightSensorService;

        private LightSensor lightSensor;

        public LightSensor LightSensor
        {
            get { return lightSensor; }
            set { lightSensor = value; OnPropertyChanged(); }
        }

        public LightSensorViewModel()
        {
            lightSensorService = new LightSensorService();
            LoadData();
            startCommand = new RelayCommand(Start);
            stopCommand = new RelayCommand(Stop);
        }

        private void LoadData()
        {
            lightSensor = lightSensorService.Get();
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
                lightSensorService.Subscribe();
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
                lightSensorService.Unsubscribe();
            }
            catch (Exception ex)
            {

            }
        }
    }
}