using System;

using Xamarin.Forms;

using Client_ML_Gesture_Sensors.Views;

namespace Client_ML_Gesture_Sensors.Tizen.Wearable.lib.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void OnGesturesClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GesturePage());
        }

        async void OnRecordClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RecordPage());
        }

        async void OnDeviceSensorsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DeviceSensorsPage());
        }

        async void OnConfigurationClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ConfigurationPage());
        }
    }
}