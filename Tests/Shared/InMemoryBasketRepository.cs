using BasketAPI.Interfaces;
using BasketAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Shared
{
    public class InMemoryBasketRepository : IBasketRepository
    {
        private readonly Guid _generatedId;
        private readonly List<Basket> _baskets;

        public InMemoryBasketRepository(Guid generatedId, List<Basket> baskets)
        {
            _generatedId = generatedId;
            _baskets = baskets;
        }

        public Basket FindById(Guid basketId)
        {
            return _baskets.SingleOrDefault(b => b.Id == basketId);
        }

        public Basket Add(Guid ownerId)
        {
            var basket = new Basket(_generatedId, ownerId);
            _baskets.Add(basket);
            return basket;
        }

        public void Remove(Basket basket)
        {
            _baskets.Remove(basket);
        }
    }
}