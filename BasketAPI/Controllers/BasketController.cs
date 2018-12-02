using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Collections.Generic;

namespace BasketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private IEnumerable<Basket> _baskets;
        private Func<Basket> _basketAdder;

        public BasketController(IEnumerable<Basket> baskets, Func<Basket> basketAdder)
        {
            _baskets = baskets;
            _basketAdder = basketAdder;
        }

        public ActionResult Get(Guid id)
        {
            var basket = _baskets.SingleOrDefault(b => b.Id == id);

            if (basket == null)
                return NotFound();

            return Ok();
        }

        public ActionResult Post()
        {
            var newBasket = _basketAdder();
            return Ok(newBasket);
        }
    }

    public class Basket
    {
        public Guid Id { get; set; }
    }
}
