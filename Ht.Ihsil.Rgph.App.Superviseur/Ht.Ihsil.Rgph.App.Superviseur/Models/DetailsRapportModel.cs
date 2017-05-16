using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Models
{
    public class DetailsRapportModel
    {
        public long DetailsRapportId { get; set; }
        public long RapportId { get; set; }
        public long Domaine { get; set; }
        public long SousDomaine { get; set; }
        public Nullable<long> Probleme { get; set; }
        public Nullable<long> Solution { get; set; }
        public string Precisions { get; set; }
        public string Suggestions { get; set; }
        public string Suivi { get; set; }
        public string Commentaire { get; set; }
    }
}
