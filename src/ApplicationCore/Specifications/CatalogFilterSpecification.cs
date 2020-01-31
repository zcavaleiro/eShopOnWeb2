using System;
using System.Linq.Expressions;
using Microsoft.eShopWeb.ApplicationCore.Entities;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications
{

    public class CatalogFilterSpecification : BaseSpecification<CatalogItem>
    {
        /// <summary>
        /// Build filter catalog expression
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="brandId"></param>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public static Expression<Func<CatalogItem, bool>> BuildCatalogFilterExpression(
            string searchText, int? brandId, int? typeId) {
            return catalogItem =>
                (!brandId.HasValue || catalogItem.CatalogBrandId == brandId) &&
                (!typeId.HasValue || catalogItem.CatalogTypeId == typeId) &&
                (string.IsNullOrEmpty(searchText) || catalogItem.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase));
        }

        public CatalogFilterSpecification(string searchText, int? brandId, int? typeId)
            : base(BuildCatalogFilterExpression(searchText, brandId, typeId))
        {
        }
    }
}
