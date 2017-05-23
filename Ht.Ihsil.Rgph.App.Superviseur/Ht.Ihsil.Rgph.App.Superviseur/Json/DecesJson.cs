using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Json
{
    public class DecesJson
    {
        public long decesId { get; set; }
        public long menageId { get; set; }
        public long logeId { get; set; }
        public long batimentId { get; set; }
        public string sdeId { get; set; }
        public short qd2NoOrdre { get; set; }
        public short qd2aSexe { get; set; }
        public string qd2bAgeDecede { get; set; }
        public short qd2c1CirconstanceDeces { get; set; }
        public short qd2c2CauseDeces { get; set; }
        public short statut { get; set; }
        public bool isFieldAllFilled { get; set; }
        public string dateDebutCollecte { get; set; }
        public string dateFinCollecte { get; set; }
        public int dureeSaisie { get; set; }
        public bool isContreEnqueteMade { get; set; }
        public string codeAgentRecenceur { get; set; }
    }
}
