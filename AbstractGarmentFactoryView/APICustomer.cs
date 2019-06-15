using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AbstractGarmentFactoryView
{
    public static class APICustomer
    {
        private static HttpClient customer = new HttpClient();

        public static void Connect()
        {
            customer.BaseAddress = new Uri(ConfigurationManager.AppSettings["IPAddress"]);
            customer.DefaultRequestHeaders.Accept.Clear();
            customer.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static T GetRequest<T>(string requestUrl)
        {
            var response = customer.GetAsync(requestUrl);
            if (response.Result.IsSuccessStatusCode)
            {
                return response.Result.Content.ReadAsAsync<T>().Result;
            }
            throw new Exception(response.Result.Content.ReadAsStringAsync().Result);
        }

        public static U PostRequest<T, U>(string requestUrl, T model)
        {
            var response = customer.PostAsJsonAsync(requestUrl, model);
            if (response.Result.IsSuccessStatusCode)
            {
                if (typeof(U) == typeof(bool))
                {
                    return default(U);
                }
                return response.Result.Content.ReadAsAsync<U>().Result;
            }
            throw new Exception(response.Result.Content.ReadAsStringAsync().Result);
        }
    }
}
