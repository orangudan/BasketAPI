using System;

namespace BasketAPI.Models
{
    public class Item
    {
        public Item(Guid itemId, int quantity)
        {
            ItemId = itemId;
            Quantity = quantity;
        }

        public Guid ItemId { get; }
        public int Quantity { get; set; }
    }
}