using System;

namespace Graft.Infrastructure
{
    public class EventItem
    {
        public DateTime Time { get; set; } = DateTime.UtcNow;
        public string Message { get; set; }

        public EventItem(string message)
        {
            Message = message;
        }
    }
}