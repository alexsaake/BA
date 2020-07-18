//using System;
//using System.Net.Http;
//using System.Text;
//using System.Text.Json;
//using System.Threading.Tasks;

//using Client_ML_Gesture_Sensors.Models;

//namespace Client_ML_Gesture_Sensors.Services
//{
//    class PredictionService
//    {
//        private HttpClient _client;

//        public PredictionService()
//        {
//            _client = new HttpClient();

//            _client.BaseAddress = new Uri("http://192.168.178.30:52144/api/");
//        }

//        public async Task<string> Predict(Gesture gestureData)
//        {
//            var options = new JsonSerializerOptions
//            {
//                PropertyNameCaseInsensitive = true
//            };

//            var jsonString = JsonSerializer.Serialize(gestureData, options);

//            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

//            var response = await _client.PostAsync("model", content);

//            var prediction = await response.Content.ReadAsStringAsync();

//            return prediction;
//        }
//    }
//}
