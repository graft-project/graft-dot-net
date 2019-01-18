namespace Graft.DAPI
{
    public enum DapiSaleStatus : sbyte
    {
        Waiting = 1,
        InProgress = 2,
        Success = 3,
        Fail = 4,
        RejectedByWallet = 5,
        RejectedByPOS = 6
    }
}
