using System;
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

        public string ServerURI { get;  set; }

        public APIConnectorService()
        {
            Client = new HttpClient();

            IsQuerying = false;
        }

        public void StartQueryResult()
        {
            Client.BaseAddress = new Uri(ServerURI);
            IsQuerying = true;
            Device.StartTimer(TimeSpan.FromSeconds(queryEachSeconds), OnTimerTick);
        }
        private bool OnTimerTick()
        {
            _ = PostPredict();
            return IsQuerying;
        }

        public void StopQueryResult()
        {
            IsQuerying = false;
        }

        private async Task PostPredict()
        {
            string jsonString = JsonConvert.SerializeObject(Gesture, Formatting.Indented);

            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await Client.PostAsync("predict/", content);

            if (response.IsSuccessStatusCode) 
            {
                Gesture.Activity = await response.Content.ReadAsStringAsync();
            }
        }

        public async Task PutSave()
        {
            Client.BaseAddress = new Uri(ServerURI);

            string jsonString = JsonConvert.SerializeObject(Gesture, Formatting.Indented);

            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await Client.PutAsync("save/", content);
        }
    }
}
