using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Models
{
   public class LogementIndividuelModel
    {
        private string logementName;

        public string LogementName
        {
            get { return logementName; }
            set { logementName = value; }
        }
        private string batimentId;

        public string BatimentId
        {
            get { return batimentId; }
            set { batimentId = value; }
        }
        private string sdeId;

        public string SdeId
        {
            get { return sdeId; }
            set { sdeId = value; }
        }
    }
}
