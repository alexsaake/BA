using System;
using System.Windows.Input;

using Xamarin.Forms;

using Client_ML_Gesture_Sensors.Models;
using Client_ML_Gesture_Sensors.Droid.Services;
using Client_ML_Gesture_Sensors.ViewModels;

namespace Client_ML_Gesture_Sensors.Droid.ViewModels
{
    public class HeartRateMonitorViewModel : BaseViewModel
    {
        private HeartRateMonitorService heartRateMonitorService;

        private HeartRateMonitor heartRateMonitor;

        public HeartRateMonitor HeartRateMonitor
        {
            get { return heartRateMonitor; }
            set { heartRateMonitor = value; OnPropertyChanged(); }
        }

        public HeartRateMonitorViewModel()
        {
            heartRateMonitorService = new HeartRateMonitorService();
            LoadData();
            StartCommand = new Command(Start);
            StopCommand = new Command(Stop);
        }

        private void LoadData()
        {
            HeartRateMonitor = heartRateMonitorService.Get();
        }

        public ICommand StartCommand { get; }

        public void Start()
        {
            try
            {
                heartRateMonitorService.Subscribe();
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
                heartRateMonitorService.Unsubscribe();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}