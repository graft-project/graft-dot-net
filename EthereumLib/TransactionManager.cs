using EthereumLib.Models;
using Graft.Infrastructure.AccountPool;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EthereumLib
{
    public class TransactionManager
    {
        public const string CurrencyCode = "ETH";

        private const string RopsteinApiLink = "http://api-ropsten.etherscan.io/";
        private const string MainNetApiLink = "http://api.etherscan.io/";
        private const string TransactionListLink = "api?module=account&action=txlist&address={0}&startblock=0&endblock=99999999&sort=asc";

        private const decimal drainLimit = 0.001M;
        private const int AccountUnlockDurationInSeconds = 60;
        private bool isTestnet;
        private string defaultAccountPassword;
        private string gethNodeAddress;
        private string drainAddress;
        private decimal drainValue;

        private string TransactionListUri
        {
            get
            {
                return (isTestnet ? RopsteinApiLink : MainNetApiLink) + TransactionListLink;
            }
        }

        private IAccountPool addressPool;

        public TransactionManager(IAccountPool addressPool, bool isTestnet, string defaultAccountPassword, string gethNodeAddress, string drainAddress, decimal drainValue)
        {
            this.isTestnet = isTestnet;
            this.addressPool = addressPool;
            this.defaultAccountPassword = defaultAccountPassword;
            this.gethNodeAddress = string.IsNullOrEmpty(gethNodeAddress) ? "http://localhost:8545" : gethNodeAddress;
            this.drainAddress = drainAddress;
            this.drainValue = drainValue;
        }

        public Task<AccountPoolItem> GetPoolOrNewAddress()
        {
            return GetPoolOrNewAddress(new TimeSpan(0));
        }

        public async Task<AccountPoolItem> GetPoolOrNewAddress(TimeSpan timeout, int tryCount = 2, int delayMS = 1000)
        {
            AccountPoolItem result = null;

            for (int i = 0; i < tryCount; i++)
            {
                try
                {
                    var data = await addressPool.GetInactiveAccount(CurrencyCode);

                    if (!data.Any())
                    {
                        await Task.Delay(timeout);

                        data = await addressPool.GetInactiveAccount(CurrencyCode);
                    }

                    if (!data.Any())
                    {
                        var account = await CreateNewAccount();

                        result = new AccountPoolItem
                        {
                            Id = Guid.NewGuid().ToString(),
                            Address = account,
                            Balance = 0,
                            CurrencyName = CurrencyCode,
                            IsProcessed = true,
                            LastTransactionHash = String.Empty
                        };

                        await addressPool.WriteNewAccount(result);
                        break;
                    }
                    else
                    {
                        result = data.First();

                        await addressPool.SetActive(result);
                        break;
                    }
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }

                await Task.Delay(new TimeSpan(0, 0, 0, 0, delayMS));
            }

            return result;
        }

        public async Task ReturnAddressToPool(string hash, string transactionHash, decimal amount)
        {
            var poolItem = await addressPool.SetInactive(hash, transactionHash, amount);

            await DrainPoolItem(poolItem);
        }

        public async Task ReturnAddressToPool(AccountPoolItem accountPoolItem, string transactionHash)
        {
            var poolItem = await addressPool.SetInactive(accountPoolItem, transactionHash);

            await DrainPoolItem(poolItem);
        }

        public async Task<GetTransferByAccountResponse> GetTransactionsByAddress(string address)
        {
            return await GetTransactionsByAddressEtherscan(address);
        }

        private async Task DrainPoolItem(AccountPoolItem poolItem)
        {
            if (poolItem == null)
            {
                return;
            }

            if (poolItem.IsProcessed)
            {
                return;
            }

            if (string.IsNullOrEmpty(drainAddress))
            {
                return;
            }

            if (!string.Equals(poolItem.CurrencyName, CurrencyCode, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            if (poolItem.Balance > drainValue && poolItem.Balance > drainLimit)
            {

                try
                {
                    var web3 = new Nethereum.Web3.Web3(gethNodeAddress);

                    var sendBalance = Converter.DecimalToAtomicUnit(poolItem.Balance - drainLimit);

                    await web3.Personal.UnlockAccount.SendRequestAsync(poolItem.Address, defaultAccountPassword, AccountUnlockDurationInSeconds).ConfigureAwait(false);

                    await web3.TransactionManager.SendTransactionAsync(poolItem.Address, drainAddress, sendBalance).ConfigureAwait(false);

                    await addressPool.ClearBalance(poolItem.Address).ConfigureAwait(false);

                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
            }
        }

        private async Task<GetTransferByAccountResponse> GetTransactionsByAddressEtherscan(string address, int tryCount = 2, int delayMS = 1000)
        {
            GetTransferByAccountResponse result = null;

            for (int i = 0; i < tryCount; i++)
            {
                try
                {
                    var requestUrl = string.Format(TransactionListUri, address);

                    using (var client = new HttpClient())
                    {
                        var response = await client.GetStringAsync(requestUrl);

                        if (!string.IsNullOrEmpty(response))
                        {
                            result = GetTransferByAccountResponse.FromJson(response);
                            break;
                        }
                    }
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }

                await Task.Delay(new TimeSpan(0, 0, 0, 0, delayMS));
            }

            return result;
        }

        private async Task<string> CreateNewAccount()
        {
            string result = null;

            for (int i = 0; i < 3; i++)
            {
                try
                {
                    var web3 = new Nethereum.Web3.Web3(gethNodeAddress);
                    result = await web3.Personal.NewAccount.SendRequestAsync(defaultAccountPassword);
                    break;
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }

                await Task.Delay(new TimeSpan(0, 0, 5));
            }

            return result;
        }
    }
}
