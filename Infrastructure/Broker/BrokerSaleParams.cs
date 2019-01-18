namespace Graft.Infrastructure.Broker
{
    public class BrokerSaleParams
    {
        public string PaymentId { get; set; }
        public decimal SaleAmount { get; set; }
        public string SaleCurrency { get; set; }
        public string PayCurrency { get; set; }
        public double ServiceProviderFee { get; set; }
        public string ServiceProviderWallet { get; set; }
        public string MerchantWallet { get; set; }
    }
}
