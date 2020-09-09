using Client_ML_Gesture_Sensors.Tizen.Wearable.lib.Views;
using Xamarin.Forms;
using System.Runtime.InteropServices;

namespace Client_ML_Gesture_Sensors.Tizen.Wearable
{
    public class App : Application
    {
        enum Power_Type { CPU = 0, DISPLAY, DISPLAY_DIM };

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
            base.OnSleep();
            DevicePowerReleaseLock((int)Power_Type.DISPLAY);
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            base.OnResume();
            DevicePowerRequestLock((int)Power_Type.DISPLAY, 0);
        }

        [DllImport("libcapi-system-device.so.0", EntryPoint = "device_power_request_lock", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int DevicePowerRequestLock(int type, int timeout_ms);

        [DllImport("libcapi-system-device.so.0", EntryPoint = "device_power_release_lock", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int DevicePowerReleaseLock(int type);
    }
}
