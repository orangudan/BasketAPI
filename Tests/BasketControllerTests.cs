using BasketAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Tests
{
    [TestFixture]
    public class BasketControllerTests
    {
        private const string TestId = "ab0d1822-06d8-4a63-8761-3f5ac7774671";
        private const string FoundBasketId = "f5b8861f-73e6-4624-b37d-8b8b5b93a229";
        private const string NotFoundBasketId = "6c696970-4c89-46bd-9b89-1fc2c27ef71e";

        [Test]
        public void get_returns_not_found_if_basket_missing()
        {
            var controller = new BasketController(new InMemoryBasketQuery(), new InMemoryBasketAdder(Guid.Parse(TestId)));
            var result = controller.Get(Guid.Parse(NotFoundBasketId));
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void get_returns_basket_if_basket_exists()
        {
            var baskets = new List<Basket>
            {
                new Basket {Id = Guid.Parse(FoundBasketId)}
            };

            var controller = new BasketController(new InMemoryBasketQuery(baskets), new InMemoryBasketAdder(Guid.Parse(TestId)));
            var result = controller.Get(Guid.Parse(FoundBasketId));
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(((OkObjectResult)result).Value, Is.TypeOf<Basket>());
        }

        [Test]
        public void result_is_ok_when_basket_created()
        {
            var controller = new BasketController(new InMemoryBasketQuery(), new InMemoryBasketAdder(Guid.Parse(TestId)));
            var result = controller.Post();
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void result_contains_newly_created_basket()
        {
            var controller = new BasketController(new InMemoryBasketQuery(), new InMemoryBasketAdder(Guid.Parse(TestId)));
            var result = controller.Post();
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(((OkObjectResult)result).Value, Is.TypeOf<Basket>());
        }

        [Test]
        public void result_contains_basket_id()
        {
            var controller = new BasketController(new InMemoryBasketQuery(), new InMemoryBasketAdder(Guid.Parse(TestId)));
            var result = controller.Post();
            var basket = (Basket)((OkObjectResult)result).Value;
            Assert.That(basket.Id, Is.EqualTo(Guid.Parse(TestId)));
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
