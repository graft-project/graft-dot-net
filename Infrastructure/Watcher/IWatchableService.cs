using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Graft.Infrastructure.Watcher
{
    public interface IWatchableService
    {
        string Name { get; }
        DateTime LastOperationTime { get; }
        WatchableServiceState State { get; }
        Dictionary<string, string> Parameters { get; }
        Dictionary<string, string> Metrics { get; }
        List<StateChangeItem> StateChangeHistory { get; }

        Task Ping();
    }
}
