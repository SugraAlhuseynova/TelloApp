using System.ComponentModel.DataAnnotations;

namespace Tello.Api.Test.DTOs.Product
{
    public class ProductPostDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(200)]
        public string Desc { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }

    }
}
