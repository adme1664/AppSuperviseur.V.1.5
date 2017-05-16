using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Exceptions
{
   public class IPAdressException:Exception
    {
       public string Message { get; set; }
       public IPAdressException()
       {

       }
       public IPAdressException(string message)
       {
           this.Message = message;
       }
    }
}
