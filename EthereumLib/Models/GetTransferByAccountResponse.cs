using System;
using System.Collections.Generic;
using System.Text;

namespace EthereumLib.Models
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class GetTransferByAccountResponse
    {
        [JsonProperty("status")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("result")]
        public Result[] Result { get; set; }
    }

    public partial class Result
    {
        [JsonProperty("blockNumber")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long BlockNumber { get; set; }

        [JsonProperty("timeStamp")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long TimeStamp { get; set; }

        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("nonce")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Nonce { get; set; }

        [JsonProperty("blockHash")]
        public string BlockHash { get; set; }

        [JsonProperty("transactionIndex")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long TransactionIndex { get; set; }

        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("gas")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Gas { get; set; }

        [JsonProperty("gasPrice")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long GasPrice { get; set; }

        [JsonProperty("isError")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long IsError { get; set; }

        [JsonProperty("txreceipt_status")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long TxreceiptStatus { get; set; }

        [JsonProperty("input")]
        public string Input { get; set; }

        [JsonProperty("contractAddress")]
        public string ContractAddress { get; set; }

        [JsonProperty("cumulativeGasUsed")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long CumulativeGasUsed { get; set; }

        [JsonProperty("gasUsed")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long GasUsed { get; set; }

        [JsonProperty("confirmations")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Confirmations { get; set; }
    }

    public partial class GetTransferByAccountResponse
    {
        public static GetTransferByAccountResponse FromJson(string json) => JsonConvert.DeserializeObject<GetTransferByAccountResponse>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this GetTransferByAccountResponse self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}
