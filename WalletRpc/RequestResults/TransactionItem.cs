using Newtonsoft.Json;

namespace WalletRpc
{
    public class TransactionItem
    {
        [JsonProperty("amount")]
        public long Amount { get; set; }

        [JsonProperty("fee")]
        public long Fee { get; set; }

        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("payment_id")]
        public string PaymentId { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("txid")]
        public string Txid { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("destinations")]
        public Destination[] Destinations { get; set; }
    }
}
