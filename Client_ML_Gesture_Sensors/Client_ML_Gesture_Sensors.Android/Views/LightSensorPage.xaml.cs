using Client_ML_Gesture_Sensors.Droid.ViewModels;

using Xamarin.Forms;

namespace Client_ML_Gesture_Sensors.Droid.Views
{
    public partial class LightSensorPage : ContentPage
    {
        public LightSensorPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((LightSensorViewModel)BindingContext).StartCommand.Execute(null);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((LightSensorViewModel)BindingContext).StopCommand.Execute(null);
        }
    }
}