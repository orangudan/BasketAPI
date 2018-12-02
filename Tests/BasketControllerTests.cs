using BasketAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class BasketControllerTests
    {
        [Test]
        public void returns_success_when_basket_created()
        {
            var controller = new BasketController();
            var result = controller.Post();
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void result_contains_newly_created_basket()
        {
            var controller = new BasketController();
            var result = controller.Post();
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(((OkObjectResult)result).Value, Is.TypeOf<Basket>());
        }
    }
}
