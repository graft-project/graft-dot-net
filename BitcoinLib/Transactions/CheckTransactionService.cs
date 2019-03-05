using BitcoinLib.Transactions.BlockCypherModels;
using NBitcoin;
using NBitcoin.RPC;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinLib.Transactions
{
    /// <summary>
    /// Usage example:
    /// CheckTransactionService ct = new CheckTransactionService();
    /// var result = await ct.GetTransactionStatuses("749258e3b151c491c830c0e5c555703981b97f49228e2ee5b6a6f06ec69c5691",  "28189e50f3f85a705276b5ed3517ba96e29dbc0667d7a966c7f93ea0f57fabdc");
    /// </summary>
    public class CheckTransactionService
    {
        const string _BlockCypherUrl = "https://api.blockcypher.com/v1/btc/{0}/txs/{1}";
        const string _BlockCypher_AddressUrl = "https://api.blockcypher.com/v1/btc/{0}/addrs/{1}";

        readonly HttpClient _client1;
        readonly HttpClient _client2;
        readonly ServiceConfiguration _config;

        public CheckTransactionService(bool isTestNetwork) 
        {
            _config = isTestNetwork ? ServiceConfiguration.TestNet : ServiceConfiguration.Main;

            _client1 = new HttpClient()
            {
                BaseAddress = new Uri("https://api.blockcypher.com/v1/btc/"),
            };

            _client2 = new HttpClient()
            {
                BaseAddress = new Uri(_config.BaseBlockchainComAddress),
            };
        }

        public async Task<CheckTransactionResult> GetTransactionStatusesByAddress(string address, int tryCount = 2, int delayMS = 1000)
        {
            var taskOne = GetTransactionStatusesByAddressBlockCypher(address, tryCount, delayMS);
            var taskTwo = GetTransactionStatusesByAddressBlockchainCom(address, tryCount, delayMS);

            var result = await Task.WhenAny(taskOne, taskTwo);

            return result.Result;
        }

        async Task<CheckTransactionResult> GetTransactionStatusesByAddressBlockCypher(string address, int tryCount = 2, int delayMS = 1000)
        {
            CheckTransactionResult result = null;

            for (int i = 0; i < tryCount; i++)
            {
                try
                {
                    AddressResponse transaction = null;

                    //var requestUrl = string.Format(BlockCypher_AddressUrl, serviceConfiguration.BlockCypherRootNetwork, address);

                    //using (var client = new HttpClient())
                    //{
                    var response = await _client1.GetStringAsync($"{_config.BlockCypherRootNetwork}/addrs/{address}");

                    if (!string.IsNullOrEmpty(response))
                    {
                        transaction = AddressResponse.FromJson(response);
                    }
                    //}

                    if (transaction != null)
                    {
                        if (transaction.UnconfirmedTxrefs != null && transaction.UnconfirmedTxrefs.Any())
                        {
                            var t = transaction.UnconfirmedTxrefs.First();

                            result = new CheckTransactionResult()
                            {
                                TxId = t.TxHash,
                                Amount = t.Value,
                                Confirmations = (int)t.Confirmations,
                                Status = t.DoubleSpend ? TransactionStatus.DoubleSpent : TransactionStatus.Created
                            };

                            break;
                        }

                        if (transaction.Txrefs != null && transaction.Txrefs.Any())
                        {
                            var t = transaction.Txrefs.First();

                            result = new CheckTransactionResult()
                            {
                                TxId = t.TxHash,
                                Amount = t.Value,
                                Confirmations = (int)t.Confirmations,
                                Status = t.DoubleSpend ? TransactionStatus.DoubleSpent : TransactionStatus.Created
                            };
                        }

                        break;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }

                await Task.Delay(delayMS);
            }

            return result;
        }

        private async Task<CheckTransactionResult> GetTransactionStatusesByAddressBlockchainCom(string address, int tryCount = 2, int delayMS = 1000)
        {
            CheckTransactionResult result = null;

            BlockchainNetModels.AddressResponse transaction = null;

            BlockchainNetModels.Tx unconfirmedAddress = null;

            for (int i = 0; i < tryCount; i++)
            {
                try
                {
                    //var unconfirmedRequest = $"{_config.BaseBlockchainComAddress}unconfirmed-transactions?format=json";
                    var unconfirmedRequest = $"unconfirmed-transactions?format=json";

                    //var requestUrl = $"{_config.BaseBlockchainComAddress}rawaddr/{address}";
                    var requestUrl = $"rawaddr/{address}";

                    //using (var client = new HttpClient())
                    //{
                        var response = await _client2.GetStringAsync(requestUrl);

                        if (!string.IsNullOrEmpty(response))
                        {
                            transaction = BlockchainNetModels.AddressResponse.FromJson(response);
                        }

                        var unconfirmedResponse = await _client2.GetStringAsync(unconfirmedRequest);

                        if (!string.IsNullOrEmpty(unconfirmedResponse))
                        {
                            var unconfirmedTransactions = BlockchainNetModels.UnconfirmedTransactions.FromJson(unconfirmedResponse);

                            if (unconfirmedTransactions != null)
                            {
                                unconfirmedAddress = unconfirmedTransactions.Txs.FirstOrDefault(x => x.Inputs.Any(y => y.PrevOut.Addr == address) || x.Out.Any(z => z.Addr == address));
                            }
                        }
                    //}

                    if (transaction != null)
                    {
                        if (transaction.Txs != null && transaction.Txs.Any())
                        {
                            var t = transaction.Txs.First();

                            TransactionStatus transactionStatus = TransactionStatus.Confirmed;

                            if (unconfirmedAddress != null)
                            {
                                transactionStatus = unconfirmedAddress.DoubleSpend ? TransactionStatus.DoubleSpent : TransactionStatus.Created;
                            }

                            result = new CheckTransactionResult()
                            {
                                TxId = t.Hash,
                                Amount = transaction.TotalReceived,
                                Confirmations = 0,
                                Status = transactionStatus
                            };

                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }

                await Task.Delay(delayMS);
            }

            return result;
        }


        //Currently no solution for this one
        //public async Task<TransactionStatus> GetTransactionStatusesByAddressBitcoinDaemon(string address, int tryCount = 2, int delayMS = 1000)
        //{
        //    var client = new NBitcoin.RPC.RPCClient(new RPCCredentialString() { UserPassword = serviceConfiguration.Credentials }, serviceConfiguration.Network);

        //    var addressObject = BitcoinAddress.Create(address, serviceConfiguration.Network);

        //    await client.ImportAddressAsync(addressObject);

        //    var result = await client.GetReceivedByAddressAsync(addressObject, 0);

        //    return TransactionStatus.Created;
        //}
    }
}
