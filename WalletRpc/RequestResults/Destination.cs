using Newtonsoft.Json;

namespace WalletRpc
{
    public class Destination
    {
        [JsonProperty(PropertyName = "amount")]
        public ulong Amount { get; set; }

        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }
    }
}
