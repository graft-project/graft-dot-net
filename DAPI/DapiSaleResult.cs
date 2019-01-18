using Newtonsoft.Json;

namespace Graft.DAPI
{
    public class DapiSaleResult
    {
        [JsonProperty(PropertyName = "PaymentID")]
        public string PaymentId { get; set; }

        [JsonProperty(PropertyName = "BlockNumber")]
        public int BlockNumber { get; set; }
    }
}
