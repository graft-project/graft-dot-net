using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinLib.Transactions.BlockchainNetModels
{
    public class Tx
    {
        [JsonProperty("ver")]
        public long Ver { get; set; }

        [JsonProperty("inputs")]
        public Input[] Inputs { get; set; }

        [JsonProperty("weight")]
        public long Weight { get; set; }

        [JsonProperty("block_height")]
        public long BlockHeight { get; set; }

        [JsonProperty("relayed_by")]
        public string RelayedBy { get; set; }

        [JsonProperty("out")]
        public Out[] Out { get; set; }

        [JsonProperty("lock_time")]
        public long LockTime { get; set; }

        [JsonProperty("result")]
        public long Result { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("double_spend")]
        public bool DoubleSpend { get; set; }

        [JsonProperty("time")]
        public long Time { get; set; }

        [JsonProperty("tx_index")]
        public long TxIndex { get; set; }

        [JsonProperty("vin_sz")]
        public long VinSz { get; set; }

        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("vout_sz")]
        public long VoutSz { get; set; }
    }
}
