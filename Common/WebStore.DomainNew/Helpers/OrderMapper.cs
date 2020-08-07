using System.Linq;
using WebStore.DomainNew.Dto.Order;
using WebStore.DomainNew.Entities;

namespace WebStore.DomainNew.Helpers
{
    public static class OrderMapper
    {
        public static OrderDto ToDto(this Order o) =>
            new OrderDto
            {
                Id = o.Id,
                Name = o.Name,
                Address = o.Address,
                Date = o.Date,
                Phone = o.Phone,
                OrderItems = o.OrderItems.Select(oi => new OrderItemDto
                {
                    Id = oi.Id,
                    Price = oi.Price,
                    Quantity = oi.Quantity
                })
            };
    }
}