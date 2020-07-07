using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Sensors_Client.Model
{
    public class REST_API : INotifyPropertyChanged
    {
        public REST_API()
        {
            SaveSensorDataCommand = new Command(async () => await serviceCallAsync());
        }


        private string rest;


        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public Command SaveSensorDataCommand { get; }

        async Task serviceCallAsync()
        {
            String RestUrl = "http://api.androidhive.info/json/glide.json";
            var uri = new Uri(RestUrl);

            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();

            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                REST = await response.Content.ReadAsStringAsync();
            }
        }

        public string REST
        {
            get { return rest; }
            set
            {
                rest = value;
                OnPropertyChanged();
            }
        }
    }
}
