using Xamarin.Forms;

using Client_ML_Gesture_Sensors.Tizen.Wearable.lib.ViewModels;

namespace Client_ML_Gesture_Sensors.Tizen.Wearable.lib.Views
{
    public partial class HeartRateMonitorPage : ContentPage
    {
        public HeartRateMonitorPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((HeartRateMonitorViewModel)BindingContext).StartCommand.Execute(null);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((HeartRateMonitorViewModel)BindingContext).StopCommand.Execute(null);
        }
    }
}