using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Models
{
    public class MenageCEModel
    {
        public long Id { get; set; }
        public long MenageId { get; set; }
        public long LogeId { get; set; }
        public long BatimentId { get; set; }
        public string SdeId { get; set; }
        public Nullable<long> Qm1NoOrdre { get; set; }
        public Nullable<long> Qm2ModeJouissance { get; set; }
        public Nullable<long> Qm5SrcEnergieCuisson1 { get; set; }
        public Nullable<long> Qm5SrcEnergieCuisson2 { get; set; }
        public Nullable<long> Qm8EndroitBesoinPhysiologique { get; set; }
        public Nullable<long> Qm11TotalIndividuVivant { get; set; }
        public Nullable<long> Statut { get; set; }
        public Nullable<bool> IsValidated { get; set; }
        public string DateDebutCollecte { get; set; }
        public string DateFinCollecte { get; set; }
        public Nullable<long> DureeSaisie { get; set; }
        public Nullable<bool> IsContreEnqueteMade { get; set; }
        public Nullable<bool> IsValidate { get; set; }
        public bool IsChecked { get; set; }

        public string MenageName
        {
            get
            {
                return "Menaj-" + Qm1NoOrdre;
            }
        }

        public MenageCEModel(long menageId)
        {
            MenageId = menageId;
           
        }
        public MenageCEModel()
        {

        }
    }
}
