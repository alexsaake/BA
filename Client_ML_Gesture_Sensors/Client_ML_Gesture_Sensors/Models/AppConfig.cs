using System;
using System.IO;

using Newtonsoft.Json;

using Xamarin.Essentials;

namespace Client_ML_Gesture_Sensors.Models
{
    class AppConfig : BaseModel
    {
        public static AppConfig AppConfiguration { get; } = new AppConfig();

        private string LocalFileName;
        private string LocalPath;

        private string serverURI;

        public string ServerURI
        {
            get { return serverURI; }
            set { serverURI = value; OnPropertyChanged(); }
        }

        private AppConfig()
        {
            LocalFileName = "config.json";
            LocalPath = Path.Combine(FileSystem.AppDataDirectory, LocalFileName);
        }

        public void LoadFromSystemFile()
        {
            if(File.Exists(LocalPath))
            {
                string json = File.ReadAllText(LocalPath);
                AppConfig config = JsonConvert.DeserializeObject<AppConfig>(json);
                if (config != null)
                {
                    ServerURI = config.ServerURI;
                }
                else
                {
                    ServerURI = "http://192.168.178.30:5000/";
                }
            }
        }

        public void SaveToSystemFile()
        {
            string json = JsonConvert.SerializeObject(AppConfiguration);
            File.WriteAllText(LocalPath, json);
        }
    }
}