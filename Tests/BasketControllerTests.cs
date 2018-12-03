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
    public class BasketControllerTests
    {
        private readonly Guid NewlyCreatedBasketId = Guid.NewGuid();
        private readonly Guid FoundBasketId = Guid.NewGuid();
        private readonly Guid MissingBasketId = Guid.NewGuid();
        private readonly Guid NotYourBasketId = Guid.NewGuid();

        private readonly Guid OwnerId = Guid.NewGuid();

        private BasketController _controller;

        [SetUp]
        public void Set_up_controller()
        {
            var basket = new Basket(FoundBasketId, OwnerId);
            basket.AddItem(Guid.NewGuid(), 1);

            var notYourBasket = new Basket(NotYourBasketId, Guid.NewGuid());

            _controller = new BasketController(
                new InMemoryBasketRepository(NewlyCreatedBasketId, new List<Basket> {basket, notYourBasket}))
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
            var result = _controller.Get(MissingBasketId);
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void Get_returns_basket_if_it_exists()
        {
            var result = _controller.Get(FoundBasketId);
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(((OkObjectResult) result).Value, Is.TypeOf<Basket>());
        }

        [Test]
        public void Get_returns_not_found_if_basket_does_not_belong_to_user()
        {
            var result = _controller.Get(NotYourBasketId);
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void Post_returns_basket_that_was_created()
        {
            var result = _controller.Post();
            Assert.That(result, Is.TypeOf<CreatedAtActionResult>());
            Assert.That(((CreatedAtActionResult) result).Value, Is.TypeOf<Basket>());
        }

        [Test]
        public void Delete_returns_not_found_if_basket_missing()
        {
            var result = _controller.Delete(MissingBasketId);
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void Delete_returns_no_content_if_deleted()
        {
            var result = _controller.Delete(FoundBasketId);
            Assert.That(result, Is.TypeOf<NoContentResult>());
        }
    }
}