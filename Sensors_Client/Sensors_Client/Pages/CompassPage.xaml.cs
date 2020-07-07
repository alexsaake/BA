using Sensors_Client.Model;
using Xamarin.Forms;

namespace Sensors_Client.Pages
{
    public partial class CompassPage : ContentPage
    {
        public CompassPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((CompassModel)BindingContext).StartCommand.Execute(null);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((CompassModel)BindingContext).StopCommand.Execute(null);
        }
    }
}