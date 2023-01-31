using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tello.Service.Exceptions
{
    public class ItemNotFoundException:Exception
    {
        public ItemNotFoundException(string msg):base(msg)
        {
        }
    }
}
