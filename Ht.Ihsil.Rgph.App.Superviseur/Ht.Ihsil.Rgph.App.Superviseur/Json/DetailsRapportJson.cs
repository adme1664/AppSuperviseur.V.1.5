using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Json
{
    public class DetailsRapportJson
    {
        public long detailsRapportId { get; set; }
        public long rapportId { get; set; }
        public long domaine { get; set; }
        public long sousDomaine { get; set; }
        public Nullable<long> probleme { get; set; }
        public Nullable<long> solution { get; set; }
        public string precisions { get; set; }
        public string suggestions { get; set; }
        public string suivi { get; set; }
        public string commentaire { get; set; }
    }
}
