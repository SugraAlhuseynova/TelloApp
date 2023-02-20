
using System.ComponentModel.DataAnnotations;

namespace Tello.Api.Test.Models
{
    public class BrandPostDto
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}
