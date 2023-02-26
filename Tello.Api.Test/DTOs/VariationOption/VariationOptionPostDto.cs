using System.ComponentModel.DataAnnotations;

namespace Tello.Api.Test.DTOs.VariationOption
{
    public class VariationOptionPostDto
    {
        public int VariationCategoryId { get; set; }
        [MaxLength(50)]
        [Required]
        public string Value { get; set; }
    }
}
