using BasketAPI.Controllers;
using NUnit.Framework;
using System;

namespace Tests
{
    [TestFixture]
    public class ValuesControllerTests
    {
        [Test]
        public void get_returns_value1_and_value2()
        {
            var controller = new ValuesController();
            var result = controller.Get();
            Assert.That(result.Value, Is.EqualTo(new[] { "value1", "fail" }));
        }
    }
}
