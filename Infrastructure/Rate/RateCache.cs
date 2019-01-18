using Graft.Infrastructure.Watcher;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Graft.Infrastructure.Rate
{
    public class RateCache : WatchableService, IRateCache
    {
        readonly Dictionary<string, RatePresenter> _presenters = new Dictionary<string, RatePresenter>();
        readonly RateCurrency[] _supportedCryptocurrencies;

        public RateCache(
            ILoggerFactory loggerFactory, 
            IEmailSender emailService, 
            IConfiguration configuration)
            : base(nameof(RateCache), "Rate Cache", loggerFactory, emailService, configuration)
        {
            _supportedCryptocurrencies = configuration
                .GetSection("RateCache:SupportedCryptocurrencies")
                .Get<RateCurrency[]>();

            foreach (var currency in _supportedCryptocurrencies)
                Add(currency);
        }

        public void Add(RateCurrency currency)
        {
            if (!_presenters.ContainsKey(currency.CurrencyCode))
            {
                _presenters[currency.CurrencyCode] = new RatePresenter(currency);
                Parameters[currency.CurrencyCode] = $"Validitity - {currency.Validity} ms";
            }
        }

        public async Task<decimal> GetRateToUsd(string currencyCode)
        {
            var sw = new Stopwatch();
            sw.Start();

            try
            {
                var rate = await _presenters[currencyCode].GetRateToUsd();

                if (State != WatchableServiceState.OK)
                    SetState(WatchableServiceState.OK);

                Metrics[$"Rate {currencyCode}"] = rate.ToString();

                return rate;
            }
            catch (Exception ex)
            {
                SetState(WatchableServiceState.Error, ex);
                throw;
            }
            finally
            {
                sw.Stop();
                LastOperationTime = DateTime.UtcNow;
                UpdateStopwatchMetrics(sw, State == WatchableServiceState.OK);
            }
        }

        public bool IsSupported(string currencyCode)
        {
            return _presenters.ContainsKey(currencyCode);
        }

        public IEnumerable<RateCurrency> GetSupportedCurrencies()
        {
            return _supportedCryptocurrencies;
        }

        public override async Task Ping()
        {
            try
            {
                var tasks = new Task[3];
                tasks[0] = GetRateToUsd("BTC");
                tasks[1] = GetRateToUsd("GRFT");
                tasks[2] = GetRateToUsd("ETH");
                await Task.WhenAll(tasks);
            }
            catch (Exception)
            {
            }
        }
    }
}
