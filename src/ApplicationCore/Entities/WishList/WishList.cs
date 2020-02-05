using System.Collections.Generic;
using System.Linq;

namespace ApplicationCore.Entities.WishList
{
    public class WishList
    {
        public string BuyerId { get; set; }
        private readonly List<WishListItem> _wishitems = new List<WishListItem>();
        public IReadOnlyCollection<WishListItem> Items => _wishitems.AsReadOnly();

        public void AddItem(int catalogItemId, decimal unitPrice, int quantity = 1)
        {
            if (!Items.Any(i => i.CatalogItemId == catalogItemId))
            {
                _wishitems.Add(new WishListItem()
                {
                    CatalogItemId = catalogItemId,
                    Quantity = quantity,
                    UnitPrice = unitPrice
                });
                return;
            }
            var existingItem = Items.FirstOrDefault(i => i.CatalogItemId == catalogItemId);
            existingItem.Quantity += quantity;
        }
        public void RemoveEmptyItems()
        {
            _wishitems.RemoveAll(i => i.Quantity == 0);
        }
    }
}