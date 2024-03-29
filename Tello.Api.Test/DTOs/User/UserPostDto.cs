﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Tello.Api.Test.DTOs.User
{
    public class UserPostDto
    {
        [MaxLength(30)]
        public string Fullname { get; set; }
        [MaxLength(40)]
        public string Email { get; set; }
        [Range(6,16)]
        [AllowNull]
        public string Password { get; set; }
        [MinLength(10), MaxLength(10)]
        public string PhoneNumber { get; set; }
        public List<string> RolesIds { get;set; }
    }
}
