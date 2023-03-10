using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tello.Core.Entities
{
    public class AppUser : IdentityUser
    {
        public string Fullname { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsBlocked { get; set; }
    }
}
