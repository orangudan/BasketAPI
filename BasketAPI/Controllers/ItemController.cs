using BasketAPI.Interfaces;
using BasketAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BasketAPI.Controllers
{
    [Route("api/basket/{basketId}/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ItemController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;

        public ItemController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        [HttpGet("{itemId}")]
        [ProducesResponseType(typeof(Item), 200)]
        [ProducesResponseType(404)]
        public ActionResult Get(Guid basketId, Guid itemId)
        {
            var basket = _basketRepository.FindById(basketId);

            if (basket == null)
                return NotFound();

            var item = basket.FindItem(itemId);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Item), 200)]
        [ProducesResponseType(404)]
        public ActionResult Post(Guid basketId, AddItem request)
        {
            var basket = _basketRepository.FindById(basketId);

            if (basket == null)
                return NotFound();

            var newItem = basket.AddItem(request.ItemId, request.Quantity);

            return Ok(newItem);
        }

        [HttpPatch("{itemId}")]
        [ProducesResponseType(typeof(Item), 200)]
        [ProducesResponseType(404)]
        public ActionResult Update(Guid basketId, Guid itemId, UpdateItem updateItem)
        {
            var basket = _basketRepository.FindById(basketId);

            if (basket == null)
                return NotFound();

            var item = basket.FindItem(itemId);
            if (item == null)
                return NotFound();

            item.Quantity = updateItem.Quantity;

            return Ok(item);
        }

        [HttpDelete("{itemId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult Delete(Guid basketId, Guid itemId)
        {
            var basket = _basketRepository.FindById(basketId);

            if (basket == null)
                return NotFound();

            var item = basket.FindItem(itemId);
            if (item == null)
                return NotFound();

            basket.Items.Remove(item);

            return Ok();
        }
    }

    public class AddItem
    {
        public Guid ItemId { get; set; }
        public int Quantity { get; set; }
    }

    public class UpdateItem
    {
        public int Quantity { get; set; }
    }
}