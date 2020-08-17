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

        public IEnumerable<ProductDto> GetProducts(ProductFilter filter)
        {
            var query = _context.Products
                .Include(p => p.Category) // жадная загрузка (Eager Load) для категорий
                .Include(p => p.Brand) // жадная загрузка (Eager Load) для брендов
                .AsQueryable();

            if (filter.BrandId.HasValue)
                query = query.Where(c => c.BrandId.HasValue && c.BrandId.Value.Equals(filter.BrandId.Value));
            if (filter.CategoryId.HasValue)
                query = query.Where(c => c.CategoryId.Equals(filter.CategoryId.Value));

            return query
                .Select(p => p.ToDto())
                .ToList();
        }

        public ProductDto GetProductById(int id)
        {
            return _context.Products
                .Include(p => p.Category) // жадная загрузка (Eager Load) для категорий
                .Include(p => p.Brand) // жадная загрузка (Eager Load) для брендов
                .FirstOrDefault(p => p.Id == id)
                .ToDto();

        }

        public Category GetSectionById(int id)
        {
            return _context.Categories.FirstOrDefault(s => s.Id == id);
        }

        public Brand GetBrandById(int id)
        {
            return _context.Brands.FirstOrDefault(s => s.Id == id);
        }

    }
}
