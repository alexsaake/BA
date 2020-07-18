using System.Text.Json.Serialization;

namespace Client_ML_Gesture_Sensors.Models
{
    public class Gesture
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("fixedAcidity")]
        public string FixedAcidity { get; set; }

        [JsonPropertyName("volatileAcidity")]
        public string VolatileAcidity { get; set; }

        [JsonPropertyName("citricAcid")]
        public string CitricAcid { get; set; }

        [JsonPropertyName("residualSugar")]
        public string ResidualSugar { get; set; }

        [JsonPropertyName("chlorides")]
        public string Chlorides { get; set; }

        [JsonPropertyName("freeSulfurDioxide")]
        public string FreeSulfurDioxide { get; set; }

        [JsonPropertyName("totalSulforDioxide")]
        public string TotalSulforDioxide { get; set; }

        [JsonPropertyName("density")]
        public string Density { get; set; }

        [JsonPropertyName("ph")]
        public string Ph { get; set; }

        [JsonPropertyName("sulphates")]
        public string Sulphates { get; set; }

        [JsonPropertyName("alcohol")]
        public string Alcohol { get; set; }

        [JsonPropertyName("quality")]
        public string Quality { get; set; }
    }
}