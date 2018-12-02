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
                new InMemoryBasketRepository(NewlyCreatedBasketId, baskets));
        }

        [Test]
        public void get_returns_not_found_if_basket_missing()
        {
            var result = _controller.Get(NotFoundBasketId);
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void get_returns_basket_if_it_exists()
        {
            var result = _controller.Get(FoundBasketId);
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(((OkObjectResult)result).Value, Is.TypeOf<Basket>());
        }

        [Test]
        public void get_includes_basket_items_in_response()
        {
            var result = _controller.Get(FoundBasketId);
            var basket = (Basket)((OkObjectResult)result).Value;
            Assert.That(basket.Items.Any(), Is.True);
        }

        [Test]
        public void post_returns_ok()
        {
            var result = _controller.Post();
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void post_returns_newly_created_basket()
        {
            var result = _controller.Post();
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(((OkObjectResult)result).Value, Is.TypeOf<Basket>());
            var basket = (Basket)((OkObjectResult)result).Value;
            Assert.That(basket.Id, Is.EqualTo(NewlyCreatedBasketId));
        }
    }

    public class InMemoryBasketRepository : IBasketRepository
    {
        private readonly Guid _generatedId;
        private readonly List<Basket> _baskets;

        public InMemoryBasketRepository(Guid generatedId, List<Basket> baskets)
        {
            _generatedId = generatedId;
            _baskets = baskets;

        }

        public Basket FindById(Guid basketId)
        {
            return _baskets.SingleOrDefault(b => b.Id == basketId);
        }

        public Basket Add()
        {
            var basket = new Basket { Id = _generatedId };
            _baskets.Add(basket);
            return basket;
        }

        public void Remove(Basket basket)
        {
            _baskets.Remove(basket);
        }
    }
}
