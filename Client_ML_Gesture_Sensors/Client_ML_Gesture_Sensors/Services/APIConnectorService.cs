using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Newtonsoft.Json;

using Client_ML_Gesture_Sensors.Models;

namespace Client_ML_Gesture_Sensors.Services
{
    class APIConnectorService
    {
        private HttpClient Client;

        private bool IsQuerying;

        public Gesture Gesture { get; set; }

        private int queryEachSeconds;

        public int QueryEachSeconds
        {
            set { queryEachSeconds = value; }
        }

        public APIConnectorService()
        {
            Client = new HttpClient();

            IsQuerying = false;
        }

        public void StartQueryResult()
        {
            IsQuerying = true;
            Device.StartTimer(TimeSpan.FromSeconds(queryEachSeconds), OnTimerTick);
        }
        private bool OnTimerTick()
        {
            _ = GetPredictionResult();
            return IsQuerying;
        }

        public void StopQueryResult()
        {
            IsQuerying = false;
        }

        private async Task GetPredictionResult()
        {
            string requestStr = "http://192.168.178.30:5000/api/";
            var requestURI = new Uri(string.Format(requestStr, string.Empty));

            HttpResponseMessage response = await Client.GetAsync(requestURI);

            if (response.IsSuccessStatusCode)
            {
                Gesture.Prediction = await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<bool> Save(Gesture _gesture)
        {
            if (_gesture.GesturePointList.Count == 20)
            {
                string jsonString = JsonConvert.SerializeObject(_gesture, Formatting.Indented);

                StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await Client.PostAsync("save", content);

                return response.IsSuccessStatusCode;
            }
            else
            {
                return false;
            }
        }
    }
}
