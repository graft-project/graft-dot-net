using Graft.Infrastructure.Models;
using Graft.Infrastructure.Watcher;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Graft.Infrastructure.Broker
{
    public class ExchangeBroker : WatchableService, IExchangeBroker
    {
        readonly ExchangeBrokerConfiguration _settings;
        static HttpClient _client;

        public ExchangeBroker(ILoggerFactory loggerFactory,
            IEmailSender emailService,
            IConfiguration configuration)
            : base(nameof(ExchangeBroker), "Exchange Broker", loggerFactory, emailService, configuration)
        {
            _settings = configuration
                .GetSection("ExchangeBroker")
                .Get<ExchangeBrokerConfiguration>();

            _client = new HttpClient
            {
                BaseAddress = new Uri(_settings.Url)
            };

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            Parameters["URL"] = _settings.Url;
        }

        public Task<BrokerExchangeResult> CalcExchange(BrokerExchangeParams parameters)
        {
            return MeasurePost<BrokerExchangeResult, BrokerExchangeParams>("CalcExchange", parameters);
        }

        public Task<BrokerExchangeResult> Exchange(BrokerExchangeParams parameters)
        {
            return MeasurePost<BrokerExchangeResult, BrokerExchangeParams>("Exchange", parameters);
        }

        public Task<BrokerExchangeResult> ExchangeStatus(BrokerExchangeStatusParams parameters)
        {
            return MeasurePost<BrokerExchangeResult, BrokerExchangeStatusParams>("ExchangeStatus", parameters);
        }

        public Task<BrokerExchangeResult> ExchangeToStable(BrokerExchangeToStableParams parameters)
        {
            return MeasurePost<BrokerExchangeResult, BrokerExchangeToStableParams>("ExchangeToStable", parameters);
        }

        public Task<BrokerExchangeResult> ExchangeToStableStatus(BrokerExchangeStatusParams parameters)
        {
            return MeasurePost<BrokerExchangeResult, BrokerExchangeStatusParams>("ExchangeToStableStatus", parameters);
        }

        public Task<BrokerParams> GetParams()
        {
            return MeasureGet<BrokerParams>("GetParams");
        }

        async Task<TResult> MeasurePost<TResult, TParams>(string method, TParams parameters)
        {
            var sw = new Stopwatch();
            sw.Start();

            try
            {
                var res = await PostAsync<TResult, TParams>(method, parameters);

                if (State != WatchableServiceState.OK)
                    SetState(WatchableServiceState.OK);

                return res;
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

        async Task<TResult> MeasureGet<TResult>(string uri)
        {
            var sw = new Stopwatch();
            sw.Start();

            try
            {
                var res = await GetAsync<TResult>(uri);

                if (State != WatchableServiceState.OK)
                    SetState(WatchableServiceState.OK);

                return res;
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

        async Task<TResult> GetAsync<TResult>(string uri)
        {
            HttpResponseMessage response = null;

            try
            {
                response = await _client.GetAsync(uri);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new ApiException(ErrorCode.NoConnectionToBroker);
            }

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var errorResult = await response.Content.ReadAsAsync<ApiErrorResult>();
                throw new ApiException(errorResult.Error);
            }

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsAsync<TResult>();
            return result;
        }

        async Task<TResult> PostAsync<TResult, TParams>(string uri, TParams parameters)
        {
            HttpResponseMessage response = null;

            try
            {
                response = await _client.PostAsJsonAsync(uri, parameters);
            }
            catch (Exception)
            {
                throw new ApiException(ErrorCode.NoConnectionToBroker);
            }

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var errorResult = await response.Content.ReadAsAsync<ApiErrorResult>();
                throw new ApiException(errorResult.Error);
            }

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsAsync<TResult>();
            return result;
        }

        public async override Task Ping()
        {
            var parameters = await GetParams();
            Metrics["Broker Parameters"] = JsonConvert.SerializeObject(parameters);
        }
    }
}
