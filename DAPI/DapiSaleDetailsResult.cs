using Newtonsoft.Json;

namespace Graft.DAPI
{
    public class DapiSaleDetailsResult
    {
        [JsonProperty(PropertyName = "AuthSample")]
        public DapiSupernodeFee[] AuthSample { get; set; }

        [JsonProperty(PropertyName = "Details")]
        public string SaleDetails { get; set; }

    }
}
