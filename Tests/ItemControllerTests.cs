using BasketAPI.Controllers;
using BasketAPI.Models;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Tests.Shared;

namespace Tests
{
    [TestFixture]
    public class ItemControllerTests
    {
        private readonly Guid MissingItemId = Guid.NewGuid();
        private readonly Guid MissingBasketId = Guid.NewGuid();

        private readonly Guid OwnerId = Guid.NewGuid();

        private ItemController _controller;

        private Basket _basket;
        private Item _item;

        private Basket _notYourBasket;

        [SetUp]
        public void Set_up_controller()
        {
            var repository = new InMemoryBasketRepository();
            _basket = repository.Add(OwnerId);
            _item = _basket.AddItem(Guid.NewGuid(), 1);

            _notYourBasket = repository.Add(Guid.NewGuid());

            _controller = new ItemController(repository)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, OwnerId.ToString())
                        }))
                    }
                }
            };
        }

        [Test]
        public void Get_returns_not_found_if_basket_missing()
        {
            var result = _controller.Get(MissingBasketId, _item.ItemId);
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void Get_returns_not_found_if_item_not_in_basket()
        {
            var result = _controller.Get(_basket.Id, MissingItemId);
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void Get_returns_not_found_if_basket_does_not_belong_to_user()
        {
            var result = _controller.Get(_notYourBasket.Id, _item.ItemId);
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
            var result = _controller.Post(MissingBasketId, new AddItem());
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void Post_returns_not_found_if_basket_does_not_belong_to_user()
        {
            var result = _controller.Post(_notYourBasket.Id, new AddItem());
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void Post_returns_item_that_was_added_to_basket()
        {
            var result = _controller.Post(_basket.Id, new AddItem());
            Assert.That(result, Is.TypeOf<CreatedAtActionResult>());
            Assert.That(((CreatedAtActionResult) result).Value, Is.TypeOf<Item>());
        }

        [Test]
        public void Update_returns_not_found_if_basket_missing()
        {
            var result = _controller.Update(MissingBasketId, _item.ItemId, new UpdateItem());
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void Update_returns_not_found_if_item_not_in_basket()
        {
            var result = _controller.Update(_basket.Id, MissingItemId, new UpdateItem());
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void Update_returns_not_found_if_basket_does_not_belong_to_user()
        {
            var result = _controller.Update(_notYourBasket.Id, _item.ItemId, new UpdateItem());
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void Update_returns_item_that_was_updated()
        {
            var result = _controller.Update(_basket.Id, _item.ItemId, new UpdateItem());
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(((OkObjectResult) result).Value, Is.TypeOf<Item>());
        }

        [Test]
        public void Delete_returns_not_found_if_basket_missing()
        {
            var result = _controller.Delete(MissingBasketId, _item.ItemId);
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void Delete_returns_not_found_if_item_not_in_basket()
        {
            var result = _controller.Delete(_basket.Id, MissingItemId);
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