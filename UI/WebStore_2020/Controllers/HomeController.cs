using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebStore.Infrastructure;
using WebStore.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebStore.Controllers
{
    [SimpleActionFilter]
    public class HomeController : Controller
    {
        private readonly IValuesService _valueService;

        public HomeController(IValuesService valueService)
        {
            _valueService = valueService;
        }

        // GET: /<controller>/
        public async Task<IActionResult> IndexAsync()
        {
            //throw new ApplicationException("Ошибочка вышла...");
            var values = await _valueService.GetAsync();
            return View(values);
        }

        // GET: /<controller>/blog
        [SimpleActionFilter]
        public IActionResult Blog()
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
