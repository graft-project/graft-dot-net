using System.Collections.Generic;

namespace Graft.Infrastructure.Broker
{
    public class BrokerExchangeResult
    {
        public string ExchangeId { get; set; }
        public PaymentStatus Status { get; set; }

        public string SellCurrency { get; set; } // currency (BTC), I want to sell
        public decimal SellAmount { get; set; } // amount BTC, I need to pay

        public string BuyCurrency { get; set; } // GRFT
        public decimal BuyAmount { get; set; } // how many Grafts I will get

        public decimal SellToUsdRate { get; set; } // rate BTC to USD
        public decimal GraftToUsdRate { get; set; } // rate GRFT to USD

        public decimal ExchangeBrokerFeeRate { get; set; } // 0.005 (0.5%)
        public decimal ExchangeBrokerFeeAmount { get; set; } // fee amount

        public string PayWalletAddress { get; set; }

        public List<EventItem> ProcessingEvents { get; set; }
    }
}
