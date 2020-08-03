using Client_ML_Gesture_Sensors.Tizen.Wearable.lib.Views;
using Xamarin.Forms;

namespace Client_ML_Gesture_Sensors.Tizen.Wearable
{
    public class App : Application
    {
        public App()
        {
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
