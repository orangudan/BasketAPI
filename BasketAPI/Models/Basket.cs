using System;
using System.Collections.Generic;
using System.Linq;

namespace BasketAPI.Models
{
    public class Basket
    {
        public Guid Id { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();

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