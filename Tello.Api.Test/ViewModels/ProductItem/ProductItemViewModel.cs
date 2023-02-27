using Tello.Api.Test.DTOs.Product;
using Tello.Api.Test.DTOs.ProductItem;

namespace Tello.Api.Test.ViewModels.ProductItem
{
    public class ProductItemViewModel
    {
        public List<ProductGetDto> Products { get; set; }
        public ProductItemPostDto PostDto { get; set; }
    }

}