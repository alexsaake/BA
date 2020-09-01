using Xamarin.Forms;

using Client_ML_Gesture_Sensors.Models;
using Client_ML_Gesture_Sensors.ViewModels;

namespace Client_ML_Gesture_Sensors.Views
{
    public partial class GesturePage : ContentPage
    {
        public GesturePage()
        {
            AppConfig.AppConfiguration.LoadFromSystemFile();
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