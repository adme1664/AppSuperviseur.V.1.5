using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Json
{
   public class EmigreJson
    {
        public long emigreId { get; set; }
        public long menageId { get; set; }
        public long logeId { get; set; }
        public long batimentId { get; set; }
        public string sdeId { get; set; }
        public short qn1numeroOrdre { get; set; }
        public string qn2aNomComplet { get; set; }
        public short qn2bSexe { get; set; }
        public string qn2cAgeAuMomentDepart { get; set; }
        public short qn2dVivantToujours { get; set; }
        public short qn2eDernierPaysResidence { get; set; }
        public short qn1Emigration { get; set; }
        public short qn1NbreEmigreF { get; set; }
        public short qn1NbreEmigreG { get; set; }
        public short statut { get; set; }
        public bool isFieldAllFilled { get; set; }
        public string dateDebutCollecte { get; set; }
        public string dateFinCollecte { get; set; }
        public int dureeSaisie { get; set; }
        public string codeAgentRecenceur { get; set; }
        public bool verified { get; set; }
    }
}
