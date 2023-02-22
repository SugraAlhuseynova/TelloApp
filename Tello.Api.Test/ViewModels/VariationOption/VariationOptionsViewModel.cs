using Tello.Api.Test.DTOs.VariationCategory;
using Tello.Api.Test.DTOs.VariationOption;

namespace Tello.Api.Test.ViewModels.VariationOption
{
    public class VariationOptionsViewModel
    {
        public List<VariationCategoryGetDto> VariationCategories { get; set; }
        public VariationOptionPostDto PostDto { get; set; }
    }
}
