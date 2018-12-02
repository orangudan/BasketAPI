using BasketAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BasketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketItemController : ControllerBase
    {
        private IBasketQuery _baskets;

        public BasketItemController(IBasketQuery baskets)
        {
            _baskets = baskets;
        }

        [HttpGet("{basketId}/{basketItemId}")]
        public ActionResult Get(Guid basketId, Guid basketItemId)
        {
            var basket = _baskets.FindById(basketId);

            if (basket == null)
                return NotFound();

            var basketItem = basket.FindItem(basketItemId);

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

            var newBasketItem = basket.AddItem(request.ProductId, request.Quantity);

            return Ok(newBasketItem);
        }

        [HttpPatch("{basketId}/{basketItemId}")]
        public ActionResult Update(Guid basketId, Guid basketItemId, UpdateBasketItem updateBasketItem)
        {
            var basket = _baskets.FindById(basketId);

            if (basket == null)
                return NotFound();

            var basketItem = basket.FindItem(basketItemId);
            if (basketItem == null)
                return NotFound();

            basketItem.Quantity = updateBasketItem.Quantity;

            return Ok(basketItem);
        }

        [HttpDelete("{basketId}/{basketItemId}")]
        public ActionResult Delete(Guid basketId, Guid basketItemId)
        {
            var basket = _baskets.FindById(basketId);

            if (basket == null)
                return NotFound();

            var basketItem = basket.FindItem(basketItemId);
            if (basketItem == null)
                return NotFound();

            basket.Items.Remove(basketItem);

            return Ok();
        }
    }

    public class AddBasketItem
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class UpdateBasketItem
    {
        public int Quantity { get; set; }
    }
}
