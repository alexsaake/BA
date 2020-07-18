using System;

using Client_ML_Gesture_Sensors.Models;
using Client_ML_Gesture_Sensors.Commands;
using Client_ML_Gesture_Sensors.Tizen.Wearable.lib.Services;
using Client_ML_Gesture_Sensors.ViewModels;

namespace Client_ML_Gesture_Sensors.Tizen.Wearable.lib.ViewModels
{
    public class HeartRateMonitorViewModel : BaseViewModel
    {
        HeartRateMonitorService HeartRateMonitorService;
        public HeartRateMonitorViewModel()
        {
            HeartRateMonitorService = new HeartRateMonitorService();
            HeartRateMonitorData = new HeartRateMonitor();
            startCommand = new RelayCommand(Start);
            stopCommand = new RelayCommand(Stop);
        }

        private HeartRateMonitor heartRateMonitorData;

        public HeartRateMonitor HeartRateMonitorData
        {
            get { return heartRateMonitorData; }
            set { heartRateMonitorData = value; OnPropertyChanged(); }
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
                HeartRateMonitorService.Subscribe(HeartRateMonitorData);
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
                HeartRateMonitorService.Unsubscribe(HeartRateMonitorData);
            }
            catch (Exception ex)
            {

            }
        }
    }
}