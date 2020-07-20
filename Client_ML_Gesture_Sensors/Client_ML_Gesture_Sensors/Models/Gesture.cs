using System.Collections.Generic;

namespace Client_ML_Gesture_Sensors.Models
{
    public class Gesture : BaseModel
    {
        private string label;

        public string Label
        {
            get { return label; }
            set
            {
                label = value;
                OnPropertyChanged();
            }
        }

        private bool canRecord;

        public bool CanRecord
        {
            get { return canRecord; }
            set
            {
                canRecord = value;
                OnPropertyChanged();
            }
        }

        private bool isAvailable;

        public bool IsAvailable
        {
            get { return isAvailable; }
            set
            {
                isAvailable = value;
                OnPropertyChanged();
            }
        }

        private bool isNotSaved;

        public bool IsNotSaved
        {
            get { return isNotSaved; }
            set
            {
                isNotSaved = value;
                OnPropertyChanged();
            }
        }

        private List<GesturePoint> gesturePointList;

        public List<GesturePoint> GesturePointList
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
            CanRecord = true;
            IsAvailable = false;
            IsNotSaved = false;

            GesturePointList = new List<GesturePoint>();
        }
    }
}