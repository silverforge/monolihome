using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoNoLiHome.Model;
using MoNoLiHome.Network.Client;

namespace MoNoLiHome.Controllers
{
    [Route("api/[controller]")]
    public class IAmHomeController : Controller
    {
        readonly IRedisConnector _connector;

        public IAmHomeController(IRedisConnector connector)
        {
            _connector = connector;
        }


        [HttpGet]
        public async Task<JsonResult> Get()
        {
            var result = await _connector.GetAsync("I_AM_HOME");

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
