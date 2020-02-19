using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces
{
    public interface IWishListService
    {
        Task<int> GetWishListItemCountAsync(string userName);
        Task TransferWishListAsync(string anonymousId, string userName);
        Task AddItemToWishList(int wishlistId, int catalogItemId);
        Task DeleteWishListAsync(int wishlistId);
    }
} 