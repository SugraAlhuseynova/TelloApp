using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tello.Core.Entities
{
    public class Slide : BaseEntity
    {
        public string Title { get; set; }
        public string Desc { get; set; }
        public string BackgroundPhotoStr { get; set; }
        public string ProductPhotoStr { get; set; }
        public IFormFile BackgroundPhoto { get; set; }
        public IFormFile ProductPhoto { get; set; }
    }
}
