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
        [Test]
        public void result_is_not_found_when_basket_doesnt_exist()
        {
            IEnumerable<Basket> baskets = new List<Basket>
            {
                new Basket { Id = Guid.Parse("f5b8861f-73e6-4624-b37d-8b8b5b93a229")}
            };

            var controller = new BasketItemController(baskets);
            var result = controller.Post(Guid.Parse("6c696970-4c89-46bd-9b89-1fc2c27ef71e"));
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }
    }
}
