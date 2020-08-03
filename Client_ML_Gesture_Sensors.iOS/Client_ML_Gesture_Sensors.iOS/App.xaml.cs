using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Client_ML_Gesture_Sensors.iOS.Services;
using Client_ML_Gesture_Sensors.iOS.Views;

namespace Client_ML_Gesture_Sensors.iOS
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
