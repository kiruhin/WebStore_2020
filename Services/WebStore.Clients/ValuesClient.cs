using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebStore.Interfaces;

namespace WebStore.Clients
{
    public class ValuesClient : BaseClient, IValuesService
    {
        public ValuesClient(IConfiguration configuration) : base(configuration)
        {
            ServiceAddress = "api/values";
        }

        protected override string ServiceAddress { get; set; }

        public IEnumerable<string> Get()
        {
            var list = new List<string>();
            var response = Client.GetAsync($"{ServiceAddress}").Result;
            if (response.IsSuccessStatusCode)
            {
                list = response.Content.ReadAsAsync<List<string>>().Result;
            }
            return list;
        }
 
        public async Task<IEnumerable<string>> GetAsync()
        {
            var list = new List<string>();
            var response = await Client.GetAsync($"{ServiceAddress}");
            if (response.IsSuccessStatusCode)
            {
                list = await response.Content.ReadAsAsync<List<string>>();
            }
            return list;
        }
 
        public string Get(int id)
        {
            var result = string.Empty;
 
            var response = Client.GetAsync($"{ServiceAddress}/get/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadAsAsync<string>().Result;
            }
            return result;
        }
 
        public async Task<string> GetAsync(int id)
        {
            var result = string.Empty;
 
            var response = await Client.GetAsync($"{ServiceAddress}/get/{id}");
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<string>();
            }
            return result;
        }
 
        public Uri Post(string value)
        {
            var response = Client.PostAsJsonAsync($"{ServiceAddress}/post", value).Result;
            response.EnsureSuccessStatusCode();
 
            return response.Headers.Location;
        }
 
        public async Task<Uri> PostAsync(string value)
        {
            var response = await Client.PostAsJsonAsync($"{ServiceAddress}/post", value);
            response.EnsureSuccessStatusCode();
 
            return response.Headers.Location;
        }
 
        public HttpStatusCode Put(int id, string value)
        {
            var response = Client.PutAsJsonAsync($"{ServiceAddress}/put/{id}", value).Result;
            response.EnsureSuccessStatusCode();
 
            return response.StatusCode;
        }
 
        public async Task<HttpStatusCode> PutAsync(int id, string value)
        {
            var response = await Client.PutAsJsonAsync($"{ServiceAddress}/put/{id}", value);
            response.EnsureSuccessStatusCode();
 
            return response.StatusCode;
        }
 
        public HttpStatusCode Delete(int id)
        {
            var response = Client.DeleteAsync($"{ServiceAddress}/delete/{id}").Result;
            return response.StatusCode;
        }
 
        public async Task<HttpStatusCode> DeleteAsync(int id)
        {
            var response = await Client.DeleteAsync($"{ServiceAddress}/delete/{id}");
            return response.StatusCode;
        }
    }
}
