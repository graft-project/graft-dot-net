using Newtonsoft.Json;
using System;

namespace WalletRpc
{
    public class WalletBalance
    {
        [JsonProperty(PropertyName = "balance")]
        public UInt64 Balance { get; set; }

        [JsonProperty(PropertyName = "unlocked_balance")]
        public UInt64 UnlockedBalance { get; set; }
    }
}
