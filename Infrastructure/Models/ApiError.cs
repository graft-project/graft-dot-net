using Graft.Infrastructure.Extensions;
using Newtonsoft.Json;

namespace Graft.Infrastructure.Models
{
    public class ApiError
    {
        [JsonProperty(PropertyName = "code")]
        public ErrorCode Code { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        public ApiError(ErrorCode code, object param = null)
        {
            Code = code;
            Message = code.GetDescription();

            if (param != null)
                Message = string.Format(Message, param);
        }
    }
}
