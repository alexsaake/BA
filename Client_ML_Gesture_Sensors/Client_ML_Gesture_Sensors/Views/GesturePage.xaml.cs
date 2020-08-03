using Client_ML_Gesture_Sensors.ViewModels;
using Xamarin.Forms;

namespace Client_ML_Gesture_Sensors.Views
{
    public partial class GesturePage : ContentPage
    {
        public GesturePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((GestureViewModel)BindingContext).StartCommand.Execute(null);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((GestureViewModel)BindingContext).StopCommand.Execute(null);
        }
    }
}