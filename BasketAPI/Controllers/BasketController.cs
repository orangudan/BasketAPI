﻿using BasketAPI.Extensions;
using BasketAPI.Interfaces;
using BasketAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BasketAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        [HttpGet("{id}", Name = "GetBasketById")]
        [ProducesResponseType(typeof(Basket), 200)]
        [ProducesResponseType(404)]
        public ActionResult Get(Guid id)
        {
            var basket = _basketRepository.FindById(id);

            if (!ValidBasket(basket))
                return NotFound();

            return Ok(basket);
        }

        [HttpPost(Name = "PostBasket")]
        [ProducesResponseType(typeof(Basket), 201)]
        public ActionResult Post()
        {
            var newBasket = _basketRepository.Add(User.GetUserId());
            return CreatedAtAction(nameof(Get), new {id = newBasket.Id}, newBasket);
        }

        [HttpDelete("{id}", Name = "DeleteBasket")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public ActionResult Delete(Guid id)
        {
            var basket = _basketRepository.FindById(id);

            if (!ValidBasket(basket))
                return NotFound();

            _basketRepository.Remove(basket);

            return NoContent();
        }

        private bool ValidBasket(Basket basket)
        {
            return basket.Exists() && basket.BelongsTo(User.GetUserId());
        }
    }
}