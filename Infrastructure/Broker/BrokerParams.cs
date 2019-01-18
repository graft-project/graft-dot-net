using Graft.Infrastructure.Rate;
using System.Collections.Generic;

namespace Graft.Infrastructure.Broker
{
    public class BrokerParams
    {
        public string Version { get; set; }
        public string Network { get; set; }
        public decimal Fee { get; set; }

        public IEnumerable<string> Currencies { get; set; }
        public IEnumerable<RateCurrency> Cryptocurrencies { get; set; }
    }
}
