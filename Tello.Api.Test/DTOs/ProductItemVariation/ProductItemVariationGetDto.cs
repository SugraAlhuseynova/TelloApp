namespace Tello.Api.Test.DTOs.ProductItemVariation
{
    public class ProductItemVariationGetDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string VariationOptionValue { get; set; }
        public string VariationName { get; set; }
        public int ProductItemId { get; set; }
        public int VariationOptionId { get;set; }
    }
}
