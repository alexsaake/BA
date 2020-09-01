using System;
using System.Windows.Input;

using Xamarin.Forms;

using Client_ML_Gesture_Sensors.Models;
using Client_ML_Gesture_Sensors.Tizen.Wearable.lib.Services;
using Client_ML_Gesture_Sensors.ViewModels;

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
            StartCommand = new Command(Start);
            StopCommand = new Command(Stop);
        }

        private void LoadData()
        {
            lightSensor = lightSensorService.Get();
        }

        public ICommand StartCommand { get; }

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

        public ICommand StopCommand { get; }

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