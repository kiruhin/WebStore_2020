using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Domain.Entities;
using WebStore.DomainNew.Dto;

namespace WebStore.DomainNew.Helpers
{
    public static class ProductMapper
    {
        public static ProductDto ToDto(this Product p) =>
            new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Order = p.Order,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                // добавим информацию о бренде, если она есть
                Brand = p.BrandId.HasValue ? new BrandDto {Id = p.Brand.Id, Name = p.Brand.Name} : null,
                Category = new CategoryDto
                {
                    Id = p.CategoryId,
                    Name = p.Category.Name
                }

            };

    }
}
