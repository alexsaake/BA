using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Client_ML_Gesture_Sensors.Models
{
    public class Gesture : BaseModel
    {
        private string prediction;

        public string Prediction
        {
            get { return prediction; }
            set
            {
                prediction = value;
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
        }
    }
}