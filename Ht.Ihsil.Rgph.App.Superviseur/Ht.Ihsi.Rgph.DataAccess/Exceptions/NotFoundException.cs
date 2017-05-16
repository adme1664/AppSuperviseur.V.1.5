using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsi.Rgph.DataAccess.Exceptions
{
   public class NotFoundException:Exception
    {
       public string Message { get; set; }
       public NotFoundException()
       {

       }
       public NotFoundException(string message)
       {
           this.Message = message;
       }
    }
}
