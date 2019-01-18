using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinLib.Transactions.BlockCypherModels
{
    public class Input
    {
        [JsonProperty("prev_hash")]
        public string PrevHash { get; set; }

        [JsonProperty("output_index")]
        public long OutputIndex { get; set; }

        [JsonProperty("script")]
        public string Script { get; set; }

        [JsonProperty("output_value")]
        public long OutputValue { get; set; }

        [JsonProperty("sequence")]
        public long Sequence { get; set; }

        [JsonProperty("addresses")]
        public string[] Addresses { get; set; }

        [JsonProperty("script_type")]
        public string ScriptType { get; set; }

        [JsonProperty("age")]
        public long Age { get; set; }
    }

}
