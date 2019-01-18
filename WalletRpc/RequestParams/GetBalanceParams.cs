using Newtonsoft.Json;

namespace WalletRpc.RequestParams
{
    public class GetBalanceParams
    {
        public static GetBalanceParams Default = new GetBalanceParams();

        [JsonProperty(PropertyName = "account_index")]
        public int AccountIndex { get; set; } = 0;
    }
}
