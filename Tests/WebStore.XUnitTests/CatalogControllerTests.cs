using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using WebStore.Controllers;
using WebStore.Domain;
using WebStore.DomainNew.Dto;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;
using Xunit;

namespace WebStore.XUnitTests
{
    public class CatalogControllerTests
    {
        Mock<IConfiguration> _configMock = new Mock<IConfiguration>();

        [Fact]
        public void ProductDetails_Returns_View_With_Correct_Item()
        {
            // Arrange
            var productMock = new Mock<IProductService>();
            productMock
                .Setup(p => p.GetProductById(It.IsAny<int>()))
                .Returns(new ProductDto
                {
                    Id = 1,
                    Name = "Test",
                    ImageUrl = "TestImage.jpg",
                    Order = 0,
                    Price = 10,
                    Brand = new BrandDto
                    {
                        Id = 1,
                        Name = "TestBrand"
                    }
                });
            var controller = new CatalogController(productMock.Object, _configMock.Object);

            // Act
            var result = controller.ProductDetails(1);

            // Assert
            var viewResult = Xunit.Assert.IsType<ViewResult>(result);
            var model = Xunit.Assert.IsAssignableFrom<ProductViewModel>(viewResult.ViewData.Model);

            Xunit.Assert.Equal(1, model.Id);
            Xunit.Assert.Equal("Test", model.Name);
            Xunit.Assert.Equal(10, model.Price);
            Xunit.Assert.Equal("TestBrand", model.Brand);
        }

        [Fact]
        public void ProductDetails_Returns_NotFound()
        {
            // Arrange
            var productMock = new Mock<IProductService>();
            productMock
                .Setup(p => p.GetProductById(It.IsAny<int>()))
                .Returns((ProductDto)null);
            var controller = new CatalogController(productMock.Object, _configMock.Object);

            // Act
            var result = controller.ProductDetails(1);

            // Assert
            Xunit.Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Shop_Method_Returns_Correct_View()
        {
            // Arrange
            var productMock = new Mock<IProductService>();
            var productDtos = new List<ProductDto>
            {
                new ProductDto
                {
                    Id = 1,
                    Name = "Test",
                    ImageUrl = "TestImage.jpg",
                    Order = 0,
                    Price = 10,
                    Brand = new BrandDto
                    {
                        Id = 1,
                        Name = "TestBrand"
                    }
                },
                new ProductDto
                {
                    Id = 2,
                    Name = "Test2",
                    ImageUrl = "TestImage2.jpg",
                    Order = 1,
                    Price = 22,
                    Brand = new BrandDto
                    {
                        Id = 1,
                        Name = "TestBrand"
                    }
                }
            };
            productMock
                .Setup(p => p.GetProducts(It.IsAny<ProductFilter>()))
                .Returns(new PagedProductDto { Products = productDtos, TotalCount = 3 });
            var controller = new CatalogController(productMock.Object, _configMock.Object);

            // Act
            var result = controller.Shop(1, 5);

            // Assert
            var viewResult = Xunit.Assert.IsType<ViewResult>(result);
            var model = Xunit.Assert.IsAssignableFrom<CatalogViewModel>(viewResult.ViewData.Model);

            Xunit.Assert.Equal(2, model.Products.Count());
            Xunit.Assert.Equal(5, model.BrandId);
            Xunit.Assert.Equal(1, model.CategoryId);
            Xunit.Assert.Equal("TestImage2.jpg", model.Products.ToList()[1].ImageUrl);
        }
    }
}