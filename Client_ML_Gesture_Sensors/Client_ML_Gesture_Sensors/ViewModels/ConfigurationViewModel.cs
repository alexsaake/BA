using Client_ML_Gesture_Sensors.Models;

namespace Client_ML_Gesture_Sensors.ViewModels
{
    class ConfigurationViewModel : BaseViewModel
    {
        private string serverURI;

        public string ServerURI
        {
            get { return serverURI; }
            set { serverURI = value; OnPropertyChanged(); }
        }

        public ConfigurationViewModel()
        {
            ServerURI = AppConfig.AppConfiguration.ServerURI;
        }

    }
}
