using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Models
{
   public  class ProblemeModel
    {
        public long ProblemeId { get; set; }
        public string SdeId { get; set; }
        public Nullable<long> BatimentId { get; set; }
        public string Objet { get; set; }
        public string Domaine { get; set; }
        public string CodeQuestion { get; set; }
        public string Nature { get; set; }
        public Nullable<long> Statut { get; set; }
    }
}
