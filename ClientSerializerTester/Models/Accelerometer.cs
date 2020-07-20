using System.Text.Json.Serialization;

namespace ClientSerializerTester.Models
{
    public class Accelerometer : BaseModel
    {
        public Accelerometer()
        {
            x = 1;
            y = 2;
            z = 3;
        }

        private float x;

        [JsonPropertyName("x")]
        public float X
        {
            get { return x; }
            set { x = value; OnPropertyChanged(); }
        }

        private float y;

        [JsonPropertyName("y")]
        public float Y
        {
            get { return y; }
            set { y = value; OnPropertyChanged(); }
        }

        private float z;

        [JsonPropertyName("z")]
        public float Z
        {
            get { return z; }
            set { z = value; OnPropertyChanged(); }
        }
    }
}
