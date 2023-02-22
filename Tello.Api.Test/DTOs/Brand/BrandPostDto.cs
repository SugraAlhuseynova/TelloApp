
using System.ComponentModel.DataAnnotations;

namespace Tello.Api.Test.DTOs.Brand
{
    public class BrandPostDto
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}
