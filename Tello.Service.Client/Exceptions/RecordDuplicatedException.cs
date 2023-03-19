using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tello.Service.Client.Exceptions
{
    public class RecordDuplicatedException:Exception
    {
        public RecordDuplicatedException(string msg) : base(msg)
        {
        }
    }
}
