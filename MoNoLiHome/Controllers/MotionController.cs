using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoNoLiHome.Model.Request;
using MoNoLiHome.Network.Service;

namespace MoNoLiHome.Controllers
{
    [Route("api/[controller]")]
    public class MotionController : Controller
    {
        readonly ILogger<MotionController> _logger;
        readonly IArrivedHomeService _arrivedHomeService;
        readonly IMotionService _motionService;

        public MotionController(IArrivedHomeService arrivedHomeService, IMotionService motionService, ILogger<MotionController> logger)
        {
            _logger = logger;
            _arrivedHomeService = arrivedHomeService;
            _motionService = motionService;
        }

        [HttpGet]
        public JsonResult Get()
        {
            return Json(new
            {
                Message = "Motion"
            });
        }

        [HttpPost]
        [Route("detected")]
        public async Task<JsonResult> PostMotionDetected([FromBody] MotionDetectedMessage message)
        {
            bool result = false;
            var amIHome = await _arrivedHomeService.AmIHomeAsync();
            if (!amIHome)
                result = await _motionService.DetectedAsync();

            return Json(new 
            {
                Response = result,
                MessageSent = true
            });
        }
    }
}
