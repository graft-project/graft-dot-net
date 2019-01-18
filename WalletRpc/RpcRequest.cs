using Newtonsoft.Json;

namespace WalletRpc
{
    public class RpcRequest<T>
    {
        [JsonProperty(PropertyName = "jsonrpc")]
        public string Jsonrpc { get; set; } 

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "method")]
        public string Method { get; set; }

        [JsonProperty(PropertyName = "params")]
        public T Parameters { get; set; }

        public RpcRequest(string method, T parameters)
        {
            Jsonrpc = "2.0";
            Id = "0";
            Method = method;
            Parameters = parameters;
        }
    }
}
