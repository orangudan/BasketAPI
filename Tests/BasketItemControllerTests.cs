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
        private readonly Guid ProductId = Guid.Parse("eb29e491-f786-4d63-b09d-40956043c542");
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
                new InMemoryBasketItemAdder());
        }

        [Test]
        public void result_is_not_found_when_basket_doesnt_exist()
        {
            var result = _controller.Post(NotFoundBasketId, new AddBasketItem { ProductId = ProductId });
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void result_contains_newly_created_basket_item()
        {
            var result = _controller.Post(FoundBasketId, new AddBasketItem { ProductId = ProductId });
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(((OkObjectResult)result).Value, Is.TypeOf<BasketItem>());
        }

        [Test]
        public void result_contains_product_id()
        {
            var result = _controller.Post(FoundBasketId, new AddBasketItem { ProductId = ProductId });
            var basketItem = (BasketItem)((OkObjectResult)result).Value;
            Assert.That(basketItem.ProductId, Is.EqualTo(ProductId));
        }
    }

    public class InMemoryBasketItemAdder : IBasketItemAdder
    {
        public BasketItem AddBasketItem(Basket basket, Guid productId)
        {
            var basketItem = new BasketItem
            {
                ProductId = productId
            };
            basket.Items.Add(basketItem);
            return basketItem;
        }
    }
}
