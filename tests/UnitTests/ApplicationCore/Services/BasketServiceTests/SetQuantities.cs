using Microsoft.eShopWeb.ApplicationCore.Exceptions;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Services;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Moq;
using System;
using System.Linq;
using Xunit;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Services.BasketServiceTests
{
    public class SetQuantities
    {
        private readonly int _invalidId = -1;
        private readonly Mock<IAsyncRepository<Basket>> _mockBasketRepo;

        public SetQuantities()
        {
            _mockBasketRepo = new Mock<IAsyncRepository<Basket>>();
        }

        [Fact]
        public async Task ThrowsGivenInvalidBasketId()
        {
            var basketService = new BasketService(_mockBasketRepo.Object, null);

            await Assert.ThrowsAsync<BasketNotFoundException>(async () =>
                await basketService.SetQuantities(
                    _invalidId,
                    new System.Collections.Generic.Dictionary<string, int>()));
        }

        [Fact]
        public async Task ThrowsGivenNullQuantities()
        {
            var basketService = new BasketService(null, null);

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await basketService.SetQuantities(123, null));
        }

        [Fact]
        public async Task Update_ExistingItemQty_Succeeds() {
            var basketId = 10;
            var basket = new Basket();
            var itemId = 1;
            basket.AddItem(itemId, 10, 1);
            var targetItem = basket.Items.First();
            targetItem.Id = itemId;
            // targetItem.Id = itemId;
            _mockBasketRepo.Setup(
                x => x.GetByIdAsync(basketId)).ReturnsAsync(basket);

            var basketService = new BasketService(_mockBasketRepo.Object, null);
            var targetItemQty = 5;
            var quantities = new System.Collections.Generic.Dictionary<string, int>() {
                { itemId.ToString(), targetItemQty }
            };
            await basketService.SetQuantities(
                    basketId,
                    quantities);
            Assert.Equal(targetItemQty, targetItem.Quantity);
            _mockBasketRepo.Verify(x => x.UpdateAsync(basket), Times.Once());
        }

    }
}
