using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces
{
    public enum Currency {
        USD,
        EUR,
        GBP
    }

    /// <summary>
    /// Abstraction for converting monetary values
    /// </summary>
    public interface ICurrencyService
    {
        /// <summary>
        /// Convert monenatary values from source to target currency
        /// </summary>
        /// <param name="value">Monetary value</param>
        /// <param name="source">Source currency</param>
        /// <param name="target">Target currency</param>
        /// <param name="cancellationToken">Token used to cancel the operation</param>
        /// <returns></returns>
        Task<decimal> Convert(
            decimal value, Currency source, Currency target,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
