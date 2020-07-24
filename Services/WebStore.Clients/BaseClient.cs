using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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
        protected abstract string ServiceAddress { get; set; }

        public BaseClient(IConfiguration configuration)
        {
            Client = new HttpClient
            {
                BaseAddress = new Uri(configuration["ClientAddress"])
            };
            Client.DefaultRequestHeaders.Accept.Clear();

            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
