using Client_ML_Gesture_Sensors.Droid.ViewModels;

using Xamarin.Forms;

namespace Client_ML_Gesture_Sensors.Droid.Views
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