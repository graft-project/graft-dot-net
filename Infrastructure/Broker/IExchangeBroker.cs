using Graft.Infrastructure.Watcher;
using System.Threading.Tasks;

namespace Graft.Infrastructure.Broker
{
    public interface IExchangeBroker : IWatchableService
    {
        Task<BrokerExchangeResult> Exchange(BrokerExchangeParams parameters);
        Task<BrokerExchangeResult> ExchangeStatus(BrokerExchangeStatusParams parameters);

        Task<BrokerExchangeResult> ExchangeToStable(BrokerExchangeToStableParams parameters);
        Task<BrokerExchangeResult> ExchangeToStableStatus(BrokerExchangeStatusParams parameters);

        Task<BrokerParams> GetParams();
    }
}
