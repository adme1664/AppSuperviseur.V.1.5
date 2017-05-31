using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Json
{
   public class ProblemeJson
    {
        public long problemeId { get; set; }
        public string sdeId { get; set; }
        public Nullable<long> batimentId { get; set; }
        public string objet { get; set; }
        public string domaine { get; set; }
        public string codeQuestion { get; set; }
        public string nature { get; set; }
        public Nullable<long> statut { get; set; }
    }
}
