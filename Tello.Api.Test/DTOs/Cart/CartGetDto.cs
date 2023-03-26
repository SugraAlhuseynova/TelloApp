namespace Tello.Api.Test.DTOs.Cart
{
    public class CartGetDto
    {
        public int Id { get; set; }
        public double TotalAmount { get; set; }
        public string OrderStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserFullname { get; set; }
        public int Count { get; set; }

    }
}
