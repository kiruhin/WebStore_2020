using System.Collections.Generic;
using System.Text;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Base;

namespace WebStore.DomainNew.Entities
{
    public class OrderItem : BaseEntity
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public virtual Order Order { get; set; } // сгенерирует внешний ключ в БД
        public virtual Product Product { get; set; } // сгенерирует внешний ключ в БД
    }
}