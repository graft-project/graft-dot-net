using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Graft.Infrastructure.AccountPool
{
    public interface IAccountPool
    {
        Task<IEnumerable<AccountPoolItem>> GetInactiveAccount(string currencyType);

        Task WriteNewAccount(AccountPoolItem accountPoolItem);

        Task<AccountPoolItem> SetInactive(string hash, string transactionHash, decimal addedValue);

        Task<AccountPoolItem> SetInactive(AccountPoolItem accountPoolItem, string transactionHash);

        Task SetActive(string hash);

        Task SetActive(AccountPoolItem accountPoolItem);

        Task ClearBalance(string hash);
    }
}
