using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Exceptions
{
    class DeviceNotConnectedException : Exception
    {
        public string Message {get; set;}
        public DeviceNotConnectedException()
        {

        }

        public DeviceNotConnectedException(string message)
        {
            this.Message = message;
        }
    }
}
