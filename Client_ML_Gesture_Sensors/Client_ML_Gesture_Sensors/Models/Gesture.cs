using System.Collections.ObjectModel;
using Xamarin.Essentials;

namespace Client_ML_Gesture_Sensors.Models
{
    public class Gesture : BaseModel
    {
        private string activity;

        public string Activity
        {
            get { return activity; }
            set
            {
                activity = value;
                OnPropertyChanged();
            }
        }

        private string wearableType;

        public string WearableType
        {
            get { return wearableType; }
            set
            {
                wearableType = value;
                OnPropertyChanged();
            }
        }

        private string username;

        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged();
            }
        }

        //wrist of the smartwatch: left = false, right = true
        private bool wornWrist;

        public bool WornWrist
        {
            get { return wornWrist; }
            set
            {
                wornWrist = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<GesturePoint> gesturePointList;

        public ObservableCollection<GesturePoint> GesturePointList
        {
            get { return gesturePointList; }
            set
            {
                gesturePointList = value;
                OnPropertyChanged();
            }
        }

        public Gesture()
        {
            GesturePointList = new ObservableCollection<GesturePoint>();
            WearableType = DeviceInfo.Manufacturer.ToString() + DeviceInfo.Model.ToString();
        }
    }
}