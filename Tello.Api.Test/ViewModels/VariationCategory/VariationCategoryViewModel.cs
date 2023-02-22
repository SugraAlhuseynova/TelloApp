using Tello.Api.Test.DTOs.Category;
using Tello.Api.Test.DTOs.Variation;
using Tello.Api.Test.DTOs.VariationCategory;

namespace Tello.Api.Test.ViewModels.VariationCategory
{
    public class VariationCategoryViewModel
    {
        public List<CategoryGetDto> Categories { get; set; }
        public List<VariationGetDto> Variations { get; set; }
        public VariationCategoryPostDto PostDto { get; set; }
    }
}
