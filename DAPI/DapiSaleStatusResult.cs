using Graft.Infrastructure;

namespace Graft.DAPI
{
    public class DapiSaleStatusResult
    {
        public DapiSaleStatus Status { get; set; }

        public PaymentStatus GetPaymentStatus()
        {
            switch (Status)
            {
                case DapiSaleStatus.Waiting:
                    return PaymentStatus.Waiting;

                case DapiSaleStatus.InProgress:
                    return PaymentStatus.InProgress;

                case DapiSaleStatus.Success:
                    return PaymentStatus.Received;

                case DapiSaleStatus.Fail:
                    return PaymentStatus.Fail;

                case DapiSaleStatus.RejectedByWallet:
                    return PaymentStatus.RejectedByWallet;

                case DapiSaleStatus.RejectedByPOS:
                    return PaymentStatus.RejectedByPOS;

                default:
                    throw new ApiException(ErrorCode.UnknownDapiPaymentStatus, (int)Status);
            }
        }
    }
}
