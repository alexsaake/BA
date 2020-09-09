using Xamarin.Forms;
using Xamarin.Essentials;

using Client_ML_Gesture_Sensors.ViewModels;

namespace Client_ML_Gesture_Sensors.Views
{
    public partial class PredictPage : ContentPage
    {
        public PredictPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((PredictViewModel)BindingContext).StartCommand.Execute(null);
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                DeviceDisplay.KeepScreenOn = true;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((PredictViewModel)BindingContext).StopCommand.Execute(null);
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                DeviceDisplay.KeepScreenOn = false;
            }
        }
    }
}