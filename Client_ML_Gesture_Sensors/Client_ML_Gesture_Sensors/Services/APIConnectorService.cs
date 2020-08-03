using Client_ML_Gesture_Sensors.Models;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client_ML_Gesture_Sensors.Services
{
    class APIConnectorService
    {
        private HttpClient _client;

        public APIConnectorService()
        {
            _client = new HttpClient();

            _client.BaseAddress = new System.Uri("http://localhost");
        }

        public async Task<string> GetClusteringResult(Gesture _gesture)
        {
            string jsonString = JsonConvert.SerializeObject(_gesture, Formatting.Indented);

            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync("clustering", content);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string clustering = await response.Content.ReadAsStringAsync();

                return clustering;
            }
            else
            {
                return response.StatusCode.ToString();
            }
        }

        public async Task<bool> Save(Gesture _gesture)
        {
            if (_gesture.GesturePointList.Count == 20)
            {
                string jsonString = JsonConvert.SerializeObject(_gesture, Formatting.Indented);

                StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PostAsync("save", content);

                return response.IsSuccessStatusCode;
            }
            else
            {
                return false;
            }
        }
    }
}
