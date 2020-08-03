using System;
using System.Collections.Generic;

namespace Client_ML_Gesture_Sensors.Models
{
    public class Gesture : BaseModel
    {
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
            GesturePointList = new List<GesturePoint>();
        }

        internal string ToCSV()
        {
            throw new NotImplementedException();
        }
    }
}