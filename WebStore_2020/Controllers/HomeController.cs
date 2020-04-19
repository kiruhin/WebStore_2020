using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore_2020.Infrastructure;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebStore_2020.Controllers
{
    [SimpleActionFilter]
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            //throw new ApplicationException("Ошибочка вышла...");
            return View();
        }

        // GET: /<controller>/blog
        [SimpleActionFilter]
        public IActionResult Blog()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }

        public IActionResult Checkout()
        {
            return View();
        }

        public IActionResult Cart()
        {
            return View();
        }

        public IActionResult BlogSingle()
        {
            return View();
        }

        public IActionResult NotFound()
        {
            return View();
        }
    }
}
