using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebStore_2020.Models;

namespace WebStore_2020.Controllers
{
    public class CarController : Controller
    {
        List<Models.CarModel> _cars;

        public CarController()
        {
            _cars = new List<CarModel>
            {
                new CarModel
                {
                    Id = 1,
                    BodyType = "Хэтчбек",
                    Color = "Красный",
                    Drive= "Передний",
                    EngineVolume = 1.6,
                    Mark = "Nissan",
                    Model = "Almera",
                    Transmission = "МКПП",
                    YearRelease = 2018
                },
                new CarModel
                {
                    Id = 2,
                    BodyType = "Х",
                    Color = "Черный",
                    Drive= "Задний",
                    EngineVolume = 1.6,
                    Mark = "ВАЗ 2107",
                    Model = "LADA",
                    Transmission = "МКПП",
                    YearRelease = 2004
                }
            };
        }

        public IActionResult Index()
        {
            return View(_cars);
        }
        public IActionResult Details(int Id)
        {
            return View(_cars.FirstOrDefault(x=>x.Id==Id));
        }
    }
}