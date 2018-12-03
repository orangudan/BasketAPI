using BasketAPI.Controllers;
using BasketAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Tests.Shared;

namespace Tests.Controllers
{
    [TestFixture]
    public class ItemControllerTests
    {
        private readonly Guid _missingItemId = Guid.NewGuid();
        private readonly Guid _missingBasketId = Guid.NewGuid();

        private readonly Guid _ownerId = Guid.NewGuid();

        private ItemController _controller;

        private Basket _basket;
        private Item _item;

        private Basket _notYourBasket;
        private Item _notYourItem;

        [SetUp]
        public void Set_up_controller()
        {
            var repository = new InMemoryBasketRepository();
            _basket = repository.Add(_ownerId);
            _item = _basket.AddItem(Guid.NewGuid(), 1);

            _notYourBasket = repository.Add(Guid.NewGuid());
            _notYourItem = _notYourBasket.AddItem(Guid.NewGuid(), 3);

            _controller = new ItemController(repository)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, _ownerId.ToString())
                        }))
                    }
                }
            };
        }

        [Test]
        public void Get_returns_not_found_if_basket_missing()
        {
            var result = _controller.Get(_missingBasketId, _item.ItemId);
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void Get_returns_not_found_if_item_not_in_basket()
        {
            var result = _controller.Get(_basket.Id, _missingItemId);
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void Get_returns_not_found_if_basket_does_not_belong_to_user()
        {
            var result = _controller.Get(_notYourBasket.Id, _notYourItem.ItemId);
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void Get_returns_item_if_in_basket()
        {
            var result = _controller.Get(_basket.Id, _item.ItemId);
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(((OkObjectResult) result).Value, Is.TypeOf<Item>());
        }

        [Test]
        public void Post_returns_not_found_if_basket_missing()
        {
            var result = _controller.Post(_missingBasketId, new AddItem(Guid.NewGuid(), 1));
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void Post_returns_not_found_if_basket_does_not_belong_to_user()
        {
            var result = _controller.Post(_notYourBasket.Id, new AddItem(Guid.NewGuid(), 1));
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void Post_returns_item_that_was_added_to_basket()
        {
            var result = _controller.Post(_basket.Id, new AddItem(Guid.NewGuid(), 1));
            Assert.That(result, Is.TypeOf<CreatedAtActionResult>());
            Assert.That(((CreatedAtActionResult) result).Value, Is.TypeOf<Item>());
        }

        [Test]
        public void Post_returns_bad_request_if_item_already_in_basket()
        {
            var result = _controller.Post(_basket.Id, new AddItem(_item.ItemId, 5));
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public void Update_returns_not_found_if_basket_missing()
        {
            var result = _controller.Update(_missingBasketId, _item.ItemId, new UpdateItem(4));
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void Update_returns_not_found_if_item_not_in_basket()
        {
            var result = _controller.Update(_basket.Id, _missingItemId, new UpdateItem(4));
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void Update_returns_not_found_if_basket_does_not_belong_to_user()
        {
            var result = _controller.Update(_notYourBasket.Id, _notYourItem.ItemId, new UpdateItem(4));
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void Update_returns_item_that_was_updated()
        {
            var result = _controller.Update(_basket.Id, _item.ItemId, new UpdateItem(4));
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(((OkObjectResult) result).Value, Is.TypeOf<Item>());
        }

        [Test]
        public void Delete_returns_not_found_if_basket_missing()
        {
            var result = _controller.Delete(_missingBasketId, _item.ItemId);
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void Delete_returns_not_found_if_item_not_in_basket()
        {
            var result = _controller.Delete(_basket.Id, _missingItemId);
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void Delete_returns_not_found_if_basket_does_not_belong_to_user()
        {
            var result = _controller.Delete(_notYourBasket.Id, _notYourItem.ItemId);
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void Delete_returns_no_content_if_deleted()
        {
            var result = _controller.Delete(_basket.Id, _item.ItemId);
            Assert.That(result, Is.TypeOf<NoContentResult>());
        }
    }
}