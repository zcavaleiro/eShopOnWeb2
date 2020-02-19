using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using System.Linq;
using Ardalis.GuardClauses;
using ApplicationCore.Entities.WishListAggregate;

namespace Microsoft.eShopWeb.ApplicationCore.Services
{
    public class WishListService : IWishListService
    {
        private readonly IAsyncRepository<WishList> _wishlistRepository;
        private readonly IAppLogger<WishListService> _logger;

        public WishListService(IAsyncRepository<WishList> wishlistRepository,
            IAppLogger<WishListService> logger)
        {
            _wishlistRepository = wishlistRepository;
            _logger = logger;
        }

        public async Task AddItemToWishList(int wishlistId, int catalogItemId)
        {
            var wishlist = await _wishlistRepository.GetByIdAsync(wishlistId);

            wishlist.AddItem(catalogItemId);

            await _wishlistRepository.UpdateAsync(wishlist);
        }

        public async Task DeleteWishListAsync(int wishlistId)
        {
            var wishlist = await _wishlistRepository.GetByIdAsync(wishlistId);
            await _wishlistRepository.DeleteAsync(wishlist);
        }

        public async Task<int> GetWishListItemCountAsync(string userName)
        {
            Guard.Against.NullOrEmpty(userName, nameof(userName));
            var wishlistSpec = new WishListWithItemsSpecification(userName);
            var wishlist = (await _wishlistRepository.ListAsync(wishlistSpec)).FirstOrDefault();
            if (wishlist == null)
            {
                _logger.LogInformation($"No wishlist found for {userName}");
                return 0;
            }
            int count = wishlist.Items.Sum(i => i.Quantity);
            _logger.LogInformation($" WishList for {userName} has {count} items.");
            return count;
        }

        public async Task TransferWishListAsync(string anonymousId, string userName)
        {
            Guard.Against.NullOrEmpty(anonymousId, nameof(anonymousId));
            Guard.Against.NullOrEmpty(userName, nameof(userName));
            var wishlistSpec = new WishListWithItemsSpecification(anonymousId);
            var wishlist = (await _wishlistRepository.ListAsync(wishlistSpec)).FirstOrDefault();
            if (wishlist == null) return;
            wishlist.BuyerId = userName;
            await _wishlistRepository.UpdateAsync(wishlist);
        }
    }
} 