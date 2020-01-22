using System.Threading;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Infrastructure.Services.CurrencyService
{
    public class CurrencyServiceStatic : ICurrencyService
    {
        /// <inheritdoc />
        public Task<decimal> Convert(decimal value, Currency source, Currency target, CancellationToken cancellationToken = default)
        {
            var convertedValue = value * 1.25m; // TODO: Apply conversion
            return Task.FromResult(convertedValue);
        }
    }
}