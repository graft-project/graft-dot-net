using System;

namespace Graft.Infrastructure.Watcher
{
    public class StateChangeItem
    {
        public DateTime Time { get; set; }
        public WatchableServiceState OldState { get; set; }
        public WatchableServiceState NewState { get; set; }
        public string Message { get; set; }
    }
}
