using System;
using System.Windows.Input;

using Xamarin.Forms;

using Client_ML_Gesture_Sensors.Models;
using Client_ML_Gesture_Sensors.Services;

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
            StartCommand = new Command(Start);
            StopCommand = new Command(Stop);
        }

        private void LoadData()
        {
            Gyroscope = gyroscopeService.Get();
        }

        public ICommand StartCommand { get; }

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

        public ICommand StopCommand { get; }

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