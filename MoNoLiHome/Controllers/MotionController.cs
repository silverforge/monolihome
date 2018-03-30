using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace MoNoLiHome.Controllers
{
    [Route("api/[controller]")]
    public class MotionController : Controller
    {
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
        public JsonResult GetMotionDetected()
        {
            return Json(new 
            {
                Response = true
            });
        }
    }
}
