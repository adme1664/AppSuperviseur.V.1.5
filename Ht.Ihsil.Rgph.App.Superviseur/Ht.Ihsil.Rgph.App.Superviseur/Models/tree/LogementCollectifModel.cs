using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Models
{
   public class LogementCollectifModel
    {
        private string logementCollectif;

        public string LogementCollectif
        {
            get { return logementCollectif; }
            set { logementCollectif = value; }
        }
        private string batimentId;
        private string sdeId;

        public string SdeId
        {
            get { return sdeId; }
            set { sdeId = value; }
        }
        public string BatimentId
        {
            get { return batimentId; }
            set { batimentId = value; }
        }
    }
}
