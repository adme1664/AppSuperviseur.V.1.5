using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Models
{
   public  class MenageTypeModel
    {
        private string logementId;
        private long batimentId;

        public long BatimentId
        {
            get { return batimentId; }
            set { batimentId = value; }
        }
        private string sdeId;

        public string LogementId
        {
            get { return logementId; }
            set { logementId = value; }
        }
        private string menageId;

        public string MenageId
        {
            get { return menageId; }
            set { menageId = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public MenageTypeModel(string name)
        {
            this.Name = name;
        }
        

        public string SdeId
        {
            get { return sdeId; }
            set { sdeId = value; }
        }
    }
}
