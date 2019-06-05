using System;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel
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

        internal async static Task<string> InsertProductAsync(clsProducts prProducts)
        {
            return await InsertOrUpdateAsync(prProducts, "http://localhost:60064/api/admin/PostProduct", "POST");
        }

        internal async static Task<string> UpdateProductAsync(clsProducts prProducts)
        {
            return await InsertOrUpdateAsync(prProducts, "http://localhost:60064/api/admin/PutProduct", "PUT");
        }

        internal async static Task<string> DeleteProductAsync(clsProducts prProductName)
        {
            
            return await InsertOrUpdateAsync(prProductName, "http://localhost:60064/api/admin/DeleteProduct", "DELETE");
                                                                                    
        }

        private async static Task<string> InsertOrUpdateAsync<TItem>(TItem prItem, string prUrl, string prRequest)
        {
            using (HttpRequestMessage lcReqMessage = new HttpRequestMessage(new HttpMethod(prRequest), prUrl))
            using (lcReqMessage.Content =
                new StringContent(JsonConvert.SerializeObject(prItem), Encoding.Default, "application/json"))
            using (HttpClient lcHttpClient = new HttpClient())
            {
                HttpResponseMessage lcRespMessage = await lcHttpClient.SendAsync(lcReqMessage);
                return await lcRespMessage.Content.ReadAsStringAsync();
            }
        }



    }
}
