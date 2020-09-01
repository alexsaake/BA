using System;
using System.Windows.Input;

using Xamarin.Forms;

using Client_ML_Gesture_Sensors.Models;
using Client_ML_Gesture_Sensors.Services;

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
            StartCommand = new Command(Start);
            StopCommand = new Command(Stop);
        }

        private void LoadData()
        {
            Accelerometer = accelerometerService.Get();
        }

        public ICommand StartCommand { get; }

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

        public ICommand StopCommand { get; }

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