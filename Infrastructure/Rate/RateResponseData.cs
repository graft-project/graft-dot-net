using System.Collections.Generic;

namespace Graft.Infrastructure.Rate
{
    public class RateResponseData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string website_slug { get; set; }
        public int rank { get; set; }
        public decimal circulating_supply { get; set; }
        public decimal total_supply { get; set; }
        public decimal? max_supply { get; set; }
        public RateResponseQuotes Quotes { get; set; }
        public int Last_updated { get; set; }
    }
}
