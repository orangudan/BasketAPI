using BasketAPI.Models;
using System;

namespace BasketAPI.Interfaces
{
    public interface IBasketQuery
    {
        Basket FindById(Guid id);
    }
}
