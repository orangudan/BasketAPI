using BasketAPI.Models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace BasketAPI.Models
{
    public class Basket
    {
        public Guid Id { get; set; }
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();

        public BasketItem FindItem(Guid basketItemId)
        {
            return Items.SingleOrDefault(item => item.ProductId == basketItemId);
        }
    }
}
