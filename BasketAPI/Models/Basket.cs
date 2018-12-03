using System;
using System.Collections.Generic;
using System.Linq;

namespace BasketAPI.Models
{
    public class Basket
    {
        private List<Item> _items { get; } = new List<Item>();

        public Guid Id { get; }
        public Guid OwnerId { get; }
        public IEnumerable<Item> Items => _items;

        public Basket(Guid id, Guid ownerId)
        {
            Id = id;
            OwnerId = ownerId;
        }

        public Item FindItem(Guid itemId)
        {
            return Items.SingleOrDefault(item => item.ItemId == itemId);
        }

        public Item AddItem(Guid itemId, int quantity)
        {
            var item = new Item(itemId, quantity);
            _items.Add(item);
            return item;
        }

        public bool ContainsItem(Guid itemId)
        {
            return Items.Any(item => item.ItemId == itemId);
        }

        public void RemoveItem(Item item)
        {
            _items.Remove(item);
        }
    }
}