using WebStore.Domain.Entities.Base;

namespace WebStore.DomainNew.Dto.Order
{
    public class OrderItemDto : BaseEntity
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}