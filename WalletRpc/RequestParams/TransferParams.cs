using Newtonsoft.Json;

namespace WalletRpc
{
    public class TransferParams
    {
        [JsonProperty(PropertyName = "destinations")]
        public Destination[] Destinations { get; set; }

        [JsonProperty(PropertyName = "account_index")]
        public uint? AccountIndex { get; set; }

        [JsonProperty(PropertyName = "subaddr_indices")]
        public uint[] SubaddrIndices { get; set; }

        [JsonProperty(PropertyName = "mixin")]
        public uint? Mixin { get; set; }

        [JsonProperty(PropertyName = "unlock_time")]
        public uint? UnlockTime { get; set; }

        [JsonProperty(PropertyName = "payment_id")]
        public string PaymentId { get; set; }

        [JsonProperty(PropertyName = "get_tx_key")]
        public bool? GetTxKey { get; set; }

        [JsonProperty(PropertyName = "priority")]
        public uint? Priority { get; set; }

        [JsonProperty(PropertyName = "do_not_relay")]
        public bool? DoNotRelay { get; set; }

        [JsonProperty(PropertyName = "get_tx_hex")]
        public bool? GetTxHex { get; set; }

        [JsonProperty(PropertyName = "get_tx_metadata")]
        public bool? GetTxMetadata { get; set; }
    }
}
