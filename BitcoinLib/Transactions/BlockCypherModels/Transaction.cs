using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinLib.Transactions.BlockCypherModels
{
    public class Transaction
    {
        [JsonProperty("block_hash")]
        public string BlockHash { get; set; }

        [JsonProperty("block_height")]
        public long BlockHeight { get; set; }

        [JsonProperty("block_index")]
        public long BlockIndex { get; set; }

        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("addresses")]
        public string[] Addresses { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("fees")]
        public long Fees { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("preference")]
        public string Preference { get; set; }

        [JsonProperty("relayed_by")]
        public string RelayedBy { get; set; }

        [JsonProperty("confirmed")]
        public DateTimeOffset Confirmed { get; set; }

        [JsonProperty("received")]
        public DateTimeOffset Received { get; set; }

        [JsonProperty("ver")]
        public long Ver { get; set; }

        [JsonProperty("double_spend")]
        public bool DoubleSpend { get; set; }

        [JsonProperty("vin_sz")]
        public long VinSz { get; set; }

        [JsonProperty("vout_sz")]
        public long VoutSz { get; set; }

        [JsonProperty("confirmations")]
        public long Confirmations { get; set; }

        [JsonProperty("confidence")]
        public long Confidence { get; set; }

        [JsonProperty("inputs")]
        public Input[] Inputs { get; set; }

        [JsonProperty("outputs")]
        public Output[] Outputs { get; set; }

        public static Transaction FromJson(string json) => JsonConvert.DeserializeObject<Transaction>(json, Converter.Settings);
    }

}
