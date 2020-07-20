using Sensor = Tizen.Sensor;

using Client_ML_Gesture_Sensors.Models;

namespace Client_ML_Gesture_Sensors.Tizen.Wearable.lib.Services
{
    class LightSensorService
    {
        private static Models.LightSensor lightSensor;

        public LightSensorService()
        {
            lightSensor = new Models.LightSensor();
        }

        public Models.LightSensor Get()
        {
            return lightSensor;
        }

        private Sensor.LightSensor _sensor;

        public void Subscribe()
        {
            if (!Sensor.LightSensor.IsSupported)
                return;

            _sensor = new Sensor.LightSensor();
            _sensor.Interval = 1000;

            _sensor.DataUpdated += LightSensor_DataUpdated;
            _sensor.Start();
        }

        public void Unsubscribe()
        {
            if (!Sensor.LightSensor.IsSupported && !_sensor.IsSensing)
                return;

            _sensor.DataUpdated -= LightSensor_DataUpdated;
            _sensor.Stop();
        }

        private void LightSensor_DataUpdated(object sender, Sensor.LightSensorDataUpdatedEventArgs e)
        {
            lightSensor.Level = e.Level;
        }
    }
}
