using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Models
{
   public class LogementModel
    {
       //
        public long LogeId { get; set; }
        public long BatimentId { get; set; }
        public string SdeId { get; set; }
        public short QlCategLogement { get; set; }
        public short Qlin1NumeroOrdre { get; set; }
        public short Qlc1TypeLogement { get; set; }
        public short Qlc2bTotalGarcon { get; set; }
        public short Qlc2bTotalFille { get; set; }
        public short QlcTotalIndividus { get; set; }
        public short Qlin2StatutOccupation { get; set; }
        public short Qlin3ExistenceLogement { get; set; }
        public short Qlin4TypeLogement { get; set; }
        public short Qlin5MateriauSol { get; set; }
        public short Qlin6NombrePiece { get; set; }
        public short Qlin7NbreChambreACoucher { get; set; }
        public short Qlin8NbreIndividuDepense { get; set; }
        public short Qlin9NbreTotalMenage { get; set; }
        public short Statut { get; set; }
        public bool IsValidated { get; set; }
        public string DateDebutCollecte { get; set; }
        public string DateFinCollecte { get; set; }
        public int DureeSaisie { get; set; }
        public bool IsFieldAllFilled { get; set; }
        public bool IsContreEnqueteMade { get; set; }
        public short NbrTentative { get; set; }
        public string CodeAgentRecenceur { get; set; }



       //
        public string Header { get; set; }
        public string RecBatiment { get; set; }
        public List<MenageModel> Menages { get; set; }
        public int TypeContreEnquete { get; set; }
   }
}
