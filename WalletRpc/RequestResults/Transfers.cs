using Newtonsoft.Json;

namespace WalletRpc
{
    public class Transfers
    {
        [JsonProperty("out")]
        public TransactionItem[] Out { get; set; }

        [JsonProperty("pending")]
        public TransactionItem[] Pending { get; set; }

        [JsonProperty("failed")]
        public TransactionItem[] Failed { get; set; }
    }
}
