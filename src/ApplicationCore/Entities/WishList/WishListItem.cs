using Microsoft.eShopWeb.ApplicationCore.Entities;

namespace ApplicationCore.Entities.WishList
{
    public class WishListItem : CatalogItem
    {
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int CatalogItemId { get; set; }
    }
}