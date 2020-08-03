using Client_ML_Gesture_Sensors.ViewModels;
using Xamarin.Forms;

namespace Client_ML_Gesture_Sensors.Views
{
    public partial class GyroscopePage : ContentPage
    {
        public GyroscopePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((GyroscopeViewModel)BindingContext).StartCommand.Execute(null);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((GyroscopeViewModel)BindingContext).StopCommand.Execute(null);
        }
    }
}