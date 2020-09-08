using Android.Content;
using Android.Hardware;
using Java.Interop;
using System;

namespace Client_ML_Gesture_Sensors.Droid.Services
{
    class HeartRateMonitorService : ISensorEventListener
    {
        private SensorManager sensorManager;
        private Sensor _heartRateMonitor;
        private Models.HeartRateMonitor heartRateMonitor;

        public IntPtr Handle => throw new NotImplementedException();

        public int JniIdentityHashCode => throw new NotImplementedException();

        public JniObjectReference PeerReference => throw new NotImplementedException();

        public JniPeerMembers JniPeerMembers => throw new NotImplementedException();

        public JniManagedPeerStates JniManagedPeerState => throw new NotImplementedException();

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
            sensorManager = (SensorManager)Android.App.Application.Context.GetSystemService(Context.SensorService);
            _heartRateMonitor = sensorManager.GetDefaultSensor(SensorType.HeartRate);
            sensorManager.RegisterListener(this, (Sensor)_heartRateMonitor, SensorDelay.Ui);
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
            throw new NotImplementedException ();
        }

        public void SetJniIdentityHashCode(int value)
        {
            throw new NotImplementedException();
        }

        public void SetPeerReference(JniObjectReference reference)
        {
            throw new NotImplementedException();
        }

        public void SetJniManagedPeerState(JniManagedPeerStates value)
        {
            throw new NotImplementedException();
        }

        public void UnregisterFromRuntime()
        {
            throw new NotImplementedException();
        }

        public void DisposeUnlessReferenced()
        {
            throw new NotImplementedException();
        }

        public void Disposed()
        {
            throw new NotImplementedException();
        }

        public void Finalized()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}