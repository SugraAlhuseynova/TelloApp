namespace Tello.Api.Test.DTOs.ProductItem
{
    public class ProductItemListItemGetDto
    {
        public int Id { get; set; }
        public float CostPrice { get; set; }
        public float SalePrice { get; set; }
        public int Count { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
    }
}
