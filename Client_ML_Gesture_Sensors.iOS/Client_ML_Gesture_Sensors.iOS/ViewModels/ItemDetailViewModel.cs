using System;

using Client_ML_Gesture_Sensors.iOS.Models;

namespace Client_ML_Gesture_Sensors.iOS.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.Text;
            Item = item;
        }
    }
}
