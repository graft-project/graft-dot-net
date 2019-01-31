using GraftLib.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletRpc;
using WalletRpc.RequestParams;

namespace GraftLib
{
    public class TransactionManager
    {
        private Task transactionQWorker;

        private GraftServiceConfiguration configuration;
        private IWallet wallet;
        private IDatabaseWorker databaseWorker;
        private ILogger logger;

        public TransactionManager(
            GraftServiceConfiguration configuration,
            IWallet wallet,
            IDatabaseWorker databaseWorker, 
            ILoggerFactory loggerFactory)
        {
            this.configuration = configuration;
            this.wallet = wallet;
            this.databaseWorker = databaseWorker;

            transactionQWorker = Task.Factory.StartNew(TaskBody);

            logger = loggerFactory.CreateLogger<TransactionManager>();
        }

        private async Task<IEnumerable<TransactionRequest>> ValidateTransactions(IEnumerable<TransactionRequest> transactions)
        {
            List<TransactionRequest> validAddress = new List<TransactionRequest>();

            foreach (var item in transactions)
            {
                bool isValid = false;

                try
                {
                   isValid = await wallet.IsAddressValid(new AddressValidRequest() { address = item.Address });
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to check tx status");
                }

                if (isValid)
                {
                    validAddress.Add(item);
                }
                else
                {
                    try
                    {
                        await databaseWorker.UpdateTransactionStatus(item.Id, TransactionRequestStatus.IncorrectAddress);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, $"Failed to update incorrect address '{item.Address}' tx '{item.Id}' status");
                    }
                }
            }

            return validAddress;
        }

        private async Task<string> SendTransactions(IEnumerable<TransactionRequest> transactions)
        {
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    var result = await wallet.CreateTransfer(new CreateTransferParams
                    {
                        Destinations = transactions.Select(x => x.ToDestination()).ToArray()
                    });

                    return result.TxHash;
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"Failed to send transactions :[ {string.Join(", ",transactions.Select(x=>x.TxId)) } ]");
                }

                await Task.Delay(new TimeSpan(0, 0, 5));
            }

            return null;
        }

        private async void TaskBody()
        {
            while (true)
            {
                try
                {
                    var transactions = await databaseWorker.GetNewTransactions();

                    if (transactions != null)
                    {
                        transactions = await ValidateTransactions(transactions);

                        if (transactions.Any())
                        {
                            logger.LogInformation($"Sending {transactions.Count()} transactions.");

                            var txHash = await SendTransactions(transactions);

                            await databaseWorker.SetTransactionStatus(transactions.Select(x => x.Id), txHash == null, txHash);
                        }
                    }

                    await Task.Delay(configuration.TransactionWaitTime);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error in TransactionManager");
                    await Task.Delay(configuration.TransactionWaitTime);
                }
            }
        }
    }
}
