using BasketAPI.Controllers;
using BasketAPI.Interfaces;
using BasketAPI.Models;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using Tests.Shared;

namespace Tests
{
    [TestFixture]
    public class BasketItemControllerTests
    {
        private readonly Guid NewlyAddedProductId = Guid.Parse("eb29e491-f786-4d63-b09d-40956043c542");

        private readonly Guid FoundProductId = Guid.Parse("3fa180b2-e65b-48ee-9e31-a032dec504b1");
        private readonly Guid MissingProductId = Guid.Parse("4bf673a3-cf0f-4f79-b9ac-ef922a1ddde8");

        private readonly Guid FoundBasketId = Guid.Parse("f5b8861f-73e6-4624-b37d-8b8b5b93a229");
        private readonly Guid MissingBasketId = Guid.Parse("6c696970-4c89-46bd-9b89-1fc2c27ef71e");

        private BasketItemController _controller;

        [SetUp]
        public void Set_up_controller()
        {
            var repository = new InMemoryBasketRepository(FoundBasketId, new List<Basket>());
            var basket = repository.Add();
            basket.AddItem(FoundProductId, 1);

            _controller = new BasketItemController(
                repository);
        }

        [Test]
        public void get_returns_not_found_if_basket_missing()
        {
            var result = _controller.Get(MissingBasketId, FoundProductId);
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void get_returns_not_found_if_basket_item_missing()
        {
            var result = _controller.Get(FoundBasketId, MissingProductId);
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void get_returns_basket_item_if_it_exists()
        {
            var result = _controller.Get(FoundBasketId, FoundProductId);
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(((OkObjectResult)result).Value, Is.TypeOf<BasketItem>());
        }

        [Test]
        public void post_returns_not_found_if_basket_missing()
        {
            var result = _controller.Post(MissingBasketId, new AddBasketItem { ProductId = NewlyAddedProductId, Quantity = 3 });
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void post_returns_newly_created_basket_item_if_basket_exists()
        {
            var result = _controller.Post(FoundBasketId, new AddBasketItem { ProductId = NewlyAddedProductId, Quantity = 3 });
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(((OkObjectResult)result).Value, Is.TypeOf<BasketItem>());

            var basketItem = (BasketItem)((OkObjectResult)result).Value;
            Assert.That(basketItem.ProductId, Is.EqualTo(NewlyAddedProductId));
            Assert.That(basketItem.Quantity, Is.EqualTo(3));
        }

        [Test]
        public void update_returns_not_found_if_basket_missing()
        {
            var result = _controller.Update(MissingBasketId, FoundProductId, new UpdateBasketItem { Quantity = 5 });
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void update_returns_not_found_if_basket_item_missing()
        {
            var result = _controller.Update(FoundBasketId, MissingProductId, new UpdateBasketItem { Quantity = 5 });
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void update_returns_updated_basket_item_if_it_exists()
        {
            var result = _controller.Update(FoundBasketId, FoundProductId, new UpdateBasketItem { Quantity = 33 });
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(((OkObjectResult)result).Value, Is.TypeOf<BasketItem>());
            var basketItem = (BasketItem)((OkObjectResult)result).Value;
            Assert.That(basketItem.Quantity, Is.EqualTo(33));
        }

        [Test]
        public void delete_returns_not_found_if_basket_missing()
        {
            var result = _controller.Delete(MissingBasketId, FoundProductId);
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void delete_returns_not_found_if_basket_item_missing()
        {
            var result = _controller.Delete(FoundBasketId, MissingProductId);
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void delete_returns_ok_if_basket_item_exists()
        {
            var result = _controller.Delete(FoundBasketId, FoundProductId);
            Assert.That(result, Is.TypeOf<OkResult>());
        }
    }
}
