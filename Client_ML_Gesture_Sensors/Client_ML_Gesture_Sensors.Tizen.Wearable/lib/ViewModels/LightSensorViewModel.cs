using System;

using Client_ML_Gesture_Sensors.Models;
using Client_ML_Gesture_Sensors.Commands;
using Client_ML_Gesture_Sensors.Tizen.Wearable.lib.Services;
using Client_ML_Gesture_Sensors.ViewModels;

namespace Client_ML_Gesture_Sensors.Tizen.Wearable.lib.ViewModels
{
    public class LightSensorViewModel : BaseViewModel
    {
        LightSensorService LightSensorService;
        public LightSensorViewModel()
        {
            LightSensorService = new LightSensorService();
            LightSensorData = new LightSensor();
            startCommand = new RelayCommand(Start);
            stopCommand = new RelayCommand(Stop);
        }

        private LightSensor lightSensorData;

        public LightSensor LightSensorData
        {
            get { return lightSensorData; }
            set { lightSensorData = value; OnPropertyChanged(); }
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
                LightSensorService.Subscribe(LightSensorData);
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
                LightSensorService.Unsubscribe(LightSensorData);
            }
            catch (Exception ex)
            {

            }
        }
    }
}