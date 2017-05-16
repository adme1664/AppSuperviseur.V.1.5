using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Exceptions
{
    public class SautUtilisationException:Exception
    {
        public string Message { get; set; }
        public SautUtilisationException()
        {

        }
        public SautUtilisationException(string message)
        {
            this.Message = message;
        }
    }
}
