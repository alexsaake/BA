using Android.App;
using Android.Content;
using Android.Hardware;
using System;

namespace Client_ML_Gesture_Sensors.Droid.Services
{
    class HeartRateMonitorService : Java.Lang.Object, ISensorEventListener
    {
        private SensorManager sensorManager;
        private Sensor _heartRateMonitor;
        private Models.HeartRateMonitor heartRateMonitor;

        public HeartRateMonitorService()
        {
            heartRateMonitor = new Models.HeartRateMonitor();
        }

        public Models.HeartRateMonitor Get()
        {
            return heartRateMonitor;
        }

        public void Subscribe()
        {
            //Subscribe to the update event and start the sensor
            this.sensorManager = Application.Context.GetSystemService(Context.SensorService) as SensorManager;
            if (this.sensorManager == null)
            {
                throw new NotSupportedException("Sensor Manager not supported");
            }

            this._heartRateMonitor = this.sensorManager.GetDefaultSensor(SensorType.HeartRate);

            this.sensorManager.RegisterListener(this, this._heartRateMonitor, SensorDelay.Ui);
        }

        public void Unsubscribe()
        {
            //Unsubscribe from the update event and stop the sensor
            sensorManager.UnregisterListener(this);
        }

        public void OnSensorChanged(SensorEvent e)
        {
            //Update the heartrate value of the current object
            if (e.Sensor.Type != SensorType.HeartRate)
                return;
            heartRateMonitor.HeartRate = (int)e.Values[0];
        }

        public void OnAccuracyChanged(Sensor sensor, SensorStatus accuracy)
        {
        }
    }
}