using System;

namespace Graft.Infrastructure.Watcher
{
    public class WatcherServiceOptions
    {
        static WatcherServiceOptions _current = new WatcherServiceOptions();

        public static WatcherServiceOptions Current
        {
            get => _current;
            set => _current = value ?? throw new ArgumentNullException(nameof(Current), "WatcherServiceOptions must be set");
        }

        public int CheckPeriodMs { get; set; } = 60_000;
    }
}
