using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Pages.WishList
{
    public class IndexModel : PageModel
    {
        private readonly IWishListService _wishlistService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private string _username = null;
        private readonly IWishListViewModelService _wishlistViewModelService;

        public IndexModel(IWishListService wishlistService,
            IWishListViewModelService wishlistViewModelService,
            SignInManager<ApplicationUser> signInManager)
        {
            _wishlistService = wishlistService;
            _signInManager = signInManager;
            _wishlistViewModelService = wishlistViewModelService;
        }

        public WishListViewModel WishListModel { get; set; } = new WishListViewModel();

        public async Task OnGet()
        {
            await SetWishListModelAsync();
        }

        public async Task<IActionResult> OnPost(CatalogItemViewModel productDetails)
        {
            if (productDetails?.Id == null)
            {
                return RedirectToPage("/Index");
            }
            await SetWishListModelAsync();

            await _wishlistService.AddItemToWishList(WishListModel.Id, productDetails.Id);

            await SetWishListModelAsync();

            return RedirectToPage();
        }

        public async Task OnPostUpdate(Dictionary<string, int> items)
        {
            await SetWishListModelAsync();

            await SetWishListModelAsync();
        }

        private async Task SetWishListModelAsync()
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                WishListModel = await _wishlistViewModelService.GetOrCreateWishListForUser(User.Identity.Name);
            }
            else
            {
                GetOrSetWishListCookieAndUserName();
                WishListModel = await _wishlistViewModelService.GetOrCreateWishListForUser(_username);
            }
        }

        private void GetOrSetWishListCookieAndUserName()
        {
            if (Request.Cookies.ContainsKey(Constants.WISHLIST_COOKIENAME))
            {
                _username = Request.Cookies[Constants.WISHLIST_COOKIENAME];
            }
            if (_username != null) return;

            _username = Guid.NewGuid().ToString();
            var cookieOptions = new CookieOptions { IsEssential = true };
            cookieOptions.Expires = DateTime.Today.AddYears(10);
            Response.Cookies.Append(Constants.WISHLIST_COOKIENAME, _username, cookieOptions);
        }
    }
}