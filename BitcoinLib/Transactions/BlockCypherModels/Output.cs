using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinLib.Transactions.BlockCypherModels
{
    public class Output
    {
        [JsonProperty("value")]
        public long Value { get; set; }

        [JsonProperty("script")]
        public string Script { get; set; }

        [JsonProperty("addresses")]
        public string[] Addresses { get; set; }

        [JsonProperty("script_type")]
        public string ScriptType { get; set; }
    }

}
