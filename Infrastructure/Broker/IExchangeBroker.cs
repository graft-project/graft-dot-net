using Graft.Infrastructure.Watcher;
using System.Threading.Tasks;

namespace Graft.Infrastructure.Broker
{
    public interface IExchangeBroker : IWatchableService
    {
        Task<BrokerSaleResult> Sale(BrokerSaleParams parameters);
        Task<BrokerSaleStatusResult> GetSaleStatus(BrokerSaleStatusParams parameters);
        Task<BrokerParams> GetParams();
    }
}
