using Newtonsoft.Json;

namespace WalletRpc.RequestParams
{
    public class CreateTransferParams
    {
        [JsonProperty(PropertyName = "destinations")]
        public Destination[] Destinations { get; set; }

        [JsonProperty(PropertyName = "mixin")]
        public int Mixin { get; set; } = 4;

        [JsonProperty(PropertyName = "get_tx_key")]
        public bool GetTxKey { get; set; } = true;
    }
}
