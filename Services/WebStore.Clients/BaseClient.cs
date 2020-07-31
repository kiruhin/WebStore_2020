using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace WebStore.Clients
{
    public abstract class BaseClient
    {
        /// <summary>
        /// Http клиент для связи
        /// </summary>
        public HttpClient Client { get; set; }

        /// <summary>
        /// Адрес сервиса
        /// </summary>
        protected abstract string ServiceAddress { get; }

        public BaseClient(IConfiguration configuration)
        {
            Client = new HttpClient
            {
                BaseAddress = new Uri(configuration["ClientAddress"])
            };
            Client.DefaultRequestHeaders.Accept.Clear();

            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected T Get<T>(string url) where T : new()
        {
            return GetAsync<T>(url).Result;
        }
 
        protected async Task<T> GetAsync<T>(string url) where T : new()
        {
            var list = new T();
            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
                list = await response.Content.ReadAsAsync<T>();
            return list;
        }
 
        protected HttpResponseMessage Post<T>(string url, T value)
        {
            return PostAsync(url, value).Result;
        }
 
        protected async Task<HttpResponseMessage> PostAsync<T>(string url, T value)
        {
            var response = await Client.PostAsJsonAsync(url, value);
            // response.EnsureSuccessStatusCode();
            return response;
        }
 
        protected HttpResponseMessage Put<T>(string url, T value)
        {
            return PutAsync(url, value).Result;
        }
 
        protected async Task<HttpResponseMessage> PutAsync<T>(string url, T value)
        {
            var response = await Client.PutAsJsonAsync(url, value);
            // response.EnsureSuccessStatusCode();
            return response;
        }
 
        protected HttpResponseMessage Delete(string url)
        {
            return DeleteAsync(url).Result;
        }
 
        protected async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            return await Client.DeleteAsync(url); ;
        }
    }
}
