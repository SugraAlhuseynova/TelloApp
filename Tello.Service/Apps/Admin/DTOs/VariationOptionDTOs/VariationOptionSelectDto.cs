using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tello.Service.Apps.Admin.DTOs.VariationOptionDTOs
{
    public class VariationOptionSelectDto
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string VariationName { get; set; }
        public string CategoryName { get; set; }
    }
}
