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
        IEnumerable<ProductDto> GetProducts(ProductFilter filter);
      
        /// <summary>
        /// Получить товар по Id
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Сущность Product, если нашел, иначе null</returns>
        ProductDto GetProductById(int id);
    }
}
