using System.Collections.Generic;
using WebStore.Models;

namespace WebStore.DomainNew.Dto.Order
{
    public class CreateOrderDto
    {
        public OrderViewModel OrderViewModel { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}