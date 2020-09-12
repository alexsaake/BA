using System;
using System.Net.Http;
using System.Net.Http.Headers;
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
            IsQuerying = false;
        }

        public void StartQueryResult()
        {
            Client = new HttpClient()
            {
                BaseAddress = new Uri(ServerURI),
                Timeout = TimeSpan.FromSeconds(10)
            };
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "");
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
            try
            {
                string jsonString = JsonConvert.SerializeObject(Gesture, Formatting.Indented);

                StringContent outContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await Client.PostAsync("predict/", outContent);

                if (response.StatusCode == System.Net.HttpStatusCode.OK) 
                {
                    HttpContent inContent = response.Content;
                    Gesture.Activity = await inContent.ReadAsStringAsync();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task PutSave()
        {
            try
            {
                Client = new HttpClient()
                {
                    BaseAddress = new Uri(ServerURI),
                    Timeout = TimeSpan.FromSeconds(10)
                };
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "");

                string jsonString = JsonConvert.SerializeObject(Gesture, Formatting.Indented);

                StringContent outContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await Client.PutAsync("save/", outContent);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
