using Microsoft.eShopWeb.ApplicationCore.Exceptions;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using ApplicationCore.Entities.WishListAggregate;
using ApplicationCore.Exceptions;

namespace Ardalis.GuardClauses
{
    public static class BasketGuards
    {
        public static void NullBasket(this IGuardClause guardClause, int basketId, Basket basket)
        {
            if (basket == null)
                throw new BasketNotFoundException(basketId);
        }
    }
      public static class WishListGuards
    {
        public static void NullWishList(this IGuardClause guardClause, int wishlistId, WishList wishlist)
        {
            if (wishlist == null)
                throw new WishListNotFoundException(wishlistId);
        }
    }
} 