using Sensor = Tizen.Sensor;

using Client_ML_Gesture_Sensors.Models;

namespace Client_ML_Gesture_Sensors.Tizen.Wearable.lib.Services
{
    class LightSensorService
    {
        private Models.LightSensor LightSensor;
        private Sensor.LightSensor _sensor;

        public void Subscribe(Models.LightSensor lightSensor)
        {
            if (!Sensor.LightSensor.IsSupported)
                return;

            _sensor = new Sensor.LightSensor();
            _sensor.Interval = 1000;

            _sensor.DataUpdated += LightSensor_DataUpdated;
            _sensor.Start();

            LightSensor = lightSensor;
        }

        public void Unsubscribe(Models.LightSensor lightSensor)
        {
            if (!Sensor.LightSensor.IsSupported && !_sensor.IsSensing)
                return;

            _sensor.DataUpdated -= LightSensor_DataUpdated;
            _sensor.Stop();
            _sensor.Dispose();

            LightSensor = null;
        }

        private void LightSensor_DataUpdated(object sender, Sensor.LightSensorDataUpdatedEventArgs e)
        {
            LightSensor.Level = e.Level;
        }
    }
}
