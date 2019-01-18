using Graft.Infrastructure;
using GraftLib.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WalletRpc;

namespace GraftLib
{
    public class GraftLibService
    {
        private IWallet wallet;
        private TransactionManager transactionManager;
        private TransactionStatusManager transactionStatusManager;

        public GraftLibService(GraftServiceConfiguration graftServiceConfiguration, IDatabaseWorker databaseWorker, ILoggerFactory loggerFactory)
        {
            wallet = new WalletRpc.Wallet(graftServiceConfiguration.ServerUrl, graftServiceConfiguration.User, graftServiceConfiguration.Password);
            transactionManager = new TransactionManager(graftServiceConfiguration, wallet, databaseWorker, loggerFactory);
            transactionStatusManager = new TransactionStatusManager(graftServiceConfiguration, wallet, databaseWorker, loggerFactory);
        }
    }
}
