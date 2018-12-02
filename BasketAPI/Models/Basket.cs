using BasketAPI.Models;
using System;
using System.Collections.Generic;

namespace BasketAPI.Models
{
    public class Basket
    {
        public Guid Id { get; set; }
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();
    }
}
