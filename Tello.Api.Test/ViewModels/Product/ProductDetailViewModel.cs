using Tello.Api.Test.DTOs.Product;
using Tello.Api.Test.DTOs.ProductItem;
using Tello.Api.Test.DTOs.VariationOption;

namespace Tello.Api.Test.ViewModels.Product
{
    public class ProductDetailViewModel
    {
        public List<ProductItemSelectDto> ProductItems { get; set; }
        public List<VariationOptionSelectDto> VariationOptions { get; set; }
        public ProductGetDto GetDto { get; set; }
    }
}
