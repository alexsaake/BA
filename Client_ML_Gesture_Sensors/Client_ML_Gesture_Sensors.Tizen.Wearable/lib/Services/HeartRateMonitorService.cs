using Tizen.Security;
using Sensor = Tizen.Sensor;

namespace Client_ML_Gesture_Sensors.Tizen.Wearable.lib.Services
{
    class HeartRateMonitorService
    {
        private static Models.HeartRateMonitor heartRateMonitor;

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
            if (!Sensor.HeartRateMonitor.IsSupported || !CheckPrivileges())
                return;

            _monitor = new Sensor.HeartRateMonitor();
            _monitor.Interval = 1000;
            _monitor.DataUpdated += Pulsometer_DataUpdated;
            _monitor.Start();
        }

        public void Unsubscribe()
        {
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
            heartRateMonitor.HeartRate = e.HeartRate;
        }
    }
}