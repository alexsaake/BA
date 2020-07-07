using Sensors_Client.Model;
using Xamarin.Forms;

namespace Sensors_Client.Pages
{
    public partial class GeolocationPage : ContentPage
    {
        public GeolocationPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((GeolocationModel)BindingContext).StartCommand.Execute(null);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((GeolocationModel)BindingContext).StopCommand.Execute(null);
        }
    }
}