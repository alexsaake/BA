using System.Text.Json.Serialization;

namespace ClientSerializerTester.Models
{
    public class Gyroscope : BaseModel
    {
        public Gyroscope()
        {
            x = 20;
            y = 21;
            z = 22;
        }

        private float x;

        [JsonPropertyName("x")]
        public float X
        {
            get { return x; }
            set
            {
                x = value;
                OnPropertyChanged();
            }
        }

        private float y;

        [JsonPropertyName("y")]
        public float Y
        {
            get { return y; }
            set
            {
                y = value;
                OnPropertyChanged();
            }
        }

        private float z;

        [JsonPropertyName("z")]
        public float Z
        {
            get { return z; }
            set
            {
                z = value;
                OnPropertyChanged();
            }
        }
    }
}
