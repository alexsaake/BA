using Xamarin.Forms;

using Client_ML_Gesture_Sensors.Droid.Views;

namespace Client_ML_Gesture_Sensors.Droid
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
