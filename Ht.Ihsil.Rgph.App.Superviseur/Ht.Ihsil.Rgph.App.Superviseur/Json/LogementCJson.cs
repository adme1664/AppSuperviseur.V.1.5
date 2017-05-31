using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Json
{
   public class LogementCJson
    {
        public long logeId { get; set; }
        public long batimentId { get; set; }
        public string sdeId { get; set; }
        public short qlin1NumeroOrdre { get; set; }
        public short qlc1TypeLogement { get; set; }
        public short qlc2bTotalGarcon { get; set; }
        public short qlc2bTotalFille { get; set; }
        public short qlcTotalIndividus { get; set; }
        public short statut { get; set; }
        public string dateEnvoi { get; set; }
        public bool isValidated { get; set; }
        public bool isSynchroToAppSup { get; set; }
        public bool isSynchroToCentrale { get; set; }
        public string dateDebutCollecte { get; set; }
        public string dateFinCollecte { get; set; }
        public int dureeSaisie { get; set; }
        public bool isFieldAllFilled { get; set; }
        public bool isContreEnqueteMade { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string codeAgentRecenceur { get; set; }
        public List<IndividuJson> individus { get; set; }
    }
}
