using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Models
{
  public  class LogementTypeModel
    {
        private string logementName;
        private long batimentId;
        private string recBatiment;


        public string LogementName
        {
            get { return logementName; }
            set { logementName = value; }
        }
        public LogementTypeModel(string logementName)
        {
            this.logementName = logementName;
        }
        public long BatimentId
        {
            get { return this.batimentId; }
            set { batimentId = value; }
        }

        
        public string RecBatiment
        {
            get { return recBatiment; }
            set { recBatiment = value; }
        }
    }
}
