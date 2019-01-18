using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Graft.Infrastructure.AccountPool
{
    public class AccountPoolItem
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string Address { get; set; }

        [ConcurrencyCheck]
        public bool IsProcessed { get; set; }

        public decimal Balance { get; set; }

        public string CurrencyName { get; set; }

        public string LastTransactionHash { get; set; }
    }
}
