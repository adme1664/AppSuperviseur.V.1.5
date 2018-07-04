using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Json
{
   public class LogementIsJson
    {
        public long logeId { get; set; }
        public long batimentId { get; set; }
        public string sdeId { get; set; }
        public short qlCategLogement { get; set; }
        public short qlin1NumeroOrdre { get; set; }
        public short qlc1TypeLogement { get; set; }
        public short qlc2bTotalGarcon { get; set; }
        public short qlc2bTotalFille { get; set; }
        public short qlcTotalIndividus { get; set; }
        public short qlin2StatutOccupation { get; set; }
        public short qlin3ExistenceLogement { get; set; }
        public short qlin4TypeLogement { get; set; }
        public short qlin5MateriauSol { get; set; }
        public short qlin6NombrePiece { get; set; }
        public short qlin7NbreChambreACoucher { get; set; }
        public short qlin8NbreIndividuDepense { get; set; }
        public short qlin9NbreTotalMenage { get; set; }
        public short statut { get; set; }
        public bool isValidated { get; set; }
        public string dateDebutCollecte { get; set; }
        public string dateFinCollecte { get; set; }
        public int dureeSaisie { get; set; }
        public bool isFieldAllFilled { get; set; }
        public bool isContreEnqueteMade { get; set; }
        public short nbrTentative { get; set; }
        public string codeAgentRecenceur { get; set; }
        public bool verified { get; set; }
        public List<MenageJson> menages { get; set; }
    }
}
