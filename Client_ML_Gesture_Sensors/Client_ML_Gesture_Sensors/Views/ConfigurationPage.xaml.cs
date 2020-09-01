using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Client_ML_Gesture_Sensors.Models;

namespace Client_ML_Gesture_Sensors.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfigurationPage : ContentPage
    {
        public ConfigurationPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            AppConfig.AppConfiguration.LoadFromSystemFile();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            AppConfig.AppConfiguration.SaveToSystemFile();
        }
    }
}