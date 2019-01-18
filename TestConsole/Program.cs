using BitcoinLib;
using Graft.Infrastructure.Broker;
using Graft.Infrastructure.Rate;
using Newtonsoft.Json;
using System;
using GraftLib;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using WalletRpc;
using GraftLib.Models;
using System.Collections.Generic;
using System.Linq;

namespace TestConsole
{

    class Program
    {
        static void Main(string[] args)
        {
            //var t = new EthereumLib.TransactionManager();

            //var r = t.GetTransactionsByAddress("0xb1ec48fa614e84e0cd7f4e33be897df41a2fc27f").Result;


            StimulusTransactionsTest.Run();
            Console.ReadLine();
        }
    }


    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        var w = new TestWallet();
    //        var d = new TestDb();

    //        var t = new TransactionManager(new GraftServiceConfiguration
    //        {
    //            TransactionWaitTime = new TimeSpan(0,0,1)
    //        },
    //        w,
    //        d);

    //        var v = new TransactionStatusManager(new GraftServiceConfiguration
    //        {
    //            TransactionStatusWaitTime = new TimeSpan(0, 0, 3)
    //        },
    //        w,
    //        d);

    //        Task.Run(async () => { while (true) { Console.Clear(); d.Print(); await Task.Delay(1000); } });


    //        Console.ReadLine();
    //    }




    //}

    //class TestDb : IDatabaseWorker
    //{
    //    List<TransactionRequest> data;

    //    public TestDb()
    //    {
    //        data = Enumerable.Range(1, 20).Select(x => new TransactionRequest { Id = x.ToString(), Status = TransactionRequestStatus.New }).ToList();
    //    }

    //    public async Task<IEnumerable<TransactionRequest>> GetNewTransactions()
    //    {
    //        return data.Where(x => x.Status == TransactionRequestStatus.New || x.Status == TransactionRequestStatus.Failed).Take(20);
    //    }

    //    public IEnumerable<TransactionRequest> GetSentTransactions()
    //    {
    //        return data.Where(x => x.Status == TransactionRequestStatus.Sent || x.Status == TransactionRequestStatus.InProgress);
    //    }

    //    public async Task SetTransactionStatus(IEnumerable<string> ids, bool isFailed, string TxId)
    //    {
    //        data.Where(x => ids.Contains(x.Id))
    //            .ToList()
    //            .ForEach(x => { x.Status = isFailed ? TransactionRequestStatus.Failed : TransactionRequestStatus.Sent; x.TxId = TxId; });
    //    }

    //    public async Task UpdateTransactionStatus(string id, TransactionRequestStatus transactionRequestStatus)
    //    {
    //        data.Where(x => x.Id == id).ToList().ForEach(x => x.Status = transactionRequestStatus);
    //    }

    //    public void Print()
    //    {
    //        foreach (var item in data)
    //        {
    //            Console.WriteLine($"{item.Id} : {item.Status}");
    //        }
    //    }
    //}

    //class TestWallet : IWallet
    //{
    //    private Random r = new Random();
    //    string[] tt = new[] { "in", "out", "pending", "failed", "pool", "mool" };

    //    private List<CreateTransferResponseResult> responseResults = new List<CreateTransferResponseResult>();

    //    public Task<string> Create(string filename, string password, string language = "English")
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public async Task<CreateTransferResponseResult> CreateTransfer(Destination[] destinations)
    //    {
    //        var data = new CreateTransferResponseResult() { TxHash = Guid.NewGuid().ToString() };

    //        responseResults.Add(data);

    //        return data;
    //    }

    //    public Task<string> GetAddress(int accountIndex = 0)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<WalletBalance> GetBalance(int accountIndex = 0)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public async Task<Transfer> GetTransferByTxId(string txId)
    //    {
    //        return new Transfer() { Type = tt[r.Next(tt.Length)], Txid = txId };
    //    }

    //    public Task<Transfers> GetTransfers()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task Open(string filename, string password)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<TransferResponse> Transfer(TransferParams parameters)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
