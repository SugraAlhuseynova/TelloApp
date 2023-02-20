using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tello.Core.Entities
{
    public class Setting : BaseEntity
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

    }
}
