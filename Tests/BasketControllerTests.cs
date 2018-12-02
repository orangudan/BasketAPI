using BasketAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;

namespace Tests
{
    [TestFixture]
    public class BasketControllerTests
    {
        private const string TestId = "ab0d1822-06d8-4a63-8761-3f5ac7774671";
        private Func<Basket> _basketAdder = () => new Basket { Id = Guid.Parse(TestId) };

        [Test]
        public void result_is_ok_when_basket_created()
        {
            var controller = new BasketController(_basketAdder);
            var result = controller.Post();
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void result_contains_newly_created_basket()
        {
            var controller = new BasketController(_basketAdder);
            var result = controller.Post();
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(((OkObjectResult)result).Value, Is.TypeOf<Basket>());
        }

        [Test]
        public void result_contains_basket_id()
        {
            var controller = new BasketController(_basketAdder);
            var result = controller.Post();
            var basket = (Basket)((OkObjectResult)result).Value;
            Assert.That(basket.Id, Is.EqualTo(Guid.Parse(TestId)));
        }
    }
}
