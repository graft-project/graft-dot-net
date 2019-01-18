namespace Graft.Infrastructure.Rate
{
    public class RateResponseQuote
    {
        public decimal price { get; set; }
        public decimal volume_24h { get; set; }
        public decimal market_cap { get; set; }
        public float percent_change_1h { get; set; }
        public float percent_change_24h { get; set; }
        public float percent_change_7d { get; set; }
    }
}
