using Microsoft.eShopWeb.ApplicationCore.Entities;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications
{
    public class CatalogFilterPaginatedSpecification : BaseSpecification<CatalogItem>
    {
        public CatalogFilterPaginatedSpecification(
            int skip, int take,
            string searchText, int? brandId, int? typeId)
            : base(i =>
                (!brandId.HasValue || i.CatalogBrandId == brandId) &&
                (!typeId.HasValue || i.CatalogTypeId == typeId) &&
                (string.IsNullOrEmpty(searchText) || i.Name.Contains(searchText)))
        {
            ApplyPaging(skip, take);
        }
    }
}
