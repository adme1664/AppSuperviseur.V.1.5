using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Models
{
   public class EmigreCEModel
    {
       public long Id { get; set; }
        public long EmigreId { get; set; }
        public Nullable<long> MenageId { get; set; }
        public Nullable<long> LogeId { get; set; }
        public Nullable<long> BatimentId { get; set; }
        public string SdeId { get; set; }
        public Nullable<long> Qn1numeroOrdre { get; set; }
        public Nullable<long> Qn1Emigration { get; set; }
        public Nullable<long> Qn1NbreEmigreF { get; set; }
        public Nullable<long> Qn1NbreEmigreG { get; set; }
        public Nullable<long> Statut { get; set; }
        public Nullable<long> IsValidated { get; set; }
        public string DateDebutCollecte { get; set; }
        public string DateFinCollecte { get; set; }
        public Nullable<long> DureeSaisie { get; set; }
        public Nullable<long> IsContreEnqueteMade { get; set; }
    }
}
