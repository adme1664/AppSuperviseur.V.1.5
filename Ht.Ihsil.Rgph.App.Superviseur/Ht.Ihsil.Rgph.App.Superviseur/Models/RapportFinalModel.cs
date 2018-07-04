using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Models
{
    public class RapportFinalModel
    {
        public Nullable<long> batimentId { get; set; }
        public Nullable<long> logeId { get; set; }
        public Nullable<long> menageId { get; set; }
        public Nullable<long> repondantPrincipalId { get; set; }
        public Nullable<long> aE_EsKeGenMounKiEde { get; set; }
        public Nullable<long> aE_IsVivreDansMenage { get; set; }
        public Nullable<long> aE_RepondantQuiAideId { get; set; }
        public Nullable<long> f_EsKeGenMounKiEde { get; set; }
        public Nullable<long> f_IsVivreDansMenage { get; set; }
        public Nullable<long> f_RepondantQuiAideId { get; set; }
        public string dateDebutCollecte { get; set; }
        public string dateFinCollecte { get; set; }
        public Nullable<long> dureeSaisie { get; set; }
        public Nullable<long> isContreEnqueteMade { get; set; }
        public string codeAgentRecenceur { get; set; }
        public long rapportFinalId { get; set; }
        public string name { get; set; }
    }
}
