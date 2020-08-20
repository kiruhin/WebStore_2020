using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WebStore.Domain;
using WebStore.DomainNew.ViewModels;
using WebStore.Infrastructure.Interfaces;
using WebStore.Interfaces;
using WebStore.Models;

namespace WebStore.Services
{
    public class CartService : ICartService
    {
        private readonly ICartStore _cartStore;
        private IProductService _productService { get; }

        public CartService(IProductService productService, ICartStore cartStore)
        {
            _cartStore = cartStore;
            _productService = productService;
        }

        public void DecrementFromCart(int id)
        {
            var cart = _cartStore.Cart;

            var item = cart.Items.FirstOrDefault(x => x.ProductId == id);

            if (item != null)
            {
                if (item.Quantity > 0)
                    item.Quantity--;

                if (item.Quantity == 0)
                    cart.Items.Remove(item);
            }

            _cartStore.Cart = cart;
        }

        public void RemoveFromCart(int id)
        {
            var cart = _cartStore.Cart;

            var item = cart.Items.FirstOrDefault(x => x.ProductId == id);

            if (item != null)
            {
                cart.Items.Remove(item);
            }

            _cartStore.Cart = cart;
        }

        public void RemoveAll()
        {
            _cartStore.Cart = new Cart { Items = new List<CartItem>() };
        }

        public void AddToCart(int id)
        {
            var cart = _cartStore.Cart;

            var item = cart.Items.FirstOrDefault(x => x.ProductId == id);
            if (item != null)
            {
                item.Quantity++;
            }
            else
            {
                cart.Items.Add(new CartItem() { ProductId = id, Quantity = 1 });
            }

            _cartStore.Cart = cart;
        }

        public CartViewModel TransformCart()
        {
            var products = _productService.GetProducts(new ProductFilter()
            {
                Ids = _cartStore.Cart.Items.Select(i => i.ProductId).ToList()
            }).Products.Select(p => new ProductViewModel()
            {
                Id = p.Id,
                ImageUrl = p.ImageUrl,
                Name = p.Name,
                Order = p.Order,
                Price = p.Price,
                Brand = p.Brand != null ? p.Brand.Name : string.Empty
            }).ToList();

            var r = new CartViewModel
            {
                Items = _cartStore.Cart.Items.ToDictionary(
                    x => products.First(y => y.Id == x.ProductId),
                    x => x.Quantity)
            };

            return r;

        }
    }
}
