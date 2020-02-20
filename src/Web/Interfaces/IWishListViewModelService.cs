using Microsoft.eShopWeb.Web.Pages.WishList;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Interfaces
{
    public interface IWishListViewModelService
    {
        Task<WishListViewModel> GetOrCreateWishListForUser(string userName);
    }
} 