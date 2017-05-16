using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.entites
{
    public class Sde
    {
        public string id { get; set; }
        public string text { get; set; }
        public int AgentId { get; set; }

        public int StatutSde { get; set; }
        public string state { get; set; }
        public int TotalBatiments { get; set; }
        public int TotalLogeCollectifs { get; set; }
        public int TotalLogeInds { get; set; }
        public int TotalMenages { get; set; }
        public int TotalIndividus { get; set; }
        public int TotalDeces { get; set; }
        public int TotalEmigres { get; set; }
        public string DeptNom { get; set; }
        public string CommuneNom { get; set; }
        public string SectionCommunale { get; set; }
        public string ResponsableName { get; set; }
        public string AgentName { get; set; }
    }
}
