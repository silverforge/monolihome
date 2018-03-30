using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoNoLiHome.Model;
using MoNoLiHome.Network.Service;

namespace MoNoLiHome.Controllers
{
    [Route("api/[controller]")]
    public class IAmHomeController : Controller
    {
        readonly IArrivedHomeService _arrivedHomeService;

        public IAmHomeController(IArrivedHomeService arrivedHomeService)
        {
            _arrivedHomeService = arrivedHomeService;
        }


        [HttpGet]
        public async Task<JsonResult> Get()
        {
            var result = await _arrivedHomeService.AmIHomeAsync();

            return Json(new
            {
                Answer = result
            });
        }

        [HttpPost]
        public JsonResult Post([FromBody] IAmHomeMessage message)
        {
            return Json(new 
            {
                Set = true
            });
        }
    }
}
