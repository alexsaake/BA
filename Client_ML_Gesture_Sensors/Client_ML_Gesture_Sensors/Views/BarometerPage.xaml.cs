using Client_ML_Gesture_Sensors.ViewModels;
using Xamarin.Forms;

namespace Client_ML_Gesture_Sensors.Views
{
    public partial class BarometerPage : ContentPage
    {
        public BarometerPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((BarometerViewModel)BindingContext).StartCommand.Execute(null);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((BarometerViewModel)BindingContext).StopCommand.Execute(null);
        }
    }
}