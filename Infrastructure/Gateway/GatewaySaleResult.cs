namespace Graft.Infrastructure.Gateway
{
    public class GatewaySaleResult
    {
        public string PaymentId { get; set; }
        public string PayCurrency { get; set; }
        public decimal PayAmount { get; set; }
        public string WalletAddress { get; set; }

        public decimal SaleAmount { get; set; }
        public string SaleCurrency { get; set; }

        public decimal PayToSaleRate { get; set; }
        public decimal GraftToSaleRate { get; set; }

        public decimal ServiceProviderFee { get; set; }
        public decimal ExchangeBrokerFee { get; set; }
        public decimal MerchantAmount { get; set; }
    }
}
