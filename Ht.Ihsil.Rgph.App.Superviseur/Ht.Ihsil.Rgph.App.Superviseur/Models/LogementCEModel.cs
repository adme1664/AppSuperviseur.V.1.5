using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Models
{
   public class LogementCEModel
    {
        public long Id { get; set; }
        public long LogeId { get; set; }
        public long BatimentId { get; set; }
        public string SdeId { get; set; }
        public Nullable<long> QlCategLogement { get; set; }
        public Nullable<long> QlcTypeLogement { get; set; }
        public Nullable<long> Qllc2bTotalGarcon { get; set; }
        public Nullable<long> Qlc2bTotalFille { get; set; }
        public Nullable<long> Qlin1NumeroOrdre { get; set; }
        public Nullable<long> Qlin2StatutOccupation { get; set; }
        public Nullable<long> Qlin4TypeLogement { get; set; }
        public Nullable<long> Qlin5MateriauSol { get; set; }
        public Nullable<long> Qlin6NombrePiece { get; set; }
        public Nullable<long> Qlin7NbreChambreACoucher { get; set; }
        public Nullable<long> Qlin8NbreIndividuDepense { get; set; }
        public Nullable<long> Qlin9NbreTotalMenage { get; set; }
        public Nullable<long> Statut { get; set; }
        public Nullable<long> IsValidated { get; set; }
        public string DateDebutCollecte { get; set; }
        public string DateFinCollecte { get; set; }
        public Nullable<long> DureeSaisie { get; set; }
        public Nullable<long> IsContreEnqueteMade { get; set; }
        public Nullable<bool> IsValidate { get; set; }
        public bool IsChecked { get; set; }
    }
}
