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
        public int QlCategLogement { get; set; }
        public int Qlin1NumeroOrdre { get; set; }
        public int Qlc1TypeLogement { get; set; }
        public int Qllc2bTotalGarcon { get; set; }
        public int Qlc2bTotalFille { get; set; }
        public int QlcTotalIndividus { get; set; }
        public int Qlin2StatutOccupation { get; set; }
        public int Qlin3ExistenceLogement { get; set; }
        public int Qlin4TypeLogement { get; set; }
        public int Qlin5MateriauSol { get; set; }
        public int Qlin6NombrePiece { get; set; }
        public int Qlin7NbreChambreACoucher { get; set; }
        public int Qlin8NbreIndividuDepense { get; set; }
        public int Qlin9NbreTotalMenage { get; set; }
        public int Statut { get; set; }
        public bool IsValidated { get; set; }
        public string DateDebutCollecte { get; set; }
        public string DateFinCollecte { get; set; }
        public int DureeSaisie { get; set; }
        public bool IsFieldAllFilled { get; set; }
        public bool IsContreEnqueteMade { get; set; }
        public int NbrTentative { get; set; }
        public string CodeAgentRecenceur { get; set; }


       //
        public string Header { get; set; }
        public string RecBatiment { get; set; }
        public List<MenageModel> Menages { get; set; }
        public int TypeContreEnquete { get; set; }
   }
}
