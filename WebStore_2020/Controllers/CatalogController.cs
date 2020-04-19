using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebStore_2020.Controllers
{
    public class CatalogController : Controller
    {
        public IActionResult Shop()
        {
            return View();
        }

        public IActionResult ProductDetails()
        {
            return View();
        }


    }
}
