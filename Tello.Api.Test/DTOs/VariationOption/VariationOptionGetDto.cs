using Tello.Api.Test.DTOs.VariationCategory;

namespace Tello.Api.Test.DTOs.VariationOption
{
    public class VariationOptionGetDto
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public VariationCategoryGetDto VariationCategory { get; set; }
    }
}
