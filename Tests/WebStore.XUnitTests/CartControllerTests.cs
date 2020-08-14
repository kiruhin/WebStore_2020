using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
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


        public CartControllerTests()
        {
            // инициализируем мок-сервисы и контроллер
            _mockCartService = new Mock<ICartService>();
            _mockOrdersService = new Mock<IOrdersService>();
            _controller = new CartController(_mockCartService.Object, _mockOrdersService.Object);
        }

        [Fact]
        public void CheckOut_ModelState_Invalid_Returns_ViewModel()
        {
            // Arrange
            _controller.ModelState.AddModelError("error", "InvalidModel");
            
            // Act 
            var result = _controller.CheckOut(new OrderViewModel
            {
                Name = "test"
            });
            
            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<OrderDetailsViewModel>(viewResult.ViewData.Model);
            Assert.Equal("test", model.OrderViewModel.Name);
        }

        [Fact]
        public void CheckOut_Calls_Service_And_Return_Redirect()
        {
            #region Arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
            }));

            _mockCartService
                .Setup(c => c.TransformCart())
                .Returns(new CartViewModel
                {
                    Items = new Dictionary<ProductViewModel, int>()
                    {
                        { new ProductViewModel(), 1 }
                    }
                });

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
            Assert.Null(redirectResult.ControllerName);
            Assert.Equal("OrderConfirmed", redirectResult.ActionName);
            Assert.Equal(1, redirectResult.RouteValues["id"]);
        }
    }
}
