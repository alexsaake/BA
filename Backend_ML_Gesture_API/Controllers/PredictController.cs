using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using Backend_ML_Gesture_Common;

namespace Backend_ML_Gesture_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PredictController : ControllerBase
    {
        private readonly string _modelPath;
        private MLContext _context;

        public PredictController()
        {
            _context = new MLContext();
            _modelPath = "./gesture.zip";
        }

        [HttpPost]
        public async Task<float> Post([FromBody] GestureData gestureData)
        {
            ITransformer model;
            DataViewSchema schema;

            if(!System.IO.File.Exists(_modelPath))
            {
                //erstelle gestureData.zip aus ML.NET 
            }

            using (var stream = System.IO.File.OpenRead(_modelPath))
            {
                model = _context.Model.Load(stream, out schema);
            }

            var predictionEngine = _context.Model.CreatePredictionEngine<GestureData, GesturePrediction>(model);

            var prediction = predictionEngine.Predict(gestureData);

            return prediction.PredictedQuality;
        }
    }
}
