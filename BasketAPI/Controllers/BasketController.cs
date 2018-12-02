using Microsoft.AspNetCore.Mvc;

namespace BasketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        public ActionResult Post()
        {
            return Ok();
        }
    }
}
