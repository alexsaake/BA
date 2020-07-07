using Sensors_Client.Model;
using Xamarin.Forms;

namespace Sensors_Client.Pages
{
    public partial class OrientationSensorPage : ContentPage
    {
        public OrientationSensorPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((OrientationSensorModel)BindingContext).StartCommand.Execute(null);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((OrientationSensorModel)BindingContext).StopCommand.Execute(null);
        }
    }
}