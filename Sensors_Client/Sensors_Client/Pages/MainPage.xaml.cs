using System;

using Sensors_Client.Pages;
using Sensors_Client.Model;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sensors_Client
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            Model = new REST_API();

            InitializeComponent();

            BindingContext = new REST_API();
        }

        public REST_API Model { get; private set; }

        async void OnAccelerometerClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AccelerometerPage());
        }

        async void OnBarometerClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BarometerPage());
        }

        async void OnCompassClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CompassPage());
        }

        async void OnGeolocationClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GeolocationPage());
        }

        async void OnGyroscopeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GyroscopePage());
        }

        async void OnMagnetometerClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MagnetometerPage());
        }

        async void OnOrientationSensorClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OrientationSensorPage());
        }
    }
}