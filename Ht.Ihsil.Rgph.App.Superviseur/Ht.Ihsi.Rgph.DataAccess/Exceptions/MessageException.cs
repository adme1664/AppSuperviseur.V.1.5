using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsi.Rgph.DataAccess.Exceptions
{
   public class MessageException: Exception
    {
        public String Message { get; set; }
        public MessageException()
        {

        }
        public MessageException(string message)
        {
            this.Message = message;
        }

    }
}
