using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebStore.Controllers;
using WebStore.Interfaces;
using Xunit;
using Xunit.Abstractions;

namespace WebStore.XUnitTests
{
    public class HomeControllerTests
    {
        private readonly ITestOutputHelper _output;
        private HomeController _controller;

        //ctor
        public HomeControllerTests(ITestOutputHelper output)
        {
            _output = output;
            var mockService = new Mock<IValuesService>();
            mockService
                .Setup(c => c.GetAsync())
                .ReturnsAsync(new List<string> { "1", "2" });

            _controller = new HomeController(mockService.Object, null);
        }

        public static IEnumerable<object[]> TestData
            => new object[][] {
                new object[] { 42 },
                new object[] { 21.12 },
                new object[] { DateTime.Now },
                new object[] { null }
            };

        [Theory]
        [MemberData(nameof(TestData))]
        public void UnrunTestRepro(object data)
        {
            Assert.NotNull(data);
        }

        [Theory(DisplayName = "Add Numbers")]
        [InlineData(4, 5, 9)]
        [InlineData(2, 3, 5)]
        public void TestAddNumbers(int x, int y, int expectedResult)
        {
            _output.WriteLine($"current x={x}");
        
            Assert.Equal(4, x);
            Assert.Equal(5, y);
        }

        [Fact]
        public async Task Index_Method_Returns_View_With_Values()
        {
            _output.WriteLine("This is extra output...");

            // Arrange - подготовка

            // Act – проверяемое действие
            var result = await _controller.Index();

            // Assert – проверки утверждениями
            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<IEnumerable<string>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public void ContactUs_Returns_View()
        {
            // Act – проверяемое действие
            var result = _controller.ContactUs();
         
            // Assert – проверки утверждениями
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ErrorStatus_404_Redirects_to_NotFound()
        {
            // Act
            var result = _controller.ErrorStatus("404");

            // Assert
            var redirectToActionResult = Xunit.Assert.IsType<RedirectToActionResult>(result);
            Xunit.Assert.Null(redirectToActionResult.ControllerName);

            Xunit.Assert.Equal("NotFound", redirectToActionResult.ActionName);
        }


        [Fact]
        public void Checkout_Returns_View()
        {
            var result = _controller.Checkout();
            Xunit.Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void BlogSingle_Returns_View()
        {
            var result = _controller.BlogSingle();
            Xunit.Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Blog_Returns_View()
        {
            var result = _controller.Blog();
            Xunit.Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Error_Returns_View()
        {
            var result = _controller.Error();
            Xunit.Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void NotFound_Returns_View()
        {
            var result = _controller.NotFound();
            Xunit.Assert.IsType<ViewResult>(result);
        }

    }
}
