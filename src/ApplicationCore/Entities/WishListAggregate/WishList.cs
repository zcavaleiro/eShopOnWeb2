using System.Collections.Generic;
using System.Linq;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace ApplicationCore.Entities.WishListAggregate
{
    public class WishList : BaseEntity, IAggregateRoot
    {
        public string BuyerId { get; set; }
        private readonly List<WishListItem> _items = new List<WishListItem>();
        public IReadOnlyCollection<WishListItem> Items => _items.AsReadOnly();

        public void AddItem(int catalogItemId)
        {
            if (!Items.Any(i => i.CatalogItemId == catalogItemId))
            {
                _items.Add(new WishListItem()
                {
                    CatalogItemId = catalogItemId
                });
                return;
            }
        }
    }
}