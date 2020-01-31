using System;
using Microsoft.eShopWeb.Web;
using Microsoft.eShopWeb.Web.Extensions;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.Web.Extensions.CacheHelpersTests
{
    public class GenerateCatalogItemCacheKey_Should
    {
        [Theory]
        [InlineData(0, Constants.ITEMS_PER_PAGE, null, null, null, "items-0-10---")]
        [InlineData(5, 20, null, null, null, "items-5-20---")]
        [InlineData(-5, 20, null, null, null, null, typeof(InvalidPageIndexException))]
        public void ReturnCatalogItemCacheKey(
            int pageIndex,
            int itemPerPage,
            string searchText,
            int? brandId,
            int? typeId,
            string expectedResult,
            Type exceptionType = null
        )
        {
            if (string.IsNullOrEmpty(expectedResult)) {
                if (exceptionType == null) {
                    throw new Exception("Missing exception type to check");
                }
                Assert.Throws(
                    exceptionType,
                    () => CacheHelpers.GenerateCatalogItemCacheKey(
                    pageIndex, itemPerPage, searchText, brandId, typeId));
            }
            else 
            {
                var result = CacheHelpers.GenerateCatalogItemCacheKey(
                    pageIndex, itemPerPage, searchText, brandId, typeId);

                Assert.Equal(expectedResult, result);
            }
        }
    }
}
