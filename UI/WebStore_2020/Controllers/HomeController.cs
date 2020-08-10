using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WebStore.Infrastructure;
using WebStore.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebStore.Controllers
{
    [SimpleActionFilter]
    public class HomeController : Controller
    {
        private readonly IValuesService _valueService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IValuesService valueService, ILogger<HomeController> logger)
        {
            _valueService = valueService;
            _logger = logger;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            _logger?.LogInformation("index action requested");

            _logger?.LogTrace("trace! winter is coming!");
            _logger?.LogInformation("info! winter is coming!");
            _logger?.LogWarning("warning! winter is coming!");
            _logger?.LogDebug("debug! winter is coming!");
            _logger?.LogError("error! winter is coming!");
            _logger?.LogCritical("critical! winter is coming!");

            throw new ApplicationException("Ошибочка вышла...");
            var values = await _valueService.GetAsync();
            return View(values);
        }

        public IActionResult ErrorStatus(string id)
        {
            if (id == "404")
                return RedirectToAction("NotFound");

            return Content($"Статуcный код ошибки: {id}");
        }
 
        public IActionResult Error()
        {
            return View();
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
