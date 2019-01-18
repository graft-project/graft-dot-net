namespace BitcoinLib.Transactions
{
    public class CheckTransactionResult
    {
        public string TxId { get; set; }
        public long Amount { get; set; }
        public int Confirmations { get; set; }
        public TransactionStatus Status { get; set; }
    }
}
