using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.DomainNew.Dto
{
    public class PagedProductDto
    {
        /// <summary>Выборка продуктов для текущей страницы</summary>
        public IEnumerable<ProductDto> Products { get; set; }

        /// <summary>Общее количество в запросе</summary>
        public int TotalCount { get; set; }
    }

}
