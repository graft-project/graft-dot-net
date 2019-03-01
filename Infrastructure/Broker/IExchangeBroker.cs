using Graft.Infrastructure.Watcher;
using System.Threading.Tasks;

namespace Graft.Infrastructure.Broker
{
    public interface IExchangeBroker : IWatchableService
    {
        Task<BrokerExchangeResult> Sale(BrokerExchangeParams parameters);
        Task<BrokerExchangeResult> GetSaleStatus(BrokerExchangeStatusParams parameters);
        Task<BrokerParams> GetParams();
    }
}
