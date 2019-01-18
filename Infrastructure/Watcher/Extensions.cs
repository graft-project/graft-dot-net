using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Graft.Infrastructure.Watcher
{
    public static class Extensions
    {
        public static void AddWatcher(this IServiceCollection services, Action<WatcherServiceOptions> configureOptions)
        {
            configureOptions(WatcherServiceOptions.Current);

            // https://github.com/aspnet/Hosting/issues/1489
            //services.AddHostedService<WatcherService>();
            var watcher = new WatcherService();
            services.AddSingleton<IHostedService>(watcher);
            services.AddSingleton(watcher);
        }
    }
}
