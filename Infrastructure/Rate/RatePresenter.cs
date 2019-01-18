using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Graft.Infrastructure.Rate
{
    public class RatePresenter
    {
        static HttpClient _client = new HttpClient();

        readonly string _ticker;
        readonly TimeSpan _validityPeriod;
        readonly TimeSpan _tolerancePeriod;
        readonly object _sync = new object();

        DateTime _lastTimestamp;
        decimal _lastRate;
        TaskCompletionSource<decimal> _tcs;

        static RatePresenter()
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri("https://api.coinmarketcap.com/v2/ticker/"),
                Timeout = TimeSpan.FromSeconds(10)
            };
        }

        public RatePresenter(RateCurrency currency)
        {
            _validityPeriod = TimeSpan.FromMilliseconds(currency.Validity);
            _tolerancePeriod = TimeSpan.FromMilliseconds(currency.TolerancePeriod);
            _ticker = currency.Ticker.ToString();
        }

        public Task<decimal> GetRateToUsd()
        {
            if (DateTime.UtcNow - _lastTimestamp > _validityPeriod)
            {
                lock (_sync)
                {
                    if (DateTime.UtcNow - _lastTimestamp > _validityPeriod)
                    {
                        if (_tcs == null)
                        {
                            _tcs = new TaskCompletionSource<decimal>();
                            RunQueryAsync();
                        }
                        return _tcs.Task;
                    }
                }
            }
            return Task.FromResult(_lastRate);
        }

        async void RunQueryAsync()
        {
            try
            {
                var res = await _client.GetAsync(_ticker);
                res.EnsureSuccessStatusCode();
                var rateResponse = await res.Content.ReadAsAsync<RateResponse>();

                lock (_sync)
                {
                    _lastTimestamp = DateTime.UtcNow;
                    _lastRate = rateResponse.Data.Quotes.USD.price;
                    _tcs.SetResult(_lastRate);
                    _tcs = null;
                }
            }
            catch(Exception ex)
            {
                lock (_sync)
                {
                    if (DateTime.UtcNow - _lastTimestamp > _tolerancePeriod)
                        _tcs.SetException(ex);
                    else
                        _tcs.SetResult(_lastRate);

                    _tcs = null;
                }
            }
        }
    }
}
