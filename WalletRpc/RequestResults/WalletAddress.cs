using Newtonsoft.Json;

namespace WalletRpc
{
    public class WalletAddress
    {
        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }
    }
}
