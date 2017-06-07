using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Json
{
    public class RapportDeroulementJson
    {
        public long rapportId { get; set; }
        public string districtId { get; set; }
        public string dateRapport { get; set; }
        public string rapportName
        {
            get;
            set;
        }
        public List<DetailsRapportJson> rdcDetails { get; set; }
    }
}
