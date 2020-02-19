using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace ApplicationCore.Entities.WishListAggregate
{
    public class WishListItem : BaseEntity, IAggregateRoot
    {
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int CatalogItemId { get; set; }
        public int WishListId { get; private set; }
    }
}