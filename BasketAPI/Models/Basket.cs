using System;
using System.Collections.Generic;
using System.Linq;

namespace BasketAPI.Models
{
    public class Basket
    {
        public Guid Id { get; }
        public List<Item> Items { get; } = new List<Item>();
        public Guid OwnerId { get; }

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
            Items.Add(item);
            return item;
        }
    }
}