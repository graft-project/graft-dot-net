using GraftLib.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GraftLib
{
    public interface IDatabaseWorker
    {
        Task<IEnumerable<TransactionRequest>> GetNewTransactions();
        Task SetTransactionStatus(IEnumerable<string> ids, bool isFailed, string TxId);
        Task<IEnumerable<TransactionRequest>> GetSentTransactions();
        Task UpdateTransactionStatus(string id, TransactionRequestStatus transactionRequestStatus);
    }
}
