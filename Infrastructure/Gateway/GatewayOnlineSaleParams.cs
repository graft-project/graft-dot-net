namespace Graft.Infrastructure.Gateway
{
    public class GatewayOnlineSaleParams : GatewaySaleParams
    {
        public string ExternalOrderId { get; set; }
        public string CallbackUrl { get; set; }
        public string CancelUrl { get; set; }
        public string CompleteUrl { get; set; }
    }
}
