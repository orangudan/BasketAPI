using System;
using System.Linq;
using BasketAPI.Interfaces;
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

        [HttpGet("{basketId}/{basketItemId}")]
        public ActionResult Get(Guid basketId, Guid basketItemId)
        {
            var basket = _baskets.FindById(basketId);

            if (basket == null)
                return NotFound();

            var basketItem = basket.Items.SingleOrDefault(i => i.ProductId == basketItemId);
            if (basketItem == null)
                return NotFound();

            return Ok(basketItem);
        }

        [HttpPost("{basketId}")]
        public ActionResult Post(Guid basketId, AddBasketItem request)
        {
            var basket = _baskets.FindById(basketId);

            if (basket == null)
                return NotFound();

            var newBasketItem = _basketItemAdder.AddBasketItem(basket, request.ProductId);

            return Ok(newBasketItem);
        }
    }

    public class AddBasketItem
    {
        public Guid ProductId { get; set; }
    }
}
