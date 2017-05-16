using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Models
{
    public class RapportArModel
    {
        public long RapportId { get; set; }
        public long BatimentId { get; set; }
        public long LogeId { get; set; }
        public long MenageId { get; set; }
        public long EmigreId { get; set; }
        public long DecesId { get; set; }
        public long IndividuId { get; set; }
        public string RapportModuleName { get; set; }
        public string CodeQuestionStop { get; set; }
        public int VisiteNumber { get; set; }
        public int RaisonActionId { get; set; }
        public string AutreRaisonAction { get; set; }
        public bool IsFieldAllFilled { get; set; }
        public string DateDebutCollecte { get; set; }
        public string DateFinCollecte { get; set; }
        public int DureeSaisie { get; set; }
        public bool IsContreEnqueteMade { get; set; }
        public string CodeAgentRecenceur { get; set; }
    }
}
