namespace Graft.Infrastructure.Broker
{
    public class BrokerSaleResult
    {
        public string PaymentId { get; set; }
        public decimal SaleAmount { get; set; }
        public string SaleCurrency { get; set; }
        public decimal PayToSaleRate { get; set; }
        public decimal GraftToSaleRate { get; set; }
        public string PayCurrency { get; set; }
        public decimal PayAmount { get; set; }
        public decimal ServiceProviderFee { get; set; }
        public decimal ExchangeBrokerFee { get; set; }
        public decimal MerchantAmount { get; set; }

        public string PayWalletAddress { get; set; }
    }
}
