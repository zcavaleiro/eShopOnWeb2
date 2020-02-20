using ApplicationCore.Entities.WishListAggregate;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.Pages.WishList;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Services
{
    public class WishListViewModelService : IWishListViewModelService
    {
        private readonly IAsyncRepository<WishList> _wishlistRepository;
        private readonly IUriComposer _uriComposer;
        private readonly IAsyncRepository<CatalogItem> _itemRepository;

        public WishListViewModelService(IAsyncRepository<WishList> wishlistRepository,
            IAsyncRepository<CatalogItem> itemRepository,
            IUriComposer uriComposer)
        {
            _wishlistRepository = wishlistRepository;
            _uriComposer = uriComposer;
            _itemRepository = itemRepository;
        }

        public async Task<WishListViewModel> GetOrCreateWishListForUser(string userName)
        {
            var wishlistSpec = new WishListWithItemsSpecification(userName);
            var wishlist = (await _wishlistRepository.ListAsync(wishlistSpec)).FirstOrDefault();

            if (wishlist == null)
            {
                return await CreateWishListForUser(userName);
            }
            return await CreateViewModelFromWishList(wishlist);
        }

        private async Task<WishListViewModel> CreateViewModelFromWishList(WishList wishlist)
        {
            var viewModel = new WishListViewModel();
            viewModel.Id = wishlist.Id;
            viewModel.BuyerId = wishlist.BuyerId;
            viewModel.Items = await GetWishListItems(wishlist.Items); ;
            return viewModel;
        }

        private async Task<WishListViewModel> CreateWishListForUser(string userId)
        {
            var wishlist = new WishList() { BuyerId = userId };
            await _wishlistRepository.AddAsync(wishlist);

            return new WishListViewModel()
            {
                BuyerId = wishlist.BuyerId,
                Id = wishlist.Id,
                Items = new List<WishListItemViewModel>()
            };
        }

        private async Task<List<WishListItemViewModel>> GetWishListItems(IReadOnlyCollection<WishListItem> wishlistItems)
        {
            var items = new List<WishListItemViewModel>();
            foreach (var item in wishlistItems)
            {
                var itemModel = new WishListItemViewModel
                {
                    Id = item.Id,
                    CatalogItemId = item.CatalogItemId
                };
                var catalogItem = await _itemRepository.GetByIdAsync(item.CatalogItemId);
                itemModel.PictureUrl = _uriComposer.ComposePicUri(catalogItem.PictureUri);
                itemModel.ProductName = catalogItem.Name;
                items.Add(itemModel);
            }

            return items;
        }
    }
}