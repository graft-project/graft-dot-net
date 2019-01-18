using System.Threading.Tasks;
using WalletRpc.RequestParams;

namespace WalletRpc
{
    public interface IWallet
    {
        Task Create(CreateWalletParams parameters);
        Task<CreateTransferResponse> CreateTransfer(CreateTransferParams parameters);
        Task<string> GetAddress();
        Task<string> GetAddress(GetAddressParams parameters);
        Task<WalletBalance> GetBalance();
        Task<WalletBalance> GetBalance(GetBalanceParams parameters);
        Task<TransferContainer> GetTransferByTxId(string txId);
        Task<Transfers> GetTransfers(GetTransfersParams parameters);
        Task Open(OpenWalletParams parameters);
        Task<TransferResponse> Transfer(TransferParams parameters);
        Task<bool> IsAddressValid(AddressValidRequest address);
    }
}