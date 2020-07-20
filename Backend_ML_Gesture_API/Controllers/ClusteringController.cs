using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;

using Backend_ML_Gesture_Common.Models;

namespace Backend_ML_Gesture_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClusteringController : ControllerBase
    {
        private readonly string _modelPath;
        private MLContext _context;

        public ClusteringController()
        {
            _context = new MLContext();
            _modelPath = "./gesture.zip";
        }

        [HttpPost]
        public async Task<string> Post([FromBody] Gesture _gesture)
        {
            ITransformer model;
            DataViewSchema schema;

            using (var stream = System.IO.File.OpenRead(_modelPath))
            {
                model = _context.Model.Load(stream, out schema);
            }

            var predictionEngine = _context.Model.CreatePredictionEngine<Gesture, Clustering>(model);

            var clustering = predictionEngine.Predict(_gesture);

            return clustering.Label;
        }
    }
}
