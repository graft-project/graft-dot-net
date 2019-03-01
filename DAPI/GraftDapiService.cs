//using Graft.Infrastructure;
//using Graft.Infrastructure.Watcher;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Diagnostics;
//using System.Threading.Tasks;

//namespace Graft.DAPI
//{
//    public class GraftDapiService : WatchableService, IGraftDapiService
//    {
//        readonly DapiConfiguration _settings;
//        readonly GraftDapi _dapi;

//        public GraftDapiService(
//            ILoggerFactory loggerFactory,
//            IEmailSender emailService,
//            IConfiguration configuration)
//            : base(nameof(GraftDapi), "GRAFT DAPI", loggerFactory, emailService, configuration)
//        {
//            _settings = configuration
//                .GetSection("DAPI")
//                .Get<DapiConfiguration>();

//            _dapi = new GraftDapi(_settings.Url);

//            Parameters["URL"] = _settings.Url;
//        }

//        public Task<DapiSaleResult> Sale(DapiSaleParams parameters)
//        {
//            return MeasureMethod(() => _dapi.Sale(parameters));
//        }

//        public Task<DapiSaleStatusResult> GetSaleStatus(DapiSaleStatusParams parameters)
//        {
//            return MeasureMethod(() => _dapi.GetSaleStatus(parameters));
//        }

//        async Task<TResult> MeasureMethod<TResult>(Func<Task<TResult>> func)
//        {
//            var sw = new Stopwatch();
//            sw.Start();

//            try
//            {
//                var res = await func();

//                if (State != WatchableServiceState.OK)
//                    SetState(WatchableServiceState.OK);

//                return res;
//            }
//            catch (Exception ex)
//            {
//                SetState(WatchableServiceState.Error, ex.Message);
//                throw;
//            }
//            finally
//            {
//                sw.Stop();
//                LastOperationTime = DateTime.UtcNow;
//                UpdateStopwatchMetrics(sw, State == WatchableServiceState.OK);
//            }
//        }

//        public override Task Ping()
//        {
//            //todo - implement ping method for GRAFT DAPI
//            return Task.CompletedTask;
//        }
//    }
//}
