using BasketAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BasketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private IBasketQuery _baskets;
        private IBasketAdder _basketAdder;

        public BasketController(IBasketQuery baskets, IBasketAdder basketAdder)
        {
            _baskets = baskets;
            _basketAdder = basketAdder;
        }

        [HttpGet("{id}")]
        public ActionResult Get(Guid id)
        {
            var basket = _baskets.FindById(id);

            if (basket == null)
                return NotFound();

            return Ok(basket);
        }

        [HttpPost]
        public ActionResult Post()
        {
            var newBasket = _basketAdder.AddBasket();
            return Ok(newBasket);
        }
    }
}
