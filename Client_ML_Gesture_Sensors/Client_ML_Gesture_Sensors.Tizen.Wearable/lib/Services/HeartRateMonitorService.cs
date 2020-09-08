using Tizen.Security;
using Sensor = Tizen.Sensor;

namespace Client_ML_Gesture_Sensors.Tizen.Wearable.lib.Services
{
    class HeartRateMonitorService
    {
        private Models.HeartRateMonitor heartRateMonitor;

        public HeartRateMonitorService()
        {
            heartRateMonitor = new Models.HeartRateMonitor();
        }

        public Models.HeartRateMonitor Get()
        {
            return heartRateMonitor;
        }

        private Sensor.HeartRateMonitor _monitor;

        public void Subscribe()
        {
            //Subscribe to the update event and start the sensor
            if (!Sensor.HeartRateMonitor.IsSupported || !CheckPrivileges())
                return;

            _monitor = new Sensor.HeartRateMonitor();
            _monitor.Interval = 1000;
            _monitor.DataUpdated += Pulsometer_DataUpdated;
            _monitor.Start();
        }

        public void Unsubscribe()
        {
            //Unsubscribe from the update event and stop the sensor
            if (!CheckPrivileges() || (!Sensor.HeartRateMonitor.IsSupported && !_monitor.IsSensing))
                return;

            _monitor.DataUpdated -= Pulsometer_DataUpdated;
            _monitor.Stop();
            _monitor.Dispose();
        }
        private bool CheckPrivileges()
        {
            string privilege = "http://tizen.org/privilege/healthinfo";
            CheckResult result = PrivacyPrivilegeManager.CheckPermission(privilege);
            bool decision = false;

            if (result == CheckResult.Allow)
            {
                decision = true;
            }
            else
            {
                PrivacyPrivilegeManager.GetResponseContext(privilege).TryGetTarget(out var context);
                if (context != null)
                {
                    context.ResponseFetched += (sender, e) =>
                    {
                        if (e.cause == CallCause.Answer && e.result == RequestResult.AllowForever)
                        {
                            decision = true;
                        }
                    };
                }
            }
            return decision;
        }

        private void Pulsometer_DataUpdated(object sender, Sensor.HeartRateMonitorDataUpdatedEventArgs e)
        {
            //Update the heartrate value of the current object
            heartRateMonitor.HeartRate = e.HeartRate;
        }
    }
}