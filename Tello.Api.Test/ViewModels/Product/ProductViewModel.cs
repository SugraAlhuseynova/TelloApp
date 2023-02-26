using Tello.Api.Test.DTOs.Brand;
using Tello.Api.Test.DTOs.Category;
using Tello.Api.Test.DTOs.Product;

namespace Tello.Api.Test.ViewModels.Product
{
    public class ProductViewModel
    {
        public List<CategoryGetDto> Categories { get; set; }
        public List<BrandGetDto> Brands { get; set; }
        public ProductPostDto Product { get; set; }

    }
}
