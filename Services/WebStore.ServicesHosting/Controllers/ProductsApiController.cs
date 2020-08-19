using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.DomainNew.Dto;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.ServicesHosting.Controllers
{
    [Produces("application/json")]
    [Route("api/products")]
    //[ApiController]
    public class ProductsApiController : ControllerBase, IProductService
    {
        private readonly IProductService _productService;
 
        public ProductsApiController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("categories")]
        public IEnumerable<Category> GetCategories()
        {
            return _productService.GetCategories();
        }

        [HttpGet("brands")]
        public IEnumerable<Brand> GetBrands()
        {
            return _productService.GetBrands();
        }
 
        [HttpPost]
        [ActionName("Post")]
        public IEnumerable<ProductDto> GetProducts([FromBody]ProductFilter filter)
        {
            return _productService.GetProducts(filter);
        }
 
        [HttpGet("{id}"), ActionName("Get")]
        public ProductDto GetProductById(int id)
        {
            var product = _productService.GetProductById(id);
            return product;
        }

        [HttpGet("categories/{id}")]
        public Category GetCategoryById(int id)
        {
            return _productService.GetCategoryById(id);
        }

        [HttpGet("brands/{id}")]
        public Brand GetBrandById(int id)
        {
            return _productService.GetBrandById(id);
        }

    }
}
