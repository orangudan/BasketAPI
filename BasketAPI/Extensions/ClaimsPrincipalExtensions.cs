using System;
using System.Security.Claims;

namespace BasketAPI.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal principal)
        {
            return Guid.Parse(principal.Identity.Name);
        }
    }
}