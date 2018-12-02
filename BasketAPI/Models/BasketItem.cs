using System;

namespace BasketAPI.Models
{
    public class BasketItem
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
