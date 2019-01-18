using Graft.Infrastructure.Watcher;
using System.Threading.Tasks;

namespace Graft.DAPI
{
    public interface IGraftDapiService : IWatchableService
    {
        Task<DapiSaleStatusResult> GetSaleStatus(DapiSaleStatusParams parameters);
        Task<DapiSaleResult> Sale(DapiSaleParams parameters);
    }
}