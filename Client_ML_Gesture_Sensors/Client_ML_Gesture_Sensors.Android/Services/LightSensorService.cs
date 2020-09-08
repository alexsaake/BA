using Android.Content;
using Android.Hardware;
using Java.Interop;
using System;

namespace Client_ML_Gesture_Sensors.Droid.Services
{
    class LightSensorService : ISensorEventListener
    {
        private SensorManager sensorManager;
		private Sensor _lightSensor;
        private Models.LightSensor lightSensor;

        public IntPtr Handle => throw new NotImplementedException();

        public int JniIdentityHashCode => throw new NotImplementedException();

        public JniObjectReference PeerReference => throw new NotImplementedException();

        public JniPeerMembers JniPeerMembers => throw new NotImplementedException();

        public JniManagedPeerStates JniManagedPeerState => throw new NotImplementedException();

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
            sensorManager = (SensorManager)Android.App.Application.Context.GetSystemService(Context.SensorService);
            _lightSensor = sensorManager.GetDefaultSensor(SensorType.Light);
            sensorManager.RegisterListener(this, (Sensor)_lightSensor, SensorDelay.Ui);
        }

        public void Unsubscribe()
        {
            sensorManager.UnregisterListener(this);
        }

        public void OnSensorChanged(SensorEvent e)
        {
            if (e.Sensor.Type != SensorType.Light)
                return;
            lightSensor.Level = e.Values[0];
        }

        public void OnAccuracyChanged(Sensor sensor, SensorStatus accuracy)
        {
        }

        public void SetJniIdentityHashCode(int value)
        {
        }

        public void SetPeerReference(JniObjectReference reference)
        {
        }

        public void SetJniManagedPeerState(JniManagedPeerStates value)
        {
        }

        public void UnregisterFromRuntime()
        {
        }

        public void DisposeUnlessReferenced()
        {
        }

        public void Disposed()
        {
        }

        public void Finalized()
        {
        }

        public void Dispose()
        {
        }
    }
}
