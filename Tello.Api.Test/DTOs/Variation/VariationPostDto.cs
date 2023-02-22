using System.ComponentModel.DataAnnotations;

namespace Tello.Api.Test.DTOs.Variation
{
    public class VariationPostDto
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}
