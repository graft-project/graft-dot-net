using Newtonsoft.Json;

namespace WalletRpc.RequestParams
{
    public class GetTransferByTxIdParams
    {
        [JsonProperty(PropertyName = "txid")]
        public string TxId { get; set; }
    }
}
