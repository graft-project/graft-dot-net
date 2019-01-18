using NBitcoin;

namespace BitcoinLib
{
    public class BitcoinWallet
    {
        readonly BitcoinExtPubKey extPubKey;
        readonly Network network;

        public BitcoinWallet(string extPubKeyString, bool isTestNetwork)
        {
            extPubKey = new BitcoinExtPubKey(extPubKeyString);
            network = isTestNetwork ? Network.TestNet : Network.Main;
        }

        public string GetNewAddress(int index)
        {
            var keyPath = new KeyPath($"m/0/{index}");
            var key = extPubKey.ExtPubKey.Derive(keyPath);
            var address = key.PubKey.GetAddress(network);
            return address.ToString();
        }
    }
}
