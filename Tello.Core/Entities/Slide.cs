using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tello.Core.Entities
{
    public class Slide : BaseEntity
    {
        public string Title { get; set; }
        public string Desc { get; set; }
        public byte? Order { get; set; }
        public string BackgroundPhotoStr { get; set; }
        public string ProductPhotoStr { get; set; }
        [NotMapped]
        public IFormFile BackgroundPhoto { get; set; }
        [NotMapped]
        public IFormFile ProductPhoto { get; set; }
    }
}
