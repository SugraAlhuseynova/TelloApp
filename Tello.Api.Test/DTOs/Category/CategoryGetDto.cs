using Tello.Api.Test.DTOs.Product;

namespace Tello.Api.Test.DTOs.Category
{
    public class CategoryGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductSelectDto> Products { get; set; }
    }
}
