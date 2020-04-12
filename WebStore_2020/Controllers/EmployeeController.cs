using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore_2020.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebStore_2020.Controllers
{
    public class EmployeeController : Controller
    {
        List<EmployeeViewModel> _employeees;

        public EmployeeController()
        {
            _employeees = new List<EmployeeViewModel> {
                new EmployeeViewModel
                {
                    Id = 1,
                    FirstName = "Иван",
                    SurName = "Иванов",
                    Patronymic = "Иванович",
                    Age = 22,
                    Position = "Начальник"
                },
                new EmployeeViewModel
                {
                    Id = 2,
                    FirstName = "Владислав",
                    SurName = "Петров",
                    Patronymic = "Иванович",
                    Age = 35,
                    Position = "Программист"
                }
            };
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            //return Content("Hello from home controller");
            return View(_employeees);
        }

        // GET: /<controller>/details/{id}
        public IActionResult Details(int id)
        {
            return View(_employeees.FirstOrDefault(x => x.Id == id));
        }
    }
}
