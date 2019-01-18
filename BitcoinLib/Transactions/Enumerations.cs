using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinLib.Transactions
{
    public enum TransactionStatus
    {
        NotFound,
        Created,
        Confirmed,
        DoubleSpent
    }
}
