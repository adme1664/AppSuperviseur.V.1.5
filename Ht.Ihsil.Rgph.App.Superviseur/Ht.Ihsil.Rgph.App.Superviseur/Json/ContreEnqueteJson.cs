using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Json
{
    public class  ContreEnqueteJson
    {
        public long contreEnqueteId { get; set; }
        public long batimentId { get; set; }
        public long logeCId { get; set; }
        public long logeIId { get; set; }
        public long menageId { get; set; }
        public string sdeId { get; set; }
        public string deptId { get; set; }
        public string comId { get; set; }
        public string districtId { get; set; }
        public string nomsSuperviseur { get; set; }
        public string codeSuperviseur { get; set; }
        public short typeContreEnqueteId { get; set; }
        public short raisonId { get; set; }
        public int statut { get; set; }
        public string dateDebut { get; set; }
        public string dateFin { get; set; }
        public bool termine { get; set; }
        public bool validate { get; set; }
        public int dureeSaisieCe { get; set; }
        public BatimentJson batimentJson { get; set; }
    }
}
