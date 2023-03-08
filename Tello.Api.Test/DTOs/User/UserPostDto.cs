using System.ComponentModel.DataAnnotations;

namespace Tello.Api.Test.DTOs.User
{
    public class UserPostDto
    {
        [MaxLength(30)]
        public string Fullname { get; set; }
        [MaxLength(40)]
        public string Email { get; set; }
        [Range(6,16)]
        public string Password { get; set; }
        [MinLength(10), MaxLength(10)]
        public string PhoneNumber { get; set; }
    }
}
