using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Models
{
    public class RetourModel
    {
        public long RetourId { get; set; }
        public Nullable<long> BatimentId { get; set; }
        public Nullable<long> LogementId { get; set; }
        public Nullable<long> MenageId { get; set; }
        public Nullable<long> DecesId { get; set; }
        public Nullable<long> EmigreId { get; set; }
        public Nullable<long> IndividuId { get; set; }
        public string SdeId { get; set; }
        public string DateRetour { get; set; }
        public string Raison { get; set; }
        public string Statut { get; set; }
    }
}
