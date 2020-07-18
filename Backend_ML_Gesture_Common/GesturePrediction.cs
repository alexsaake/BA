using Microsoft.ML.Data;

namespace Backend_ML_Gesture_Common
{
    public class GesturePrediction
    {
        [ColumnName("Score")]
        public float PredictedQuality;
    }
}
