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

    }
}
