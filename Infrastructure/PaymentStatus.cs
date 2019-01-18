namespace Graft.Infrastructure
{
    public enum PaymentStatus : sbyte
    {
        TimedOut = -1,
        DoubleSpend = -2,
        NotEnoughAmount = -3,
        Fail = -4,
        RejectedByWallet = -5,
        RejectedByPOS = -6,

        New = 0,

        Waiting = 1,
        InProgress = 2,
        Received = 3,
        Confirmed = 4,
    }
}
