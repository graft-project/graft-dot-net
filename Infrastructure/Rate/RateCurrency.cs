namespace Graft.Infrastructure.Rate
{
    public class RateCurrency 
    {
        public int Validity { get; set; }
        public int TolerancePeriod { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public int Ticker { get; set; }
    }
}
