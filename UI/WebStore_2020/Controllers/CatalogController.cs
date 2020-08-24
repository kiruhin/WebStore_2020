using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebStore.Domain;
using WebStore.DomainNew.ViewModels;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductService _productService;
        private readonly IConfiguration _configuration;

        public CatalogController(IProductService productService, IConfiguration configuration)
        {
            _productService = productService;
            _configuration = configuration;
        }

        public IActionResult Shop(int? categoryId, int? brandId, int page = 1)
        {
            var pageSize = int.Parse(_configuration["PageSize"]);

            var products = GetProducts(categoryId, brandId, page, out var totalCount);
      
            // сконвертируем в CatalogViewModel
            var model = new CatalogViewModel
            {
                BrandId = brandId,
                CategoryId = categoryId,
                Products = products,
                PageViewModel = new PageViewModel
                {
                    PageSize = pageSize,
                    PageNumber = page,
                    TotalItems = totalCount
                }
            };

            return View(model);
        }

        public IActionResult ProductDetails(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
                return NotFound();

            return View(new ProductViewModel
            {
                Id = product.Id,
                ImageUrl = product.ImageUrl,
                Name = product.Name,
                Order = product.Order,
                Price = product.Price,
                Brand = product.Brand?.Name ?? string.Empty
            });

        }

        public IActionResult GetFilteredItems(int? categoryId, int? brandId, int page = 1)
        {
            var productsModel = GetProducts(categoryId, brandId, page, out var totalCount);

            return PartialView("_Partial/_FeaturedItems", productsModel);
        }

        private IEnumerable<ProductViewModel> GetProducts(int? categoryId, int? brandId, int page, out int totalCount)
        {
            var products = _productService.GetProducts(new ProductFilter
            {
                CategoryId = categoryId,
                BrandId = brandId,
                Page = page,
                PageSize = int.Parse(_configuration["PageSize"])
            });
            totalCount = products.TotalCount;

            return products.Products.Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    ImageUrl = p.ImageUrl,
                    Name = p.Name,
                    Order = p.Order,
                    Price = p.Price,
                    Brand = p.Brand?.Name ?? String.Empty
                })
                .OrderBy(p => p.Order)
                .ToList();
        }

    }
}
