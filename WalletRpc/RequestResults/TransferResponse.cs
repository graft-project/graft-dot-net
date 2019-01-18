using Newtonsoft.Json;

namespace WalletRpc
{
    public class TransferResponse
    {
        [JsonProperty(PropertyName = "fee")]
        public ulong Fee { get; set; }

        [JsonProperty(PropertyName = "tx_hash")]
        public string TxHash { get; set; }

        [JsonProperty(PropertyName = "tx_key")]
        public string TxKey { get; set; }

        [JsonProperty(PropertyName = "amount_keys")]
        public string[] AmountKeys { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public double Amount { get; set; }

        [JsonProperty(PropertyName = "tx_blob")]
        public string TxBlob { get; set; }

        [JsonProperty(PropertyName = "tx_metadata")]
        public string TxMetadata { get; set; }

        [JsonProperty(PropertyName = "multisig_txset")]
        public string MultisigTxset { get; set; }
    }
}
