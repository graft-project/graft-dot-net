using Newtonsoft.Json;

namespace Graft.DAPI
{
    public class DapiSaleParams
    {
        [JsonProperty(PropertyName = "Address")]
        public string Address { get; set; }

        [JsonProperty(PropertyName = "SaleDetails")]
        public string SaleDetails { get; set; }

        [JsonProperty(PropertyName = "PaymentID")]
        public string PaymentId { get; set; }

        [JsonProperty(PropertyName = "Amount")]
        public ulong Amount { get; set; }
    }
}
