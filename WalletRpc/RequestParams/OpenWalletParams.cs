using Newtonsoft.Json;

namespace WalletRpc.RequestParams
{
    public class OpenWalletParams
    {
        [JsonProperty(PropertyName = "filename")]
        public string Filename { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
    }
}
