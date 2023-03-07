using Tello.Api.Test.DTOs.ProductItem;
using Tello.Api.Test.DTOs.VariationOption;

namespace Tello.Api.Test.ViewModels.ProductItem
{
    public class ProductItemDetailViewModel
    {
        public List<VariationOptionSelectDto> VariationOptions { get; set; }
        public ProductItemGetDto GetDto { get; set; } 
    }
}
