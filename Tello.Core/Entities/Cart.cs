using System.ComponentModel.DataAnnotations.Schema;
using Tello.Core.Enums;

namespace Tello.Core.Entities
{
    public class Cart : BaseEntity
    {
        public double TotalAmount { get; set; }
        public bool IsConfirmed { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public List<ProductOrders> ProductOrders { get; set; }
    }
}
