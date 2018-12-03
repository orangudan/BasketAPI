using BasketAPI.Interfaces;
using BasketAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using BasketAPI.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace BasketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Authorize]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Basket), 200)]
        [ProducesResponseType(404)]
        public ActionResult Get(Guid id)
        {
            var basket = _basketRepository.FindById(id);

            if (basket == null || basket.OwnerId != User.GetUserId())
                return NotFound();

            return Ok(basket);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Basket), 200)]
        public ActionResult Post()
        {
            var newBasket = _basketRepository.Add(User.GetUserId());
            return CreatedAtAction(nameof(Get), new {id = newBasket.Id}, newBasket);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public ActionResult Delete(Guid id)
        {
            var basket = _basketRepository.FindById(id);

            if (basket == null || basket.OwnerId != User.GetUserId())
                return NotFound();

            _basketRepository.Remove(basket);

            return NoContent();
        }
    }
}