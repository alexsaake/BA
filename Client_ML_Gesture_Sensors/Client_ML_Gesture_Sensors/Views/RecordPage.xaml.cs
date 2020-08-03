using Client_ML_Gesture_Sensors.ViewModels;
using Xamarin.Forms;

namespace Client_ML_Gesture_Sensors.Views
{
    public partial class RecordPage : ContentPage
    {
        public RecordPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((RecordViewModel)BindingContext).StartCommand.Execute(null);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((RecordViewModel)BindingContext).StopCommand.Execute(null);
        }
    }
}