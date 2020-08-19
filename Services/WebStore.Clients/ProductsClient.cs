using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Configuration;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.DomainNew.Dto;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Clients
{
    public class ProductsClient : BaseClient, IProductService
    {
        protected override string ServiceAddress { get; } = "api/products";

        public ProductsClient(IConfiguration configuration) : base(configuration)
        {
        }

        public IEnumerable<Category> GetCategories()
        {
            var url = $"{ServiceAddress}/categories";
            var result = Get<List<Category>>(url);
            return result;
        }

        public IEnumerable<Brand> GetBrands()
        {
            var url = $"{ServiceAddress}/brands";
            var result = Get<List<Brand>>(url);
            return result;
        }
 
        public IEnumerable<ProductDto> GetProducts(ProductFilter filter)
        {
            var url = $"{ServiceAddress}";
            var response = Post(url, filter);
            var result = response.Content.ReadAsAsync<IEnumerable<ProductDto>>().Result;
            return result;
        }
 
        public ProductDto GetProductById(int id)
        {
            var url = $"{ServiceAddress}/{id}";
            var result = Get<ProductDto>(url);
            return result;
        }

        public Category GetCategoryById(int id)
        {
            var url = $"{ServiceAddress}/categories/{id}";
            var result = Get<Category>(url);
            return result;
        }

        public Brand GetBrandById(int id)
        {
            var url = $"{ServiceAddress}/brands/{id}";
            var result = Get<Brand>(url);
            return result;
        }

    }
}
