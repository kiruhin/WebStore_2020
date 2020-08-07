using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DomainNew.Dto.Order;
using WebStore.DomainNew.Entities;
using WebStore.Models;

namespace WebStore.Infrastructure.Interfaces
{
    public interface IOrdersService
    {
        IEnumerable<OrderDto> GetUserOrders(string userName);
        OrderDto GetOrderById(int id);
        OrderDto CreateOrder(CreateOrderDto orderModel, string userName);
    }
}

