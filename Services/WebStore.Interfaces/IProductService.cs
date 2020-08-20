using System.Collections.Generic;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.DomainNew.Dto;

namespace WebStore.Infrastructure.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Category> GetCategories();
        IEnumerable<Brand> GetBrands();
        /// <summary>Список товаров с постраничным разбиением</summary>
        /// <param name="filter">Фильтр товаров</param>
        PagedProductDto GetProducts(ProductFilter filter);
      
        /// <summary>
        /// Получить товар по Id
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Сущность Product, если нашел, иначе null</returns>
        ProductDto GetProductById(int id);

        /// <summary>Секция по Id</summary>
        /// <param name="id">Id</param>
        Category GetCategoryById(int id);

        /// <summary>Бренд по Id</summary>
        /// <param name="id">Id</param>
        Brand GetBrandById(int id);
    }
}
