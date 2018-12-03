using BasketAPI.Extensions;
using BasketAPI.Interfaces;
using BasketAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using FluentValidation;

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

        [HttpGet("{itemId}", Name = "GetItemById")]
        [ProducesResponseType(typeof(Item), 200)]
        [ProducesResponseType(404)]
        public ActionResult Get(Guid basketId, Guid itemId)
        {
            var basket = _basketRepository.FindById(basketId);

            if (!ValidBasket(basket))
                return NotFound();

            var item = basket.FindItem(itemId);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost(Name = "PostItem")]
        [ProducesResponseType(typeof(Item), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult Post(Guid basketId, AddItem request)
        {
            var basket = _basketRepository.FindById(basketId);

            if (!ValidBasket(basket))
                return NotFound();

            if (basket.ContainsItem(request.ItemId))
                return BadRequest();

            var newItem = basket.AddItem(request.ItemId, request.Quantity);

            return CreatedAtAction(nameof(Get), new {basketId = basket.Id, itemId = newItem.ItemId}, newItem);
        }

        [HttpPatch("{itemId}", Name = "PatchItem")]
        [ProducesResponseType(typeof(Item), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult Update(Guid basketId, Guid itemId, UpdateItem updateItem)
        {
            var basket = _basketRepository.FindById(basketId);

            if (!ValidBasket(basket))
                return NotFound();

            var item = basket.FindItem(itemId);
            if (item == null)
                return NotFound();

            item.Quantity = updateItem.Quantity;

            return Ok(item);
        }

        [HttpDelete("{itemId}", Name = "DeleteItem")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public ActionResult Delete(Guid basketId, Guid itemId)
        {
            var basket = _basketRepository.FindById(basketId);

            if (!ValidBasket(basket))
                return NotFound();

            var item = basket.FindItem(itemId);
            if (item == null)
                return NotFound();

            basket.RemoveItem(item);

            return NoContent();
        }

        private bool ValidBasket(Basket basket)
        {
            return basket.Exists() && basket.BelongsTo(User.GetUserId());
        }
    }

    public class AddItem
    {
        public Guid ItemId { get; set; }
        public int Quantity { get; set; }

        public AddItem(Guid itemId, int quantity)
        {
            ItemId = itemId;
            Quantity = quantity;
        }

        public class Validator : AbstractValidator<AddItem>
        {
            public Validator()
            {
                RuleFor(x => x.ItemId).NotEmpty();
                RuleFor(x => x.Quantity).GreaterThan(0);
            }
        }
    }

    public class UpdateItem
    {
        public int Quantity { get; set; }

        public UpdateItem(int quantity)
        {
            Quantity = quantity;
        }

        public class Validator : AbstractValidator<UpdateItem>
        {
            public Validator()
            {
                RuleFor(x => x.Quantity).GreaterThan(0);
            }
        }
    }
}