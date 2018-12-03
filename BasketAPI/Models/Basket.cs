using System;
using System.Collections.Generic;
using System.Linq;

namespace BasketAPI.Models
{
    public class Basket
    {
        public Guid Id { get; }

        public List<Item> Items { get; } = new List<Item>();

        public Basket(Guid id)
        {
            Id = id;
        }

        public Item FindItem(Guid itemId)
        {
            return Items.SingleOrDefault(item => item.ItemId == itemId);
        }

        public Item AddItem(Guid itemId, int quantity)
        {
            var item = new Item {ItemId = itemId, Quantity = quantity};
            Items.Add(item);
            return item;
        }
    }
}