using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoNoLiHome.Network.Service;

namespace MoNoLiHome.Controllers
{
    [Route("api/[controller]")]
    public class MotionController : Controller
    {
        readonly IArrivedHomeService _arrivedHomeService;
        readonly ILogger<MotionController> _logger;

        public MotionController(IArrivedHomeService arrivedHomeService, ILogger<MotionController> logger)
        {
            _logger = logger;
            _arrivedHomeService = arrivedHomeService;
        }

        [HttpGet]
        public JsonResult Get()
        {
            return Json(new
            {
                Message = "Motion"
            });
        }


        [HttpGet]
        [Route("detected")]
        public async Task<JsonResult> GetMotionDetected()
        {
            var amIHome = await _arrivedHomeService.AmIHomeAsync();
            if (!amIHome)
            {
                
            }


            return Json(new 
            {
                Response = true,
                MessageSent = false
            });
        }
    }
}
