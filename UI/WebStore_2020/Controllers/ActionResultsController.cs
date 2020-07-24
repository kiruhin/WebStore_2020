using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using WebStore.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebStore.Controllers
{
    public class ActionResultsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MereContentString(string name)
        {
            return Content($"Hi, {name}. I'm mere string content result");
        }

        /// <summary>
        /// Returns nothing and StatusCode = 200
        /// </summary>
        /// <returns></returns>
        public IActionResult Nothing()
        {
            return new EmptyResult();
        }

        /// <summary>
        /// Returns nothing and StatusCode = 204
        /// </summary>
        /// <returns></returns>
        public IActionResult Nothing204()
        {
            return new NoContentResult();
        }

        public JsonResult JsonObjectSerialized()
        {
            var employee = new EmployeeViewModel
            {
                Id = 1,
                FirstName = "Иван",
                SurName = "Иванов",
                Patronymic = "Иванович",
                Age = 22,
                Position = "Начальник"
            };

            return Json(employee);
        }

        public IActionResult GoGoogle()
        {
            return Redirect("https://google.com");
        }

        public IActionResult GoToHomePage()
        {
            return LocalRedirect("~/Home/Index");
        }

        public IActionResult RedirectWithParameters()
        {
            return RedirectToAction("MereContentString", "ActionResults", new { name = "Dear user" });
        }

        public IActionResult ForbiddenResource()
        {
            //return Forbid(); // the same
            return StatusCode(403);
        }

        public IActionResult NotFoundResource()
        {
            //return StatusCode(404, "Nothing found. Sorry.");
            return NotFound("Nothing found. Sorry.");
        }

        public IActionResult AgeCheck(int age)
        {
            if (age < 18)
                return Unauthorized("Sorry. Adults only");

            return Content("You're welcome");
        }

        public IActionResult TellMeItsOk()
        {
            return Ok("Everything is gonna be fine!");
        }

        public IActionResult ReallyBadRequest(string s)
        {
            if (String.IsNullOrEmpty(s))
                return BadRequest("Some parameter was expected");
            
            return View("Index");
        }

        public IActionResult GetFile()
        {
            // Путь к файлу
            string file_path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/blog/man-two.jpg");
            // Тип файла - content-type
            string file_type = "image/jpeg";
            // Имя файла - необязательно
            string file_name = "My awesome ring.jpg";
            return PhysicalFile(file_path, file_type, file_name);
        }

        // Отправка массива байтов
        public FileResult GetBytes()
        {
            string file_path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/15.jpg");
            byte[] mas = System.IO.File.ReadAllBytes(file_path);
            string file_type = "image/jpeg";
            string file_name = "My awesome ring.jpg";
            return File(mas, file_type, file_name);
        }

        // Отправка потока
        public FileResult GetStream()
        {
            string file_path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/15.jpg");
            FileStream fs = new FileStream(file_path, FileMode.Open);
            string file_type = "image/jpeg";
            string file_name = "My awesome ring.jpg";
            return File(fs, file_type, file_name);
        }
    }
}
