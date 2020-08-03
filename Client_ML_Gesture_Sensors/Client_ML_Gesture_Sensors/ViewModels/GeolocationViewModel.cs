using Client_ML_Gesture_Sensors.Commands;
using Client_ML_Gesture_Sensors.Models;
using Client_ML_Gesture_Sensors.Services;
using System;

namespace Client_ML_Gesture_Sensors.ViewModels
{
    public class GeolocationViewModel : BaseViewModel
    {
        GeolocationService geolocationService;

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
            startCommand = new RelayCommand(Start);
            stopCommand = new RelayCommand(Stop);
        }

        private void LoadData()
        {
            Geolocation = geolocationService.Get();
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
                geolocationService.Subscribe();
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
                geolocationService.Unsubscribe();
            }
            catch (Exception ex)
            {

            }
        }
    }
}