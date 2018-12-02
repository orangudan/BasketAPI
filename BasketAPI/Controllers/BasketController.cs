﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Collections.Generic;

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

        public ActionResult Get(Guid id)
        {
            var basket = _baskets.FindById(id);

            if (basket == null)
                return NotFound();

            return Ok(basket);
        }

        public ActionResult Post()
        {
            var newBasket = _basketAdder.AddBasket();
            return Ok(newBasket);
        }
    }

    public class Basket
    {
        public Guid Id { get; set; }
    }

    public interface IBasketQuery
    {
        Basket FindById(Guid id);
    }

    public interface IBasketAdder
    {
        Basket AddBasket();
    }
}
