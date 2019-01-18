namespace Graft.Infrastructure.Gateway
{
    public class GatewaySaleParams
    {
        public string PosSn { get; set; }

        public decimal SaleAmount { get; set; }
        public string SaleCurrency { get; set; }
        public string PayCurrency { get; set; }

        public string SaleDetails { get; set; }
    }
}
