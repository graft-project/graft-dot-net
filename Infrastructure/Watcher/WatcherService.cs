using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Graft.Infrastructure.Watcher
{
    public class WatcherService : IHostedService, IDisposable
    {
        readonly List<IWatchableService> _services = new List<IWatchableService>();
        readonly List<string> _errors = new List<string>();
        TimeSpan _checkPeriod;
        Timer _timer;

        public IEnumerable<IWatchableService> Services => _services;
        public IEnumerable<string> Errors => _errors;

        public WatcherService()
        {
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _checkPeriod = TimeSpan.FromMilliseconds(WatcherServiceOptions.Current.CheckPeriodMs);
            _timer = new Timer(DoWork, null, TimeSpan.Zero, _checkPeriod);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public WatcherService Add(IWatchableService service)
        {
            _services.Add(service);
            return this;
        }

        void DoWork(object state)
        {
            foreach (var item in _services.Where(t => (DateTime.UtcNow - t.LastOperationTime) > _checkPeriod))
            {
                item.Ping().ContinueWith(t =>
                    {
                        _errors.Add($"{DateTime.UtcNow} {item.Name}: {t.Exception.InnerException?.Message ?? t.Exception.Message}");
                    }, TaskContinuationOptions.OnlyOnFaulted)
                    .ConfigureAwait(false);
            }
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
