using ApplicationCore.Entities.WishListAggregate;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications
{
    public sealed class WishListWithItemsSpecification : BaseSpecification<WishList>
    {
        public WishListWithItemsSpecification(int wishlistId)
            :base(b => b.Id == wishlistId)
        {
            AddInclude(b => b.Items);
        }
        public WishListWithItemsSpecification(string buyerId)
            :base(b => b.BuyerId == buyerId)
        {
            AddInclude(b => b.Items);
        }
    }
} 