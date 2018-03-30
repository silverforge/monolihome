using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoNoLiHome.Model;
using MoNoLiHome.Network.Service;

namespace MoNoLiHome.Controllers
{
    [Route("api/[controller]")]
    public class IAmHomeController : Controller
    {
        readonly IArrivedHomeService _arrivedHomeService;
        readonly ILogger<IAmHomeController> _logger;

        public IAmHomeController(IArrivedHomeService arrivedHomeService, ILogger<IAmHomeController> logger)
        {
            _logger = logger;
            _arrivedHomeService = arrivedHomeService;
        }


        [HttpGet]
        public async Task<JsonResult> Get()
        {
            var result = await _arrivedHomeService.AmIHomeAsync();
            _logger.LogDebug($" ::: IAmHomeController.Get result ::: {result}");

            return Json(new
            {
                Answer = result
            });
        }

        [HttpPost]
        public async Task<JsonResult> Post([FromBody] IAmHomeMessage message)
        {
            var result = await _arrivedHomeService.IAmHomeAsync(message.Toggle);
            _logger.LogDebug($" ::: IAmHomeController.Post result ::: {result} :: with set ::: {message.Toggle}");

            return Json(new 
            {
                Set = result
            });
        }
    }
}
