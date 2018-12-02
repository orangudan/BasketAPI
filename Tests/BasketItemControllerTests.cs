using BasketAPI.Controllers;
using BasketAPI.Interfaces;
using BasketAPI.Models;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Tests
{
    [TestFixture]
    public class BasketItemControllerTests
    {
        private readonly Guid NewlyAddedProductId = Guid.Parse("eb29e491-f786-4d63-b09d-40956043c542");
        private readonly Guid FoundProductId = Guid.Parse("3fa180b2-e65b-48ee-9e31-a032dec504b1");
        private readonly Guid NotFoundProductId = Guid.Parse("4bf673a3-cf0f-4f79-b9ac-ef922a1ddde8");
        private readonly Guid NotFoundBasketId = Guid.Parse("6c696970-4c89-46bd-9b89-1fc2c27ef71e");
        private readonly Guid FoundBasketId = Guid.Parse("f5b8861f-73e6-4624-b37d-8b8b5b93a229");
        private readonly Guid NewBasketItemId = Guid.Parse("8aeb643d-37a3-420f-ac30-9181148a7cb5");

        private BasketItemController _controller;

        [SetUp]
        public void Set_up_controller()
        {
            var _baskets = new List<Basket>
            {
                new Basket
                {
                    Id = FoundBasketId,
                    Items = new List<BasketItem>
                    {
                        new BasketItem { ProductId = FoundProductId }
                    }
                }
            };

            _controller = new BasketItemController(
                new InMemoryBasketQuery(_baskets),
                new InMemoryBasketItemAdder());
        }

        [Test]
        public void get_returns_not_found_if_basket_item_is_missing()
        {
            var result = _controller.Get(FoundBasketId, NotFoundProductId);
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void get_returns_basket_item_if_basket_item_exists()
        {
            var result = _controller.Get(FoundBasketId, FoundProductId);
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(((OkObjectResult)result).Value, Is.TypeOf<BasketItem>());
        }

        [Test]
        public void result_is_not_found_when_basket_doesnt_exist()
        {
            var result = _controller.Post(NotFoundBasketId, new AddBasketItem { ProductId = NewlyAddedProductId });
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void result_contains_newly_created_basket_item()
        {
            var result = _controller.Post(FoundBasketId, new AddBasketItem { ProductId = NewlyAddedProductId });
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(((OkObjectResult)result).Value, Is.TypeOf<BasketItem>());
        }

        [Test]
        public void result_contains_product_id()
        {
            var result = _controller.Post(FoundBasketId, new AddBasketItem { ProductId = NewlyAddedProductId });
            var basketItem = (BasketItem)((OkObjectResult)result).Value;
            Assert.That(basketItem.ProductId, Is.EqualTo(NewlyAddedProductId));
        }

        [Test]
        public void result_contains_quantity()
        {
            var result = _controller.Post(FoundBasketId, new AddBasketItem { ProductId = NewlyAddedProductId, Quantity = 3 });
            var basketItem = (BasketItem)((OkObjectResult)result).Value;
            Assert.That(basketItem.Quantity, Is.EqualTo(3));
        }
    }

    public class InMemoryBasketItemAdder : IBasketItemAdder
    {
        public BasketItem AddBasketItem(Basket basket, Guid productId, int quantity)
        {
            var basketItem = new BasketItem
            {
                ProductId = productId,
                Quantity = quantity
            };
            basket.Items.Add(basketItem);
            return basketItem;
        }
    }
}
