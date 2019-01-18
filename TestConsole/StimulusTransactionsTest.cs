using Graft.DAPI;
using Graft.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using WalletRpc;


namespace TestConsole
{
    public class StimulusTransactionsTest
    {
        public static async void Run()
        {
            try
            {
                //var wallet = new Wallet("http://54.84.187.74:29982/", "", "");
                var wallet = new Wallet("http://100.24.50.163:28682/", "", "");
                var address = await wallet.GetAddress();
                var balance = await wallet.GetBalance();
                Console.WriteLine($"Wallet {address} \nbalance: {GraftConvert.FromAtomicUnits(balance.Balance)}\nUnlocked balance: {GraftConvert.FromAtomicUnits(balance.UnlockedBalance)}");


                // params
                ulong amount = GraftConvert.ToAtomicUnits(1.23M);
                //string merchantWalletAddress = "F6YZFhK6MiEHwNYmXHZkRHeX7xCkxfutEUY6QT6541MFNPWPzia3E5NgY7A92mHSc1Evhu7ehdyrK92tC7UW5LKF7pzAcmF";
                string merchantWalletAddress = "FDL3iHduJBGNZGgem6DF6hRt5vGEiBvLfVa4ZVwJjxLyEeLjcUvznMMWnPws3K3N4nbrsjskNLTGePQGXRo3FeqpEG7YwDe";
                string paymentId = Guid.NewGuid().ToString();


                //var dapi = new GraftDapi("http://18.214.197.224:28690/dapi/v2.0/");
                var dapi = new GraftDapi("http://100.24.50.163:28690/dapi/v2.0/");


                // sale -----------------------------------------
                var dapiParams = new DapiSaleParams
                {
                    PaymentId = paymentId,
                    SaleDetails = "sale details string",
                    Address = merchantWalletAddress,
                    Amount = amount
                };

                var saleResult = await dapi.Sale(dapiParams);


                // sale_status -----------------------------------------
                var dapiStatusParams = new DapiSaleStatusParams
                {
                    PaymentId = paymentId,
                    BlockNumber = saleResult.BlockNumber
                };

                var saleStatusResult = await dapi.GetSaleStatus(dapiStatusParams);
                Console.WriteLine($"Sale status: {saleStatusResult.Status}");


                // sale_details -----------------------------------------
                var dapiSaleDetailsParams = new DapiSaleDetailsParams
                {
                    PaymentId = paymentId,
                    BlockNumber = saleResult.BlockNumber
                };

                var saleDetailsResult = await dapi.SaleDetails(dapiSaleDetailsParams);
                var json = JsonConvert.SerializeObject(saleDetailsResult, Formatting.Indented);
                Console.WriteLine($"Sale Details:\n{json}");


                // prepare payment
                var destinations = new List<Destination>();


                ulong totalAuthSampleFee = 0;
                foreach (var item in saleDetailsResult.AuthSample)
                {
                    destinations.Add(new Destination { Amount = item.Fee, Address = item.Address });
                    totalAuthSampleFee += item.Fee;
                }

                destinations.Add(new Destination { Amount = amount - totalAuthSampleFee, Address = merchantWalletAddress });

                var transferParams = new TransferParams
                {
                    Destinations = destinations.ToArray(),
                    //PaymentId = paymentId,
                    DoNotRelay = true,
                    GetTxHex = true,
                    GetTxMetadata = true,
                    GetTxKey = true
                };


                var transferResult = await wallet.TransferRta(transferParams);
                json = JsonConvert.SerializeObject(transferResult, Formatting.Indented);
                Console.WriteLine($"Transfer Result:\n{json}");


                // DAPI pay
                var payParams = new DapiPayParams
                {
                    Address = merchantWalletAddress,
                    PaymentId = paymentId,
                    BlockNumber = saleResult.BlockNumber,
                    Amount = amount,
                    Transactions = new string[] { transferResult.TxBlob }
                };

                json = JsonConvert.SerializeObject(payParams, Formatting.Indented);
                Debug.WriteLine($"Pay Parameters:\n{json}");

                var payResult = await dapi.Pay(payParams);
                json = JsonConvert.SerializeObject(payResult, Formatting.Indented);
                Console.WriteLine($"Pay Result:\n{json}");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

    }
}
