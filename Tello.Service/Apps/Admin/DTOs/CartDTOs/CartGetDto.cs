using Tello.Service.Apps.Admin.DTOs.ProductItemDTOs;

namespace Tello.Service.Apps.Admin.DTOs.CartDTOs
{
    public class CartGetDto
    {
        public int Id { get; set; }
        public double TotalAmount { get; set; }
        public string OrderStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserFullname { get; set; }
        public List<ProductItemCardDto> ProductItems { get; set; }
    }
}
