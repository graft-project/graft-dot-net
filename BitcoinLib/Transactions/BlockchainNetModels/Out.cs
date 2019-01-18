using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinLib.Transactions.BlockchainNetModels
{
    public class Out
    {
        [JsonProperty("spent")]
        public bool Spent { get; set; }

        [JsonProperty("tx_index")]
        public long TxIndex { get; set; }

        [JsonProperty("type")]
        public long Type { get; set; }

        [JsonProperty("addr")]
        public string Addr { get; set; }

        [JsonProperty("value")]
        public long Value { get; set; }

        [JsonProperty("n")]
        public long N { get; set; }

        [JsonProperty("script")]
        public string Script { get; set; }
    }
}
