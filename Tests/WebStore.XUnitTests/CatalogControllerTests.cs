using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.XUnitTests
{
    public class CatalogControllerTests
    {
        // проверим, что метод controller.ProductDetails() возвращает корректную модель
        public void ProductDetails_Returns_View_With_Correct_Item()
        {
            // Arrange
            // заглушка для нужного метода
            // должна возвращать какой-то список товаров List<ProductDto>

            // Act
            // вызываем тестируемый метод

            // Assert
            // проверим тип пришедших данных
            // проверим поля модели
        }

        // метод controller.ProductDetails() должен вернуть 404 NotFound, если не найден товар по id
        public void ProductDetails_Returns_NotFound()
        {
            // Arrange
            // заглушка для метода 
            // этот метод всегда будет возвращать null

            // Act

            // Assert
            // ответ от контроллера должен быть типа NotFoundResult
        }

        // проверяем корректную работу метода controller.Shop()
        public void Shop_Method_Returns_Correct_View()
        {
            // Arrange
            // заглушка для метода 
            // должна возвращать какой-то список товаров List<ProductDto>

            // Act
            // вызываем тестируемый метод

            // Assert
            // проверяем тип данных
            // проверяем поля модели
        }

    }
}
