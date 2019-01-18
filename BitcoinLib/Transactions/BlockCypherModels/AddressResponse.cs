using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinLib.Transactions.BlockCypherModels
{
    public class AddressResponse
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("total_received")]
        public long TotalReceived { get; set; }

        [JsonProperty("total_sent")]
        public long TotalSent { get; set; }

        [JsonProperty("balance")]
        public long Balance { get; set; }

        [JsonProperty("unconfirmed_balance")]
        public long UnconfirmedBalance { get; set; }

        [JsonProperty("final_balance")]
        public long FinalBalance { get; set; }

        [JsonProperty("n_tx")]
        public long NTx { get; set; }

        [JsonProperty("unconfirmed_n_tx")]
        public long UnconfirmedNTx { get; set; }

        [JsonProperty("final_n_tx")]
        public long FinalNTx { get; set; }

        [JsonProperty("txrefs")]
        public Txref[] Txrefs { get; set; }

        [JsonProperty("unconfirmed_txrefs")]
        public Txref[] UnconfirmedTxrefs { get; set; }

        [JsonProperty("tx_url")]
        public Uri TxUrl { get; set; }

        public static AddressResponse FromJson(string json) => JsonConvert.DeserializeObject<AddressResponse>(json, Converter.Settings);
    }
}
