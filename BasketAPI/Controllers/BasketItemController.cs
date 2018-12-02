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
        private IBasketQuery _baskets;
        private Func<BasketItem> _basketItemAdder;

        public BasketItemController(IBasketQuery baskets, Func<BasketItem> basketItemAdder)
        {
            _baskets = baskets;
            _basketItemAdder = basketItemAdder;
        }

        [HttpPost("{basketId}")]
        public ActionResult Post(Guid basketId)
        {
            var basket = _baskets.FindById(basketId);

            if (basket == null)
                return NotFound();

            var newBasketItem = _basketItemAdder();

            return Ok(newBasketItem);
        }
    }

    public class BasketItem
    {
        public Guid Id { get; set; }
    }
}
