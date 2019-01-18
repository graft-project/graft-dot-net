using Newtonsoft.Json;

namespace WalletRpc.RequestParams
{
    public class GetTransfersParams
    {
        [JsonProperty(PropertyName = "out")]
        public bool Out { get; set; } = true;

        [JsonProperty(PropertyName = "pending")]
        public bool Pending { get; set; } = true;

        [JsonProperty(PropertyName = "failed")]
        public bool Failed { get; set; } = true;
    }
}
