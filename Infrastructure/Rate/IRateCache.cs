using Graft.Infrastructure.Watcher;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Graft.Infrastructure.Rate
{
    public interface IRateCache : IWatchableService
    {
        void Add(RateCurrency currency);
        Task<decimal> GetRateToUsd(string currencyCode);
        IEnumerable<RateCurrency> GetSupportedCurrencies();
        bool IsSupported(string currencyCode);
    }
}