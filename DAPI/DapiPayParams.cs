using Newtonsoft.Json;

namespace Graft.DAPI
{
    public class DapiPayParams
    {
        public string Address { get; set; }

        [JsonProperty(PropertyName = "PaymentID")]
        public string PaymentId { get; set; }

        public ulong Amount { get; set; }

        public int BlockNumber { get; set; }

        public string[] Transactions { get; set; }
    }
}
