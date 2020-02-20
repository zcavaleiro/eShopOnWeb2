using Microsoft.eShopWeb;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Services;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.eShopWeb.Web.Services
{
    public static class CatalogExtensions
    {
        /// <summary>
        /// Add catalog services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddCatalogServices(this IServiceCollection services, IConfiguration configuration) {
            services.AddScoped<ICatalogViewModelService, CachedCatalogViewModelService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IBasketViewModelService, BasketViewModelService>();
            services.AddScoped<IWishListService, WishListService>();
            services.AddScoped<IWishListViewModelService, WishListViewModelService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<CatalogViewModelService>();
            services.AddScoped<ICatalogItemViewModelService, CatalogItemViewModelService>();
            services.Configure<CatalogSettings>(configuration);
            services.AddSingleton<IUriComposer>(new UriComposer(configuration.Get<CatalogSettings>()));
            
        }
        
    }
}