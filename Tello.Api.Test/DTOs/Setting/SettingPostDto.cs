
using System.ComponentModel.DataAnnotations;

namespace Tello.Api.Test.DTOs.Setting
{
    public class SettingPostDto
    {
        public string Key { get; set; }
        [Required]
        [MaxLength(30)]
        public string Value { get; set; }
    }
}
