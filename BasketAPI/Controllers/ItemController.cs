using BasketAPI.Interfaces;
using BasketAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using BasketAPI.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace BasketAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    [Route("api/basket/{basketId}/[controller]")]
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

            if (basket == null || basket.OwnerId != User.GetUserId())
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

            if (basket == null || basket.OwnerId != User.GetUserId())
                return NotFound();

            var newItem = basket.AddItem(request.ItemId, request.Quantity);

            return CreatedAtAction(nameof(Get), new {basketId = basket.Id, itemId = newItem.ItemId}, newItem);
        }

        [HttpPatch("{itemId}")]
        [ProducesResponseType(typeof(Item), 200)]
        [ProducesResponseType(404)]
        public ActionResult Update(Guid basketId, Guid itemId, UpdateItem updateItem)
        {
            var basket = _basketRepository.FindById(basketId);

            if (basket == null || basket.OwnerId != User.GetUserId())
                return NotFound();

            var item = basket.FindItem(itemId);
            if (item == null)
                return NotFound();

            item.Quantity = updateItem.Quantity;

            return Ok(item);
        }

        [HttpDelete("{itemId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public ActionResult Delete(Guid basketId, Guid itemId)
        {
            var basket = _basketRepository.FindById(basketId);

            if (basket == null || basket.OwnerId != User.GetUserId())
                return NotFound();

            var item = basket.FindItem(itemId);
            if (item == null)
                return NotFound();

            basket.Items.Remove(item);

            return NoContent();
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