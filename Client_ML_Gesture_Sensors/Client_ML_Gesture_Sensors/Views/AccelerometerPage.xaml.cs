using Client_ML_Gesture_Sensors.ViewModels;
using Xamarin.Forms;

namespace Client_ML_Gesture_Sensors.Views
{
    public partial class AccelerometerPage : ContentPage
    {
        public AccelerometerPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((AccelerometerViewModel)BindingContext).StartCommand.Execute(null);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((AccelerometerViewModel)BindingContext).StopCommand.Execute(null);
        }
    }
}