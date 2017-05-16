using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Exceptions
{
   public class ChefMenageException:Exception
    {
       public string Message { get; set; }
       public ChefMenageException()
       {

       }
       public ChefMenageException(string message)
       {
           this.Message = message;
       }
    }
}
