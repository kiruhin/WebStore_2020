using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore_2020.Models
{
    public class CarModel
    {
        public int Id { get; set; }
        public string Mark { get; set; }
        public string Model { get; set; }
        /// <summary>
        /// Тип кузова
        /// </summary>
        public string BodyType { get; set; }
        /// <summary>
        /// год выпуска
        /// </summary>
        public int YearRelease { get; set; }
        public string Transmission { get; set; }
        public string Color { get; set; }
        /// <summary>
        /// Привод: задний, полный, передний
        /// </summary>
        public string Drive { get; set; }
        /// <summary>
        /// Объем двигателя
        /// </summary>
        public double EngineVolume { get; set; }
    }
}
