using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Models
{
   public class ContreEnqueteModel
    {
        public long ContreEnqueteId { get; set; }
        public Nullable<long> BatimentId { get; set; }
        public string SdeId { get; set; }
        public string CodeDistrict { get; set; }
        public string NomSuperviseur { get; set; }
        public string PrenomSuperviseur { get; set; }
        public Nullable<byte> ModelTirage { get; set; }
        public Nullable<int> TypeContreEnquete { get; set; }
        public Nullable<int> Raison { get; set; }
        public Nullable<int> Statut { get; set; }
        public string DateDebut { get; set; }
        public string DateFin { get; set; }
        public string ContreEnqueteName { get; set; }
        public bool IsTerminate { get; set; }
        public bool IsValidate { get; set; }

    }
}
