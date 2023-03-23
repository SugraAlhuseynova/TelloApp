namespace Tello.Api.Test.DTOs.Card
{
    public class CardGetDto
    {
        public int Id { get; set; }
        public double TotalAmount { get; set; }
        public string OrderStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserFullname { get; set; }
        public int Count { get; set; }

    }
}
