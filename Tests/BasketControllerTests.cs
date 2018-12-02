using BasketAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using BasketAPI.Models;
using BasketAPI.Interfaces;

namespace Tests
{
    [TestFixture]
    public class BasketControllerTests
    {
        private readonly Guid NewlyCreatedBasketId = Guid.Parse("ab0d1822-06d8-4a63-8761-3f5ac7774671");
        private readonly Guid FoundBasketId = Guid.Parse("f5b8861f-73e6-4624-b37d-8b8b5b93a229");
        private readonly Guid NotFoundBasketId = Guid.Parse("6c696970-4c89-46bd-9b89-1fc2c27ef71e");

        private BasketController _controller;

        [SetUp]
        public void Set_up_controller()
        {
            var baskets = new List<Basket>
            {
                new Basket
                {
                    Id = FoundBasketId,
                    Items = new List<BasketItem>
                    {
                        new BasketItem()
                    }
                }
            };

            _controller = new BasketController(
                new InMemoryBasketQuery(baskets),
                new InMemoryBasketAdder(NewlyCreatedBasketId));
        }

        [Test]
        public void get_returns_not_found_if_basket_missing()
        {
            var result = _controller.Get(NotFoundBasketId);
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void get_returns_basket_if_basket_exists()
        {
            var result = _controller.Get(FoundBasketId);
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(((OkObjectResult)result).Value, Is.TypeOf<Basket>());
        }

        [Test]
        public void get_returns_basket_items()
        {
            var result = _controller.Get(FoundBasketId);
            var basket = (Basket)((OkObjectResult)result).Value;
            Assert.That(basket.Items.Any(), Is.True);
        }

        [Test]
        public void result_is_ok_when_basket_created()
        {
            var result = _controller.Post();
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void result_contains_newly_created_basket()
        {
            var result = _controller.Post();
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(((OkObjectResult)result).Value, Is.TypeOf<Basket>());
        }

        [Test]
        public void result_contains_basket_id()
        {
            var result = _controller.Post();
            var basket = (Basket)((OkObjectResult)result).Value;
            Assert.That(basket.Id, Is.EqualTo(NewlyCreatedBasketId));
        }
    }

    public class InMemoryBasketAdder : IBasketAdder
    {
        private Guid _generatedId;

        public InMemoryBasketAdder(Guid generatedId)
        {
            _generatedId = generatedId;
        }

        public Basket AddBasket()
        {
            return new Basket
            {
                Id = _generatedId
            };
        }
    }

    public class InMemoryBasketQuery : IBasketQuery
    {
        private IEnumerable<Basket> _baskets;

        public InMemoryBasketQuery()
        {
            _baskets = new List<Basket>();
        }

        public InMemoryBasketQuery(IEnumerable<Basket> baskets)
        {
            _baskets = baskets;
        }

        public Basket FindById(Guid id)
        {
            return _baskets.SingleOrDefault(b => b.Id == id);
        }
    }
}
