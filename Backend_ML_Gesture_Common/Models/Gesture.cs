using System.Collections.Generic;

namespace Backend_ML_Gesture_Common.Models
{
    public class Gesture
    {
        private string label;

        public string Label
        {
            get { return label; }
            set
            {
                label = value;
            }
        }

        private bool canRecord;

        public bool CanRecord
        {
            get { return canRecord; }
            set
            {
                canRecord = value;
            }
        }

        private bool isAvailable;

        public bool IsAvailable
        {
            get { return isAvailable; }
            set
            {
                isAvailable = value;
            }
        }

        private bool isNotSaved;

        public bool IsNotSaved
        {
            get { return isNotSaved; }
            set
            {
                isNotSaved = value;
            }
        }

        private List<GesturePoint> gesturePointList;

        public List<GesturePoint> GesturePointList
        {
            get { return gesturePointList; }
            set
            {
                gesturePointList = value;
            }
        }

        public Gesture()
        {
            CanRecord = true;
            IsAvailable = false;
            IsNotSaved = true;

            GesturePointList = new List<GesturePoint>();
        }
    }
}