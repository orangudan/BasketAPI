using System;
using BasketAPI.Models;

namespace BasketAPI.Interfaces
{
    public interface IBasketItemAdder
    {
        BasketItem AddBasketItem(Basket basket, Guid productId);
    }
}
