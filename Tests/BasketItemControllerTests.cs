using BasketAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Tests
{
    [TestFixture]
    public class BasketItemControllerTests
    {
        private readonly Guid NotFoundBasketId = Guid.Parse("6c696970-4c89-46bd-9b89-1fc2c27ef71e");
        private readonly Guid FoundBasketId = Guid.Parse("f5b8861f-73e6-4624-b37d-8b8b5b93a229");
        private readonly Guid NewBasketItemId = Guid.Parse("8aeb643d-37a3-420f-ac30-9181148a7cb5");

        private BasketItemController _controller;

        [SetUp]
        public void Set_up_controller()
        {
            var _baskets = new List<Basket>
            {
                new Basket { Id = FoundBasketId }
            };

            _controller = new BasketItemController(
                new InMemoryBasketQuery(_baskets),
                new InMemoryBasketItemAdder(NewBasketItemId));
        }

        [Test]
        public void result_is_not_found_when_basket_doesnt_exist()
        {
            var result = _controller.Post(NotFoundBasketId);
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void result_contains_newly_created_basket_item()
        {
            var result = _controller.Post(FoundBasketId);
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(((OkObjectResult)result).Value, Is.TypeOf<BasketItem>());
        }

        [Test]
        public void result_contains_basket_item_id()
        {
            var result = _controller.Post(FoundBasketId);
            var basketItem = (BasketItem)((OkObjectResult)result).Value;
            Assert.That(basketItem.Id, Is.EqualTo(NewBasketItemId));
        }
    }

    public class InMemoryBasketItemAdder : IBasketItemAdder
    {
        private Guid _generatedId;

        public InMemoryBasketItemAdder(Guid generatedId)
        {
            _generatedId = generatedId;
        }

        public BasketItem AddBasketItem()
        {
            return new BasketItem
            {
                Id = _generatedId
            };
        }
    }
}
