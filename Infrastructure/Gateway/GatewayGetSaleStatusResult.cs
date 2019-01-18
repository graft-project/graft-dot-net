namespace Graft.Infrastructure.Gateway
{
    public class GatewayGetSaleStatusResult
    {
        public string PaymentId { get; set; }
        public PaymentStatus Status { get; set; }
    }
}
