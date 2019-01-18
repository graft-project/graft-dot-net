using NBitcoin;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinLib.Transactions
{
    public class ServiceConfiguration
    {
        public string BlockCypherRootNetwork { get; set; }
        public Network Network { get; set; }
        //public System.Net.NetworkCredential Credentials { get; set; }
        public string BaseBlockchainComAddress { get; set; }

        private ServiceConfiguration(){}

        public static ServiceConfiguration TestNet
        {
            get
            {
                return new ServiceConfiguration()
                {
                    BlockCypherRootNetwork = "test3",
                    Network = Network.TestNet,
                    //Credentials = new System.Net.NetworkCredential("test", "test"),
                    BaseBlockchainComAddress = "https://testnet.blockchain.info/"
                };
            }
        }

        public static ServiceConfiguration Main
        {
            get
            {
                return new ServiceConfiguration()
                {
                    BlockCypherRootNetwork = "main", 
                    Network = Network.Main,
                    //Credentials = new System.Net.NetworkCredential("test", "test"),
                    BaseBlockchainComAddress = "https://blockchain.info/"
                };
            }
        }
    }
}
