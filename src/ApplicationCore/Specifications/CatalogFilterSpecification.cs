using Microsoft.eShopWeb.ApplicationCore.Entities;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications
{

    public class CatalogFilterSpecification : BaseSpecification<CatalogItem>
    {
        public CatalogFilterSpecification(string searchText, int? brandId, int? typeId)
            : base(catalogItem =>
                (!brandId.HasValue || catalogItem.CatalogBrandId == brandId) &&
                (!typeId.HasValue || catalogItem.CatalogTypeId == typeId) &&
                (string.IsNullOrEmpty(searchText) || catalogItem.Name.Contains(searchText)))
        {
        }
    }
}
