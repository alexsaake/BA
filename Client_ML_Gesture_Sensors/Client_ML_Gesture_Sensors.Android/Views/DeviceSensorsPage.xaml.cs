using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Client_ML_Gesture_Sensors.Views;

namespace Client_ML_Gesture_Sensors.Droid.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeviceSensorsPage : ContentPage
    {
        public DeviceSensorsPage()
        {
            InitializeComponent();
        }

        async void OnAccelerometerClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AccelerometerPage());
        }

        async void OnBarometerClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BarometerPage());
        }

        async void OnGeolocationClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GeolocationPage());
        }

        async void OnGyroscopeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GyroscopePage());
        }
    }
}