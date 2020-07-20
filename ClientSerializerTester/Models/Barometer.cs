using System.Text.Json.Serialization;
using System.Threading.Tasks.Dataflow;

namespace ClientSerializerTester.Models
{
    public class Barometer : BaseModel
    {
        public Barometer()
        {
            pressure = 10;
        }

        private double pressure;

        [JsonPropertyName("pressure")]
        public double Pressure
        {
            get { return pressure; }
            set
            {
                pressure = value;
                OnPropertyChanged();
            }
        }
    }
}
