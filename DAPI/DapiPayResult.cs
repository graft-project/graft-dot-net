using Newtonsoft.Json;

namespace Graft.DAPI
{
    public class DapiPayResult
    {
        [JsonProperty(PropertyName = "PaymentID")]
        public string PaymentId { get; set; }

        public int Result { get; set; }
    }
}
