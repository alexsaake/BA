using System;
using System.Windows.Input;

using Xamarin.Forms;

using Client_ML_Gesture_Sensors.Models;
using Client_ML_Gesture_Sensors.Services;

namespace Client_ML_Gesture_Sensors.ViewModels
{
    public class BarometerViewModel : BaseViewModel
    {
        private BarometerService barometerService;

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
            StartCommand = new Command(Start);
            StopCommand = new Command(Stop);
        }

        private void LoadData()
        {
            Barometer = barometerService.Get();
        }

        public ICommand StartCommand { get; }

        public void Start()
        {
            try
            {
                barometerService.Subscribe();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ICommand StopCommand { get; }

        void Stop()
        {
            try
            {
                barometerService.Unsubscribe();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}