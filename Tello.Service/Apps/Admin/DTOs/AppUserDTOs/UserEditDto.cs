using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tello.Service.Apps.Admin.DTOs.AppUserDTOs
{
    public class UserEditDto
    {
        public string Fullname { get; set; }
        public string Email { get; set; }
        public List<string> RolesIds { get; set; }
    }
}
