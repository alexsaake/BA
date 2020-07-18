using System;

using Client_ML_Gesture_Sensors.Models;
using Client_ML_Gesture_Sensors.Commands;
using Client_ML_Gesture_Sensors.Services;

namespace Client_ML_Gesture_Sensors.ViewModels
{
    public class GeolocationViewModel : BaseViewModel
    {
        GeolocationService GeolocationService;
        public GeolocationViewModel()
        {
            GeolocationService = new GeolocationService();
            GeolocationData = new Geolocation();
            startCommand = new RelayCommand(Start);
        }

        private Geolocation geolocationData;

        public Geolocation GeolocationData
        {
            get { return geolocationData; }
            set { geolocationData = value; OnPropertyChanged(); }
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
                _ = GeolocationService.GetGeolocationAsync(GeolocationData);
            }
            catch (Exception ex)
            {

            }
        }
    }
}