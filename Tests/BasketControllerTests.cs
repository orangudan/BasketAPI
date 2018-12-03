using BasketAPI.Controllers;
using BasketAPI.Models;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Tests.Shared;

namespace Tests
{
    [TestFixture]
    public class BasketControllerTests
    {
        private readonly Guid NewlyCreatedBasketId = Guid.Parse("ab0d1822-06d8-4a63-8761-3f5ac7774671");
        private readonly Guid FoundBasketId = Guid.Parse("f5b8861f-73e6-4624-b37d-8b8b5b93a229");
        private readonly Guid MissingBasketId = Guid.Parse("6c696970-4c89-46bd-9b89-1fc2c27ef71e");

        private readonly Guid OwnerId = Guid.NewGuid();

        private BasketController _controller;

        [SetUp]
        public void Set_up_controller()
        {
            var basket = new Basket(FoundBasketId, Guid.NewGuid());
            basket.AddItem(Guid.NewGuid(), 1);

            _controller = new BasketController(
                new InMemoryBasketRepository(NewlyCreatedBasketId, new List<Basket> {basket}))
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