using BasketAPI.Models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace BasketAPI.Models
{
    public class Basket
    {
        public Guid Id { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();

        public Item FindItem(Guid basketItemId)
        {
            return Items.SingleOrDefault(item => item.ProductId == basketItemId);
        }

        public Item AddItem(Guid productId, int quantity)
        {
            var item = new Item { ProductId = productId, Quantity = quantity };
            Items.Add(item);
            return item;
        }
    }
}
