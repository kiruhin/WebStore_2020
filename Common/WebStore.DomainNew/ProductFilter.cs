using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Domain
{
    /// <summary>
    /// Класс для фильтрации товаров
    /// </summary>
    public class ProductFilter
    {
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
        public List<int> Ids { get; set; }

        
        /// <summary>Текущая страница</summary>
        public int Page { get; set; }

        /// <summary>Количество элементов на странице</summary>
        public int? PageSize { get; set; }
    }
}
