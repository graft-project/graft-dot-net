using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletRpc
{
    public class WalletPool
    {
        List<Wallet> _wallets = new List<Wallet>();
        public IEnumerable<Wallet> Wallets => _wallets;

        public WalletPool(IEnumerable<WalletConfig> wallets)
        {
            foreach (var item in wallets)
            {
                var wallet = new Wallet(item.Url, "", "");
                _wallets.Add(wallet);
            }
        }

        public async Task InitAll()
        {
            var tasks = _wallets.Select(async w =>
            {
                await w.GetAddress();
                await w.GetBalance();
            });

            await Task.WhenAll(tasks);
        }

        public async Task UpdateBalances()
        {
            var tasks = _wallets.Select(async w =>
            {
                await w.GetBalance();
            });

            await Task.WhenAll(tasks);
        }

        public Wallet GetPayWallet(decimal amount)
        {
            return _wallets.OrderBy(w => w.LastPayTime)
                .Where(w => w.UnlockedBalance >= amount)
                .FirstOrDefault();
        }

        public Wallet GetRecvWallet(string excludeAddress)
        {
            return _wallets.OrderBy(w => w.LastReceiveTime)
                .Where(w => w.Address != excludeAddress)
                .FirstOrDefault();
        }

    }
}
