using BasketAPI.Models;

namespace BasketAPI.Interfaces
{
    public interface IBasketRepository
    {
        Basket Add();
        void Remove(Basket basket);
    }
}
