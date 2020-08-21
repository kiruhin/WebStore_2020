using System.Collections.Generic;
using System.Linq;
using Moq;
using WebStore.Domain;
using WebStore.DomainNew.Dto;
using WebStore.DomainNew.ViewModels;
using WebStore.Infrastructure.Interfaces;
using WebStore.Interfaces;
using WebStore.Models;
using WebStore.Services;
using Xunit;

namespace WebStore.XUnitTests
{
    public class CartServiceTests
    {
        [Fact]
        // проверим правильншость подсчета суммарного количества товаров в корзине
        public void Cart_Class_ItemsCount_Returns_Correct_Quantity()
        {
            // Arrange
            var cart = new Cart
            {
                Items = new List<CartItem>
                {
                    new CartItem
                    {
                        ProductId = 1,
                        Quantity = 10
                    },
                    new CartItem
                    {
                        ProductId = 3,
                        Quantity = 5
                    }
                }
            };

            // Act
            var result = cart.ItemsCount;

            // Assert
            // результат должен быть 10 + 5 = 15
            Assert.Equal(15, result);
        }

        [Fact]
        // проверим правильншость подсчета суммарного количества товаров в модели представления корзины
        public void CartViewModel_Returns_Correct_ItemsCount()
        {
            // Arrange
            var cartViewModel = new CartViewModel
            {
                Items = new Dictionary<ProductViewModel, int>
                {
                    {
                        new ProductViewModel
                        {
                            Id = 1,
                            Name = "TestItem",
                            Price = 5.0m
                        },
                        10
                    },
                    {
                        new ProductViewModel
                        {
                            Id = 2,
                            Name = "TestItem2",
                            Price = 1.0m
                        },
                        20
                    },
                }
            };

            // Act
            var result = cartViewModel.ItemsCount;

            // Assert
            // результат должен быть 10 + 20 = 20
            Assert.Equal(30, result);
        }

        [Fact]
        public void CartService_AddToCart_WorksCorrect()
        {
            // Arrange
            // подготовим пустую корзину
            var cart = new Cart
            {
                Items = new List<CartItem>()
            };

            var productData = new Mock<IProductService>();
            var cartStore = new Mock<ICartStore>();
            cartStore.Setup(c => c.Cart).Returns(cart);

            var cartService = new CartService(productData.Object, cartStore.Object);

            // Act
            cartService.AddToCart(5);

            // Assert
            Assert.Equal(1, cart.ItemsCount);
            Assert.Equal(1, cart.Items.Count);
            Assert.Equal(5, cart.Items[0].ProductId);
        }

        [Fact]
        public void CartService_AddToCart_Increment_Quantity()
        {
            // Arrange
            // подготовим корзину с товарами
            var cart = new Cart()
            {
                Items = new List<CartItem>
                {
                    new CartItem { ProductId = 5, Quantity = 2 }
                }
            };

            var productData = new Mock<IProductService>();
            var cartStore = new Mock<ICartStore>();
            cartStore.Setup(c => c.Cart).Returns(cart);

            var cartService = new CartService(productData.Object, cartStore.Object);

            // Act
            cartService.AddToCart(5);

            // Assert
            Assert.Equal(1, cart.Items.Count);
            Assert.Equal(3, cart.ItemsCount);
        }

        [Fact]
        public void CartService_RemoveFromCart_Removes_Correct_Item()
        {
            // Arrange
            // корзина с товарами
            var cart = new Cart()
            {
                Items = new List<CartItem>()
                {
                    new CartItem { ProductId = 1, Quantity = 3},
                    new CartItem { ProductId = 2, Quantity = 1}
                }
            };

            var productData = new Mock<IProductService>();
            var cartStore = new Mock<ICartStore>();
            cartStore.Setup(c => c.Cart).Returns(cart);

            var cartService = new CartService(productData.Object, cartStore.Object);

            // Act
            // удаляем товар
            cartService.RemoveFromCart(1);

            // Assert
            // должен остаться 1 товар в корзине
            Assert.Equal(1, cart.Items.Count);
            Assert.Equal(2, cart.Items[0].ProductId);
        }

        [Fact]
        public void CartService_RemoveAll_Clear_Cart()
        {
            // Arrange
            // корзина с товарами
            var cart = new Cart()
            {
                Items = new List<CartItem>()
                {
                    new CartItem(){ProductId = 1,Quantity = 3},
                    new CartItem(){ProductId = 2, Quantity = 1}
                }
            };

            var productData = new Mock<IProductService>();
            var cartStore = new Mock<ICartStore>();
            cartStore.Setup(c => c.Cart).Returns(cart);

            var cartService = new CartService(productData.Object, cartStore.Object);

            // Act
            // удаляем все
            cartService.RemoveAll();

            // Assert
            // должна быть пустая корзина
            Assert.Equal(0, cart.Items.Count);
        }

        [Fact]
        public void CartService_Remove_Item_When_Decrement()
        {
            // Arrange
            // такая же корзина, как и в прежнем тесте
            var cart = new Cart()
            {
                Items = new List<CartItem>()
                {
                    new CartItem(){ProductId = 1,Quantity = 3},
                    new CartItem(){ProductId = 2, Quantity = 1}
                }
            };

            var productData = new Mock<IProductService>();
            var cartStore = new Mock<ICartStore>();
            cartStore.Setup(c => c.Cart).Returns(cart);

            var cartService = new CartService(productData.Object, cartStore.Object);

            // Act
            cartService.DecrementFromCart(2);

            // Assert
            // осталось 3 из 4 товаров
            Assert.Equal(3, cart.ItemsCount);
            // осталось только 1 наименование товара
            Assert.Equal(1, cart.Items.Count);
        }

        [Fact]
        public void CartService_TransformCart_WorksCorrect()
        {
            // Arrange
            var cart = new Cart()
            {
                Items = new List<CartItem>()
                {
                    new CartItem(){ProductId = 1, Quantity = 4}
                }
            };

            var products = new List<ProductDto>()
            {
                new ProductDto()
                {
                    Id = 1,
                    ImageUrl = "",
                    Name = "Test",
                    Order = 0,
                    Price = 1.11m,
                }
            };

            PagedProductDto pagedProducts = new PagedProductDto {Products = products};

            var productData = new Mock<IProductService>();
            productData.Setup(c => c.GetProducts(It.IsAny<ProductFilter>())).Returns(pagedProducts);
            var cartStore = new Mock<ICartStore>();
            cartStore.Setup(c => c.Cart).Returns(cart);

            var cartService = new CartService(productData.Object, cartStore.Object);

            // Act
            var result = cartService.TransformCart();

            // Assert
            Assert.Equal(4, result.ItemsCount);
            Assert.Equal(1.11m, result.Items.First().Key.Price);
        }

    }
}
