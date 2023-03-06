using Tello.Api.Test.DTOs.Category;
using Tello.Api.Test.DTOs.Product;
using Tello.Api.Test.DTOs.ProductItem;
using Tello.Api.Test.DTOs.Variation;
using Tello.Api.Test.DTOs.VariationCategory;
using Tello.Api.Test.DTOs.VariationOption;

namespace Tello.Api.Test.ViewModels.ProductItem
{
    public class ProductItemViewModel
    {
        public List<ProductSelectDto> Products { get; set; }
        public List<VariationOptionSelectDto> VariationOptions { get; set; }
        public List<VariationGetDto> Variations { get; set; }
        public List<CategoryGetDto> Categories { get; set; }
        public List<VariationCategoryGetDto> VariationCategories { get; set; }
        public ProductItemPostDto PostDto { get; set; }
    }

}