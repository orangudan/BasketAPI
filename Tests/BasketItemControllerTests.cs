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
        private const string NotFoundBasketId = "6c696970-4c89-46bd-9b89-1fc2c27ef71e";
        private const string FoundBasketId = "f5b8861f-73e6-4624-b37d-8b8b5b93a229";
        private const string NewBasketItemId = "8aeb643d-37a3-420f-ac30-9181148a7cb5";
        private Func<BasketItem> _basketItemAdder = () => new BasketItem { Id = Guid.Parse(NewBasketItemId) };
        private IEnumerable<Basket> _baskets = new List<Basket>
        {
            new Basket { Id = Guid.Parse(FoundBasketId)}
        };

        [Test]
        public void result_is_not_found_when_basket_doesnt_exist()
        {
            var controller = new BasketItemController(_baskets, _basketItemAdder);
            var result = controller.Post(Guid.Parse(NotFoundBasketId));
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void result_contains_newly_created_basket_item()
        {
            var controller = new BasketItemController(_baskets, _basketItemAdder);
            var result = controller.Post(Guid.Parse(FoundBasketId));
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(((OkObjectResult)result).Value, Is.TypeOf<BasketItem>());
        }

        [Test]
        public void result_contains_basket_item_id()
        {
            var controller = new BasketItemController(_baskets, _basketItemAdder);
            var result = controller.Post(Guid.Parse(FoundBasketId));
            var basketItem = (BasketItem)((OkObjectResult)result).Value;
            Assert.That(basketItem.Id, Is.EqualTo(Guid.Parse(NewBasketItemId)));
        }
    }
}
