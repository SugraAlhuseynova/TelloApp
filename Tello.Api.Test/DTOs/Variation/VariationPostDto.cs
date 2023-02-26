using System.ComponentModel.DataAnnotations;

namespace Tello.Api.Test.DTOs.Variation
{
    public class VariationPostDto
    {
        [Required]
        [MaxLength(35)]
        public string Name { get; set; }
    }
}
