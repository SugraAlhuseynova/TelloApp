namespace Tello.Api.Test.DTOs.ProductItem
{
    public class ProductItemGetDto
    {
        public int Id { get; set; }
        public float CostPrice { get; set; }
        public float SalePrice { get; set; }
        public int Count { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
        public List<int> ProductItemVariationIds { get; set; }
    }
}
