using BasketAPI.Models;
using NUnit.Framework;
using System;

namespace Tests.Models
{
    [TestFixture()]
    public class BasketTests
    {
        [Test]
        public void Contains_returns_true_if_item_added()
        {
            var basket = new Basket(Guid.NewGuid(), Guid.NewGuid());
            var item = basket.AddItem(Guid.NewGuid(), 3);
            Assert.That(basket.ContainsItem(item.ItemId), Is.True);
        }

        [Test]
        public void Contains_returns_false_if_item_not_added()
        {
            var basket = new Basket(Guid.NewGuid(), Guid.NewGuid());
            Assert.That(basket.ContainsItem(Guid.NewGuid()), Is.False);
        }

        [Test]
        public void Find_item_returns_item_with_matching_id()
        {
            var basket = new Basket(Guid.NewGuid(), Guid.NewGuid());
            var itemAdded = basket.AddItem(Guid.NewGuid(), 3);
            var itemFound = basket.FindItem(itemAdded.ItemId);
            Assert.That(itemFound, Is.Not.Null);
            Assert.That(itemFound.ItemId, Is.EqualTo(itemAdded.ItemId));
        }
    }
}
