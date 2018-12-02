using BasketAPI.Models;
using System;

namespace BasketAPI.Interfaces
{
    public interface IBasketRepository
    {
        Basket FindById(Guid basketId);
        Basket Add();
        void Remove(Basket basket);
    }
}