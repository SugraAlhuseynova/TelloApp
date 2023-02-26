using Tello.Api.Test.DTOs.VariationCategory;

namespace Tello.Api.Test.DTOs.VariationOption
{
    public class VariationOptionListItemGetDto
    {
        public int Id { get; set; }
        //public string VariationName { get; set; }
        //public string CategoryName { get; set; }
        public string Value { get; set; }
        public VariationCategoryGetDto VariationCategory { get; set; }
    }
}
