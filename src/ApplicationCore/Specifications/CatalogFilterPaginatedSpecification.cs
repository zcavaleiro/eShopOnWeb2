using Microsoft.eShopWeb.ApplicationCore.Entities;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications
{
    public class CatalogFilterPaginatedSpecification : CatalogFilterSpecification
    {
        public CatalogFilterPaginatedSpecification(
            int skip, int take,
            string searchText, int? brandId, int? typeId)
            : base(searchText, brandId, typeId)
        {
            ApplyPaging(skip, take);
        }
    }
}
