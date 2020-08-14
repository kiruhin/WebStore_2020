using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebStore.Controllers;
using WebStore.DomainNew.Dto.Order;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;
using Xunit;

namespace WebStore.XUnitTests
{
    public class CartControllerTests
    {
        Mock<ICartService> _mockCartService;
        Mock<IOrdersService> _mockOrdersService;
        CartController _controller;

        // ctor
        public CartControllerTests()
        {
            // инициализируем мок-сервисы и контроллер
            _mockCartService = new Mock<ICartService>();
            _mockOrdersService = new Mock<IOrdersService>();
            _controller = new CartController(_mockCartService.Object, _mockOrdersService.Object);
        }

        [Fact]
        // проверяем поведение метода _controller.CheckOut при наличии ошибки в модели
        public void CheckOut_ModelState_Invalid_Returns_ViewModel()
        {
            // Arrange
            // добавим ошибку в ModelState
            _controller.ModelState.AddModelError("error", "InvalidModel");

            // Act
            var result = _controller.CheckOut(new OrderViewModel
            {
                Name = "test"
            });

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<OrderDetailsViewModel>(viewResult.ViewData.Model);
            // модель все равно должна была создаться
            Assert.Equal("test", model.OrderViewModel.Name);
        }

        [Fact]
        //проверяем, что при создании заказа нас действительно перенаправляют на страницу с подтверждением
        //также проверим id модели
        public void CheckOut_Calls_Service_And_Return_Redirect()
        {
            #region Arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
            }));

            // setting up cartService
            _mockCartService
                .Setup(c => c.TransformCart())
                .Returns(new CartViewModel
                {
                    Items = new Dictionary<ProductViewModel, int>()
                    {
                        { new ProductViewModel(), 1 }
                    }
                });

            // setting up ordersService
            _mockOrdersService
                .Setup(c => c.CreateOrder(
                    It.IsAny<CreateOrderDto>(),
                    It.IsAny<string>()))
                .Returns(new OrderDto { Id = 1 });

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = user
                }
            };
            #endregion

            // Act
            var result = _controller.CheckOut(new OrderViewModel
            {
                Name = "test",
                Address = "",
                Phone = ""
            });

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            // имя контроллера должно быть пустым при редиректе
            Assert.Null(redirectResult.ControllerName);
            // имя action-метода должно быть "OrderConfirmed" (куда перенаправили)
            Assert.Equal("OrderConfirmed", redirectResult.ActionName);
            // id заказа = 1 (какой и передали в модели)
            Assert.Equal(1, redirectResult.RouteValues["id"]);
        }

    }
}
