using Tello.Core.Enums;

namespace Tello.Core.Entities
{
    public class Card : BaseEntity
    {
        //public Card()
        //{
        //    TotalAmount = ProductOrders.Select(x=>x.Price).Sum();
        //}
        public double TotalAmount { get; set; }
        public bool IsConfirmed { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public List<ProductOrder> ProductOrders { get; set;}
    }
}
