using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinLib.Transactions.BlockchainNetModels
{
    public class AddressResponse
    {
        [JsonProperty("hash160")]
        public string Hash160 { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("n_tx")]
        public long NTx { get; set; }

        [JsonProperty("total_received")]
        public long TotalReceived { get; set; }

        [JsonProperty("total_sent")]
        public long TotalSent { get; set; }

        [JsonProperty("final_balance")]
        public long FinalBalance { get; set; }

        [JsonProperty("txs")]
        public Tx[] Txs { get; set; }

        public static AddressResponse FromJson(string json) => JsonConvert.DeserializeObject<AddressResponse>(json, Converter.Settings);
    }
}
