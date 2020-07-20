using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Backend_ML_Gesture_Common.Models;

namespace Backend_ML_Gesture_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaveController : ControllerBase
    {
        public SaveController()
        {

        }

        [HttpPost]
        public async Task<bool> Post([FromBody] Gesture _gesture)
        {
            return true;
        }
    }
}
