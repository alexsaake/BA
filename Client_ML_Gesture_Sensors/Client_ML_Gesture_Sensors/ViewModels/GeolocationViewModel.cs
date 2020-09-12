using System;
using System.Windows.Input;

using Xamarin.Forms;

using Client_ML_Gesture_Sensors.Models;
using Client_ML_Gesture_Sensors.Services;

namespace Client_ML_Gesture_Sensors.ViewModels
{
    public class GeolocationViewModel : BaseViewModel
    {
        private GeolocationService geolocationService;

        private Geolocation geolocation;

        public Geolocation Geolocation
        {
            get { return geolocation; }
            set { geolocation = value; OnPropertyChanged(); }
        }

        public GeolocationViewModel()
        {
            geolocationService = new GeolocationService();
            LoadData();
            StartCommand = new Command(Start);
            StopCommand = new Command(Stop);
        }

        private void LoadData()
        {
            Geolocation = geolocationService.Get();
        }

        public ICommand StartCommand { get; }

        public void Start()
        {
            try
            {
                geolocationService.Subscribe();
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
                geolocationService.Unsubscribe();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}