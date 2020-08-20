using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.DomainNew.Dto;
using WebStore.DomainNew.Helpers;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Infrastructure.Services
{
    public class SqlProductService : IProductService
    {
        private readonly WebStoreContext _context;

        public SqlProductService(WebStoreContext context)
        {
            _context = context;
        }
        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public IEnumerable<Brand> GetBrands()
        {
            return _context.Brands.ToList();
        }

        public PagedProductDto GetProducts(ProductFilter filter)
        {
            var products = _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .AsQueryable();

            if (filter.CategoryId.HasValue)
                products = products.Where(x => x.CategoryId == filter.CategoryId.Value);
            if (filter.BrandId.HasValue)
                products = products.Where(x => x.BrandId == filter.BrandId.Value);

            var model = new PagedProductDto { TotalCount = products.Count() };
            if (filter.PageSize != null) // если указан размер страницы
            {
                model.Products = products
                    .OrderBy(p => p.Order)
                    .Skip((filter.Page - 1) * (int)filter.PageSize) // пропустим просмотренные товары
                    .Take((int)filter.PageSize) // возьмем нужное количество
                    .Select(p => p.ToDto()).ToList(); // сконвертируем в DTO
            }
            else // иначе работаем по старой логике
            {
                model.Products = products
                    .OrderBy(p => p.Order)
                    .Select(p => p.ToDto()).ToList();
            }
            return model;
        }

        public ProductDto GetProductById(int id)
        {
            return _context.Products
                .Include(p => p.Category) // жадная загрузка (Eager Load) для категорий
                .Include(p => p.Brand) // жадная загрузка (Eager Load) для брендов
                .FirstOrDefault(p => p.Id == id)
                .ToDto();

        }

        public Category GetCategoryById(int id)
        {
            return _context.Categories.FirstOrDefault(s => s.Id == id);
        }

        public Brand GetBrandById(int id)
        {
            return _context.Brands.FirstOrDefault(s => s.Id == id);
        }

    }
}
