using Newtonsoft.Json;

namespace Graft.DAPI
{
    public class DapiSaleDetailsParams
    {
        [JsonProperty(PropertyName = "PaymentID")]
        public string PaymentId { get; set; }

        [JsonProperty(PropertyName = "BlockNumber")]
        public int BlockNumber { get; set; }
    }
}
