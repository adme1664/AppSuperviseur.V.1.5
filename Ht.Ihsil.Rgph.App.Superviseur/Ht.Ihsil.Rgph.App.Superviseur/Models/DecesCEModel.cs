using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Models
{
   public class DecesCEModel
    {
        public long Id { get; set; }
        public long DecesId { get; set; }
        public long MenageId { get; set; }
        public long LogeId { get; set; }
        public long BatimentId { get; set; }
        public string SdeId { get; set; }
        public Nullable<long> Qd2NoOrdre { get; set; }
        public Nullable<long> Qd1Deces { get; set; }
        public Nullable<long> Qd2bAgeDecede { get; set; }
        public Nullable<long> Qd1aNbreDecesF { get; set; }
        public Nullable<long> Qd1aNbreDecesG { get; set; }
        public Nullable<long> Statut { get; set; }
        public Nullable<long> IsValidated { get; set; }
        public string DateDebutCollecte { get; set; }
        public string DateFinCollecte { get; set; }
        public Nullable<long> DureeSaisie { get; set; }
        public Nullable<long> IsContreEnqueteMade { get; set; }
        public bool IsValidate { get; set; }
    }
}
