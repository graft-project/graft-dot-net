using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GraftLib.Models
{
    public enum TransactionRequestStatus
    {
        IncorrectAddress = -3,
        RpcFailed = -2,
        Failed = -1,
        New = 0,
        InProgress = 1,
        Sent = 2,
        Out = 3
    }

    public class TransactionRequest
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string Address { get; set; }
        public ulong Amount { get; set; }

        [ConcurrencyCheck]
        public TransactionRequestStatus Status { get; set; } = TransactionRequestStatus.New;

        public string TxId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime LastUpdatedTime { get; set; }

        public WalletRpc.Destination ToDestination()
        {
            return new WalletRpc.Destination() { Address = this.Address, Amount = this.Amount };
        }
    }
}
