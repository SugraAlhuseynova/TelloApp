using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tello.Service.Client.Member.DTOs.CommentDTOs
{   
    public class CommentGetDto
    {
        public string Desc { get; set; }
        public int ProductItemId { get; set; }
        public string UserName { get; set; }
    }
}
