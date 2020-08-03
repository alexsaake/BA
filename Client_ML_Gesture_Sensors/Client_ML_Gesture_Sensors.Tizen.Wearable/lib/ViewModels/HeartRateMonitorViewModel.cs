using Client_ML_Gesture_Sensors.Commands;
using Client_ML_Gesture_Sensors.Models;
using Client_ML_Gesture_Sensors.Tizen.Wearable.lib.Services;
using Client_ML_Gesture_Sensors.ViewModels;
using System;

namespace Client_ML_Gesture_Sensors.Tizen.Wearable.lib.ViewModels
{
    public class HeartRateMonitorViewModel : BaseViewModel
    {
        HeartRateMonitorService heartRateMonitorService;

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
            startCommand = new RelayCommand(Start);
            stopCommand = new RelayCommand(Stop);
        }

        private void LoadData()
        {
            HeartRateMonitor = heartRateMonitorService.Get();
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
                heartRateMonitorService.Subscribe();
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
                heartRateMonitorService.Unsubscribe();
            }
            catch (Exception ex)
            {

            }
        }
    }
}