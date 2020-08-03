using System;
using System.IO;

using Xamarin.Essentials;

using Client_ML_Gesture_Sensors.Models;

namespace Client_ML_Gesture_Sensors.Services
{
    class FileSystemService
    {
        const string localFileName = "temp.csv";

        string localPath;

        public FileSystemService()
        {
            string localPath = Path.Combine(FileSystem.CacheDirectory, localFileName);
        }

        public void SaveToFileSystem(Gesture gesture)
        {
            File.WriteAllText(localPath, gesture.ToCSV());
        }

        public string LoadFromFileSystem(object sender, EventArgs e)
        {
            return File.ReadAllText(localPath);
        }
    }
}
