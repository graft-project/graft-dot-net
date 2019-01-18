using Graft.DAPI.Entities;
using Graft.Infrastructure;
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

        public GraftDapi(string url)
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri(url)
            };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
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
