using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using GraftLib.Models;
using System.Linq;
using Graft.Infrastructure;
using WalletRpc;
using Microsoft.Extensions.Logging;

namespace GraftLib
{
    public class TransactionStatusManager
    {
        private Task transactionQWorker;

        private GraftServiceConfiguration configuration;
        private IWallet wallet;
        private IDatabaseWorker databaseWorker;
        private ILogger logger;

        public TransactionStatusManager(
            GraftServiceConfiguration configuration,
            IWallet wallet,
            IDatabaseWorker databaseWorker,
            ILoggerFactory loggerFactory)
        {
            this.configuration = configuration;
            this.wallet = wallet;
            this.databaseWorker = databaseWorker;

            transactionQWorker = Task.Factory.StartNew(TaskBody);

            logger = loggerFactory.CreateLogger<TransactionStatusManager>();
        }

        private async void TaskBody()
        {
            while (true)
            {
                try
                {
                    var transactions = await databaseWorker.GetSentTransactions();

                    if (transactions != null && transactions.Any())
                    {
                        foreach (var item in transactions)
                        {
                            await UpdateTransaction(item);
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to get sent transactions.");
                }
                await Task.Delay(configuration.TransactionStatusWaitTime);
            }
        }

        private async Task UpdateTransaction(TransactionRequest item)
        {
            try
            {
                var result = await wallet.GetTransferByTxId(item.TxId);

                if (result != null && result.Transfer != null)
                {
                    switch (result.Transfer.GetTransferType())
                    {
                        case WalletRpc.TransferType.Pool:
                        case WalletRpc.TransferType.Pending:
                        case WalletRpc.TransferType.In:
                            await databaseWorker.UpdateTransactionStatus(item.Id, TransactionRequestStatus.InProgress);
                            break;
                        case WalletRpc.TransferType.Out:
                            await databaseWorker.UpdateTransactionStatus(item.Id, TransactionRequestStatus.Out);
                            break;
                        case WalletRpc.TransferType.Failed:
                        case WalletRpc.TransferType.Unknown:
                        default:
                            await databaseWorker.UpdateTransactionStatus(item.Id, TransactionRequestStatus.RpcFailed);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to check transaction status for {item.TxId}.");
            }
        }
    }
}
