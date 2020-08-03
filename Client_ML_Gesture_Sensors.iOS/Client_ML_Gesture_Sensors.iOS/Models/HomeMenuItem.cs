using System;
using System.Collections.Generic;
using System.Text;

namespace Client_ML_Gesture_Sensors.iOS.Models
{
    public enum MenuItemType
    {
        Browse,
        About
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
