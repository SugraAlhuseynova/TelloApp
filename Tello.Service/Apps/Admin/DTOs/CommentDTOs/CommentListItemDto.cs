using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tello.Service.Apps.Admin.DTOs.CommentDTOs
{
    public class CommentListItemDto
    {
        public int Id { get; set; }
        public string Desc { get; set; }
        public int ProductItemId { get; set; }
        public string ProductName { get; set; }
        public string AppUserId { get; set; }
        public string AppName { get; set; }
    }
}
