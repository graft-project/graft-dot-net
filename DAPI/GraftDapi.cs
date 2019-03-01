using Graft.DAPI.Entities;
using Graft.Infrastructure;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Graft.DAPI
{
    public class GraftDapi
    {
        HttpClient client;

        public GraftDapi(IConfiguration configuration)
        {
            var settings = configuration
                .GetSection("DAPI")
                .Get<DapiConfiguration>();

            client = new HttpClient()
            {
                BaseAddress = new Uri(settings.Url)
            };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }


        public static PaymentStatus DapiStatusToPaymentStatus(DapiSaleStatus dapiStatus)
        {
            switch (dapiStatus)
            {
                case DapiSaleStatus.Waiting:
                    return PaymentStatus.Waiting;
                case DapiSaleStatus.InProgress:
                    return PaymentStatus.InProgress;
                case DapiSaleStatus.Success:
                    return PaymentStatus.Received;
                case DapiSaleStatus.Fail:
                    return PaymentStatus.Fail;
                case DapiSaleStatus.RejectedByWallet:
                    return PaymentStatus.RejectedByWallet;
                case DapiSaleStatus.RejectedByPOS:
                    return PaymentStatus.RejectedByPOS;
                default:
                    throw new InvalidCastException();
            }
        }

        public Task<DapiSaleResult> Sale(DapiSaleParams parameters)
        {
            return PostAsync<DapiSaleResult, DapiSaleParams>("sale", parameters);
        }

        public Task<DapiPayResult> Pay(DapiPayParams parameters)
        {
            return PostAsync<DapiPayResult, DapiPayParams>("pay", parameters);
        }

        public Task<DapiSaleStatusResult> GetSaleStatus(DapiSaleStatusParams parameters)
        {
            return PostAsync<DapiSaleStatusResult, DapiSaleStatusParams>("sale_status", parameters);
        }

        public Task<DapiSaleDetailsResult> SaleDetails(DapiSaleDetailsParams parameters)
        {
            return PostAsync<DapiSaleDetailsResult, DapiSaleDetailsParams>("sale_details", parameters);
        }

        async Task<TResult> PostAsync<TResult, TParams>(string method, TParams parameters)
        {
            HttpResponseMessage response = null;

            try
            {
                var request = new DapiRequest<TParams>(method, parameters);
                response = await client.PostAsJsonAsync(method, request);
            }
            catch (Exception)
            {
                throw new ApiException(ErrorCode.NoConnectionToDapi);
            }

            var strResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<DapiResult<TResult>>(strResult);

            if (result.Error != null)
            {
                throw new ApiException(ErrorCode.DapiError, $"Code: {result.Error.Code}, Message: {result.Error.Message}");
            }

            response.EnsureSuccessStatusCode();
            return result.Result;
        }
    }
}
