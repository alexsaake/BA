using Xamarin.Forms;

using Client_ML_Gesture_Sensors.Models;
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
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((PredictViewModel)BindingContext).StopCommand.Execute(null);
        }
    }
}