using Android.App;
using Android.Content;
using Android.Hardware;
using System;

namespace Client_ML_Gesture_Sensors.Droid.Services
{
    public partial class LightSensorService : Java.Lang.Object, ISensorEventListener
    {
        private SensorManager sensorManager;
		private Sensor _lightSensor;
        private Models.LightSensor lightSensor;

        public LightSensorService()
        {
            lightSensor = new Models.LightSensor();
        }

        public Models.LightSensor Get()
        {
            return lightSensor;
        }

        public void Subscribe()
        {
            this.sensorManager = Application.Context.GetSystemService(Context.SensorService) as SensorManager;
            if (this.sensorManager == null)
            {
                throw new NotSupportedException("Sensor Manager not supported");
            }

            this._lightSensor = this.sensorManager.GetDefaultSensor(SensorType.Light);

            this.sensorManager.RegisterListener(this, this._lightSensor, SensorDelay.Ui);
        }

        public void Unsubscribe()
        {
            sensorManager.UnregisterListener(this);
        }

        void ISensorEventListener.OnSensorChanged(SensorEvent e)
        {
            if (e.Sensor.Type != SensorType.Light)
                return;
            lightSensor.Level = e.Values[0];
        }

        void ISensorEventListener.OnAccuracyChanged(Sensor sensor, SensorStatus accuracy)
        {
        }
    }
}
