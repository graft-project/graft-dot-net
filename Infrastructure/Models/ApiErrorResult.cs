using Newtonsoft.Json;

namespace Graft.Infrastructure.Models
{
    public class ApiErrorResult
    {
        [JsonProperty(PropertyName = "error")]
        public ApiError Error { get; set; }

        public ApiErrorResult(ApiError error)
        {
            Error = error;
        }
    }
}
