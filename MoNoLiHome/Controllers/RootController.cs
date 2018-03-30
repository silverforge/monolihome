using Microsoft.AspNetCore.Mvc;

namespace MoNoLiHome.Controllers
{
    [Route("api")]
    public class RootController : Controller
    {
        [HttpGet]
        public JsonResult Get()
        {
            return Json(new 
            {
                Version = "1.0",
                Name = "MoNoLiHome"
            });
        }
    }
}
