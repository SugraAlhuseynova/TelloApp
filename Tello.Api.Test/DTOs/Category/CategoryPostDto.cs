using System.ComponentModel.DataAnnotations;

namespace Tello.Api.Test.DTOs.Category
{
    public class CategoryPostDto
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}
