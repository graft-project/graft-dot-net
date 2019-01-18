using Newtonsoft.Json;

namespace WalletRpc
{
    public sealed class RpcResponse<T>
    {
        [JsonProperty(PropertyName = "jsonrpc")]
        public string Jsonrpc { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "result")]
        public T Result { get; set; }

        [JsonProperty(PropertyName = "error")]
        public RpcError Error { get; set; }
    }
}
