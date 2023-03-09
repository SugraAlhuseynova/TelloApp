using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tello.Service.Apps.Admin.DTOs.AppUserDTOs.RoleDtos
{
    public class RoleGetDto
    {
        public string Id { get; set; }
        public string Role { get; set; }
        public int UsersCount { get; set; }
    }
}
