using Tello.Api.Test.DTOs.ProductItem;
using Tello.Api.Test.DTOs.ProductItemVariation;
using Tello.Api.Test.DTOs.VariationOption;

namespace Tello.Api.Test.ViewModels.ProductItemVariation
{
    public class ProductItemVariationViewModel
    {
        public List<ProductItemSelectDto> ProductItems { get; set; }
        public List<VariationOptionSelectDto> VariationOptions { get; set; }
        public ProductItemVariationPostDto PostDto { get; set; }
    }
}
