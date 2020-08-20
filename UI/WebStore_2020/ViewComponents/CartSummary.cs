using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.ViewComponents
{
    public class CartSummary : ViewComponent
    {
        private readonly ICartService _cartService;

        public CartSummary(ICartService cartService)
        {
            _cartService = cartService;
        }

        public IViewComponentResult Invoke() =>
            View(_cartService.TransformCart());
    }
}
