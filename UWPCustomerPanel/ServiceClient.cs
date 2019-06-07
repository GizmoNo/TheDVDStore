using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;


namespace UWPCustomerPanel
{
    public static class ServiceClient
    {
        internal async static Task<List<string>> GetCategoryListAsync()
        {
            using (HttpClient lcHttpClient = new HttpClient())
                return JsonConvert.DeserializeObject<List<string>>
                    (await lcHttpClient.GetStringAsync("http://localhost:60064/api/admin/GetCategoryList/"));
        }

        internal async static Task<clsCategory> GetProductListAsync(string prCategoryName)
        {
            using (HttpClient lcHttpClient = new HttpClient())
                return JsonConvert.DeserializeObject<clsCategory>
                    (await lcHttpClient.GetStringAsync("http://localhost:60064/api/admin/GetProductList?Name=" + prCategoryName));
        }

        internal async static Task<clsProducts> GetProductAsync(string prProductName)
        {
            using (HttpClient lcHttpClient = new HttpClient())
                return JsonConvert.DeserializeObject<clsProducts>
                    (await lcHttpClient.GetStringAsync("http://localhost:60064/api/admin/GetProduct?Name=" + prProductName));
        }

        internal async static Task<string> CreateOrder(clsOrder prOrder)
        {
            return await InsertOrUpdateAsync(prOrder, "http://localhost:60064/api/admin/CreateOrder", "POST");
        }

        internal async static Task<string> UpdateQuanityInStock(clsProducts prProduct)
        {
            return await InsertOrUpdateAsync(prProduct, "http://localhost:60064/api/admin/UpdateQuanityInStock", "PUT");
        }

        private async static Task<string> InsertOrUpdateAsync<TItem>(TItem prItem, string prUrl, string prRequest)
        {
            using (HttpRequestMessage lcReqMessage = new HttpRequestMessage(new HttpMethod(prRequest), prUrl))
            using (lcReqMessage.Content =
                new StringContent(JsonConvert.SerializeObject(prItem), Encoding.UTF8, "application/json"))
            using (HttpClient lcHttpClient = new HttpClient())
            {
                HttpResponseMessage lcRespMessage = await lcHttpClient.SendAsync(lcReqMessage);
                return await lcRespMessage.Content.ReadAsStringAsync();
            }
        }

    }
}
