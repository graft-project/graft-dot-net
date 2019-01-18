using Newtonsoft.Json;

namespace Graft.DAPI
{
    public class DapiError
    {
        [JsonProperty(PropertyName = "code")]
        public int Code { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }
}
