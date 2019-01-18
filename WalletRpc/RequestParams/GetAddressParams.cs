using Newtonsoft.Json;

namespace WalletRpc.RequestParams
{
    public class GetAddressParams
    {
        public static GetAddressParams Default = new GetAddressParams();

        [JsonProperty(PropertyName = "account_index")]
        public int AccountIndex { get; set; } = 0;

        [JsonProperty(PropertyName = "address_indices")]
        public int[] AddressIndices { get; set; } = { 0, 1 };

    }
}
