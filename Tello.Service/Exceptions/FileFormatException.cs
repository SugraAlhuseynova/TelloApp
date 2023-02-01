using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tello.Service.Exceptions
{
    internal class FileFormatException:Exception
    {
        public FileFormatException(string msg) : base(msg) 
        {

        }
    }
}
