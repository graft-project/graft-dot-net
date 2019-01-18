using System;

namespace Graft.Infrastructure.Gateway
{
    public class GatewayParams
    {
        public string AppVersion { get; set; }
        public Uri BrokerUri { get; set; }

        public Broker.BrokerParams BrokerParams { get; set; }
    }
}
