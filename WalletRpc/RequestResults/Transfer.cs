using Newtonsoft.Json;

namespace WalletRpc
{
    public class TransferContainer
    {
        [JsonProperty("transfer")]
        public Transfer Transfer { get; set; }
    }

    public class Transfer
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("amount")]
        public long Amount { get; set; }

        [JsonProperty("confirmations")]
        public long Confirmations { get; set; }

        [JsonProperty("destinations")]
        public Destination[] Destinations { get; set; }

        [JsonProperty("double_spend_seen")]
        public bool DoubleSpendSeen { get; set; }

        [JsonProperty("fee")]
        public long Fee { get; set; }

        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("payment_id")]
        public string PaymentId { get; set; }

        [JsonProperty("subaddr_index")]
        public SubaddrIndex SubaddrIndex { get; set; }

        [JsonProperty("suggested_confirmations_threshold")]
        public long SuggestedConfirmationsThreshold { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("txid")]
        public string Txid { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("unlock_time")]
        public long UnlockTime { get; set; }

        public TransferType GetTransferType()
        {
            switch (Type.ToLower())
            {
                case "in":
                    return TransferType.In;
                case "out":
                    return TransferType.Out;
                case "pending":
                    return TransferType.Pending;
                case "failed":
                    return TransferType.Failed;
                case "pool":
                    return TransferType.Pool;
                default:
                    return TransferType.Unknown;
            }

        }
    }

    public class SubaddrIndex
    {
        [JsonProperty("major")]
        public long Major { get; set; }

        [JsonProperty("minor")]
        public long Minor { get; set; }
    }

    public enum TransferType
    {
        Unknown,
        In,
        Out,
        Pending,
        Failed,
        Pool
    }

}
