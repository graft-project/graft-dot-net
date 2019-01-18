namespace Graft.Infrastructure.Broker
{
    // only one amount must be set, all other must be 0
    public class BrokerExchangeParams
    {
        // fiat equivalent. For example - I want to sell BTC in equivalent to 1$
        // or buy GRFT in equivalent to 1$
        public string FiatCurrency { get; set; }
        
        public decimal SellFiatAmount { get; set; } // how many BTC in fiat equivalent I want to sell
        public decimal BuyFiatAmount { get; set; } // how many GRFT in fiat equivalent I want to buy


        public string SellCurrency { get; set; } // currency (BTC), I want to sell
        public decimal SellAmount { get; set; } // amount BTC, I want to sell

        
        public string BuyCurrency { get; set; } // must be GRFT
        public decimal BuyAmount { get; set; } // how many Grafts I want to buy

        public string WalletAddress { get; set; } // my Graft wallet address
    }
}
