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
        private readonly Guid NewlyAddedItemId = Guid.Parse("eb29e491-f786-4d63-b09d-40956043c542");
        private readonly Guid FoundItemId = Guid.Parse("3fa180b2-e65b-48ee-9e31-a032dec504b1");
        private readonly Guid MissingItemId = Guid.Parse("4bf673a3-cf0f-4f79-b9ac-ef922a1ddde8");
        private readonly Guid FoundBasketId = Guid.Parse("f5b8861f-73e6-4624-b37d-8b8b5b93a229");
        private readonly Guid MissingBasketId = Guid.Parse("6c696970-4c89-46bd-9b89-1fc2c27ef71e");
        private readonly Guid UnauthorizedBasketId = Guid.Parse("4525e2c5-312a-41cf-9333-4143ce6a05d7");

        private readonly Guid OwnerId = Guid.NewGuid();

        private ItemController _controller;

        [SetUp]
        public void Set_up_controller()
        {
            var repository = new InMemoryBasketRepository(FoundBasketId, new List<Basket>());
            var basket = repository.Add(OwnerId);
            basket.AddItem(FoundItemId, 1);

            _controller = new ItemController(
                repository);
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, OwnerId.ToString())
                    }))
                }
            };
        }

        [Test]
        public void Get_returns_not_found_if_basket_missing()
        {
            var result = _controller.Get(MissingBasketId, FoundItemId);
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void Get_returns_not_found_if_item_not_in_basket()
        {
            var result = _controller.Get(FoundBasketId, MissingItemId);
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void Get_returns_not_found_if_basket_does_not_belong_to_user()
        {
            var result = _controller.Get(UnauthorizedBasketId, FoundItemId);
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void Get_returns_item_if_in_basket()
        {
            var result = _controller.Get(FoundBasketId, FoundItemId);
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(((OkObjectResult) result).Value, Is.TypeOf<Item>());
        }

        [Test]
        public void Post_returns_not_found_if_basket_missing()
        {
            var result = _controller.Post(MissingBasketId, new AddItem {ItemId = NewlyAddedItemId, Quantity = 3});
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void Post_returns_item_that_was_added_to_basket()
        {
            var result = _controller.Post(FoundBasketId, new AddItem {ItemId = NewlyAddedItemId, Quantity = 3});
            Assert.That(result, Is.TypeOf<CreatedAtActionResult>());
            Assert.That(((CreatedAtActionResult) result).Value, Is.TypeOf<Item>());

            var item = (Item) ((CreatedAtActionResult) result).Value;
            Assert.That(item.ItemId, Is.EqualTo(NewlyAddedItemId));
            Assert.That(item.Quantity, Is.EqualTo(3));
        }

        [Test]
        public void Update_returns_not_found_if_basket_missing()
        {
            var result = _controller.Update(MissingBasketId, FoundItemId, new UpdateItem {Quantity = 5});
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void Update_returns_not_found_if_item_not_in_basket()
        {
            var result = _controller.Update(FoundBasketId, MissingItemId, new UpdateItem {Quantity = 5});
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void Update_returns_item_that_was_updated()
        {
            var result = _controller.Update(FoundBasketId, FoundItemId, new UpdateItem {Quantity = 33});
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(((OkObjectResult) result).Value, Is.TypeOf<Item>());
            var item = (Item) ((OkObjectResult) result).Value;
            Assert.That(item.Quantity, Is.EqualTo(33));
        }

        [Test]
        public void Delete_returns_not_found_if_basket_missing()
        {
            var result = _controller.Delete(MissingBasketId, FoundItemId);
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void Delete_returns_not_found_if_item_not_in_basket()
        {
            var result = _controller.Delete(FoundBasketId, MissingItemId);
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void Delete_returns_no_content_if_deleted()
        {
            var result = _controller.Delete(FoundBasketId, FoundItemId);
            Assert.That(result, Is.TypeOf<NoContentResult>());
        }
    }
}