using Newtonsoft.Json;

namespace Graft.DAPI
{
    public class DapiSaleStatusParams
    {
        [JsonProperty(PropertyName = "PaymentID")]
        public string PaymentId { get; set; }

        [JsonProperty(PropertyName = "BlockNumber")]
        public int BlockNumber { get; set; }
    }
}
