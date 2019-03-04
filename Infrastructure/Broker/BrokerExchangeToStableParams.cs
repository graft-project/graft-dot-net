namespace Graft.Infrastructure.Broker
{
    public class BrokerExchangeToStableParams
    {
        public string ExchangeId { get; set; }

        public string SellCurrency { get; set; } // must be GRFT
        public decimal SellAmount { get; set; } // amount GRFT, I want to sell

        public string WalletAddress { get; set; } // my Graft wallet address
    }
}
