using System;
using Microsoft.AspNetCore.Mvc;

namespace BasketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketItemController : ControllerBase
    {
        private IBasketQuery _baskets;
        private IBasketItemAdder _basketItemAdder;

        public BasketItemController(IBasketQuery baskets, IBasketItemAdder basketItemAdder)
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

            var newBasketItem = _basketItemAdder.AddBasketItem();

            return Ok(newBasketItem);
        }
    }

    public class BasketItem
    {
        public Guid Id { get; set; }
    }

    public interface IBasketItemAdder
    {
        BasketItem AddBasketItem();
    }
}
