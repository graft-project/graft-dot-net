using Newtonsoft.Json;

namespace WalletRpc.RequestParams
{
    public class CreateWalletParams
    {
        [JsonProperty(PropertyName = "filename")]
        public string Filename { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        [JsonProperty(PropertyName = "language")]
        public string Language { get; set; } = "English";
    }
}
