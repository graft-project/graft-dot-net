using Graft.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WalletRpc.RequestParams;

namespace WalletRpc
{
    public class Wallet : IWallet
    {
        HttpClient client;

        public string Address { get; private set; }
        public WalletBalance RawBalance { get; private set; }

        public DateTime LastPayTime { get; set; } = DateTime.MinValue;
        public DateTime LastReceiveTime { get; set; } = DateTime.MinValue;

        public decimal Balance => GraftConvert.FromAtomicUnits(RawBalance?.Balance ?? -0);
        public decimal UnlockedBalance => GraftConvert.FromAtomicUnits(RawBalance?.UnlockedBalance ?? 0);


        public Wallet(string uri, string username, string password)
        {
            //var credCache = new CredentialCache
            //{
            //    { new Uri(uri), "Digest", new NetworkCredential("test", "test") }
            //};

            //client = new HttpClient(new HttpClientHandler { Credentials = credCache })
            //{
            //    BaseAddress = new Uri(uri)
            //};

            client = new HttpClient()
            {
                BaseAddress = new Uri(uri)
            };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }


        async Task<TResult> PostAsync<TResult, TParams>(string method, TParams parameters)
        {
            HttpResponseMessage response = null;

            try
            {
                var request = new RpcRequest<TParams>(method, parameters);
                var json = JsonConvert.SerializeObject(request);
                response = await client.PostAsync("json_rpc", new StringContent(json));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new ApiException(ErrorCode.NoConnectionToRpc);
            }

            var strResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<RpcResponse<TResult>>(strResult);

            if (result.Error != null)
                throw new ApiException(ErrorCode.RpcError, $"Code: {result.Error.Code}, Message: {result.Error.Message}");

            response.EnsureSuccessStatusCode();
            return result.Result;
        }

        public Task<CreateTransferResponse> CreateTransfer(CreateTransferParams parameters)
        {
            LastPayTime = DateTime.UtcNow;
            return PostAsync<CreateTransferResponse, CreateTransferParams>("transfer", parameters);
        }

        public Task<TransferResponse> Transfer(TransferParams parameters)
        {
            LastPayTime = DateTime.UtcNow;
            return PostAsync<TransferResponse, TransferParams>("transfer", parameters);
        }

        public Task<TransferResponse> TransferRta(TransferParams parameters)
        {
            LastPayTime = DateTime.UtcNow;
            return PostAsync<TransferResponse, TransferParams>("transfer_rta", parameters);
        }

        public Task<TransferContainer> GetTransferByTxId(string txId)
        {
            return PostAsync<TransferContainer, GetTransferByTxIdParams>("get_transfer_by_txid",
                new GetTransferByTxIdParams
                {
                    TxId = txId
                });
        }

        public Task<Transfers> GetTransfers(GetTransfersParams parameters)
        {
            return PostAsync<Transfers, GetTransfersParams>("get_transfers", parameters);
        }

        public Task Create(CreateWalletParams parameters)
        {
            return PostAsync<EmptyResponse, CreateWalletParams>("create_wallet", parameters);
        }

        public Task Open(OpenWalletParams parameters)
        {
            return PostAsync<EmptyResponse, OpenWalletParams>("open_wallet", parameters);
        }

        public async Task<string> GetAddress(GetAddressParams parameters)
        {
            var res = await PostAsync<WalletAddress, GetAddressParams>("getaddress", parameters);
            Address = res.Address;
            return Address;
        }

        public Task<string> GetAddress()
        {
            return GetAddress(GetAddressParams.Default);
        }

        public async Task<WalletBalance> GetBalance(GetBalanceParams parameters)
        {
            RawBalance = await PostAsync<WalletBalance, GetBalanceParams>("getbalance", parameters);
            return RawBalance;
        }

        public Task<WalletBalance> GetBalance()
        {
            return GetBalance(GetBalanceParams.Default);
        }

        public async Task<bool> IsAddressValid(AddressValidRequest address)
        {
            bool result = false;
            try
            {
                await PostAsync<RequestResults.AddressIndex, AddressValidRequest>("add_address_book", address);
                result = true;
            }
            catch
            {
                //Address invalid
            }
            return result;
        }
    }
}
