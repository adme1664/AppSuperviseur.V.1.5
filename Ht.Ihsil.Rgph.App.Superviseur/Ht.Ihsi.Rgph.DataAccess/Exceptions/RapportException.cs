using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsi.Rgph.DataAccess.Exceptions
{
   public class RapportException:Exception
    {
       public string Message { get; set; }
       public RapportException()
       {

       }
       public RapportException(string message)
       {
           this.Message = message;
       }
    }
}
