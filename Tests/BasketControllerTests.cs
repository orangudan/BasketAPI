using BasketAPI.Controllers;
using BasketAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Tests.Shared;

namespace Tests
{
    [TestFixture]
    public class BasketControllerTests
    {
        private readonly Guid _missingBasketId = Guid.NewGuid();

        private readonly Guid _ownerId = Guid.NewGuid();

        private BasketController _controller;
        private Basket _basket;
        private Basket _notYourBasket;

        [SetUp]
        public void Set_up_controller()
        {
            var repository = new InMemoryBasketRepository();
            _basket = repository.Add(_ownerId);
            _basket.AddItem(Guid.NewGuid(), 1);

            _notYourBasket = repository.Add(Guid.NewGuid());

            _controller = new BasketController(repository)
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
            var result = _controller.Get(_missingBasketId);
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void Get_returns_basket_if_it_exists()
        {
            var result = _controller.Get(_basket.Id);
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(((OkObjectResult) result).Value, Is.TypeOf<Basket>());
        }

        [Test]
        public void Get_returns_not_found_if_basket_does_not_belong_to_user()
        {
            var result = _controller.Get(_notYourBasket.Id);
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
            var result = _controller.Delete(_missingBasketId);
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void Delete_returns_not_found_if_basket_does_not_belong_to_user()
        {
            var result = _controller.Delete(_notYourBasket.Id);
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void Delete_returns_no_content_if_deleted()
        {
            var result = _controller.Delete(_basket.Id);
            Assert.That(result, Is.TypeOf<NoContentResult>());
        }
    }
}