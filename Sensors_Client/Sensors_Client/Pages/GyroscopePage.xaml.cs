using Sensors_Client.Model;
using Xamarin.Forms;

namespace Sensors_Client.Pages
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
            ((GyroscopeModel)BindingContext).StopCommand.Execute(null);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((GyroscopeModel)BindingContext).StopCommand.Execute(null);
        }
    }
}