using Sensors_Client.Model;
using Xamarin.Forms;

namespace Sensors_Client.Pages
{
    public partial class MagnetometerPage : ContentPage
    {
        public MagnetometerPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((MagnetometerModel)BindingContext).StartCommand.Execute(null);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((MagnetometerModel)BindingContext).StopCommand.Execute(null);
        }
    }
}