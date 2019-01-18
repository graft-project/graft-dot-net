using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinLib.Transactions.BlockchainNetModels
{
    public class Input
    {
        [JsonProperty("sequence")]
        public long Sequence { get; set; }

        [JsonProperty("witness")]
        public string Witness { get; set; }

        [JsonProperty("prev_out")]
        public Out PrevOut { get; set; }

        [JsonProperty("script")]
        public string Script { get; set; }
    }
}
