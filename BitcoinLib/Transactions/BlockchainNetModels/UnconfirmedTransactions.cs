using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinLib.Transactions.BlockchainNetModels
{
    public class UnconfirmedTransactions
    {
        [JsonProperty("txs")]
        public Tx[] Txs { get; set; }

        public static UnconfirmedTransactions FromJson(string json) => JsonConvert.DeserializeObject<UnconfirmedTransactions>(json, Converter.Settings);
    }
}
