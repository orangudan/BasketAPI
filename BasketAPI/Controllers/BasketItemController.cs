using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace BasketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketItemController : ControllerBase
    {
        private IEnumerable<Basket> _baskets;

        public BasketItemController(IEnumerable<Basket> baskets)
        {
            _baskets = baskets;
        }

        [HttpPost("{basketId}")]
        public ActionResult Post(Guid basketId)
        {
            var basket = _baskets.SingleOrDefault(b => b.Id == basketId);

            if (basket == null)
                return NotFound();

            return Ok();
        }
    }
}
