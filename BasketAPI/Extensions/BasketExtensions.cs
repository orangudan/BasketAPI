using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasketAPI.Models;

namespace BasketAPI.Extensions
{
    public static class BasketExtensions
    {
        public static bool BelongsTo(this Basket basket, Guid ownerId)
        {
            return basket.OwnerId == ownerId;
        }

        public static bool Exists(this Basket basket)
        {
            return basket != null;
        }
    }
}
