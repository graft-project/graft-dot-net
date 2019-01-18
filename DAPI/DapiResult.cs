using Newtonsoft.Json;

namespace Graft.DAPI
{
    public class DapiResult<T>
    {
        [JsonProperty(PropertyName = "jsonrpc")]
        public string Jsonrpc { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "result")]
        public T Result { get; set; }

        [JsonProperty(PropertyName = "error")]
        public DapiError Error { get; set; }
    }
}
