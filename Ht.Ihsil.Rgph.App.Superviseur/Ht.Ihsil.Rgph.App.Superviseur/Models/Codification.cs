using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Models
{
    public class Codification
    {
        public int A7Codifie { get; set; }
        public int A7Autre { get; set; }
        public int A7NeSaitPas { get; set; }

        public int A5Codifie { get; set; }
        public int A5Autre { get; set; }
        public int A5NeSaitPas { get; set; }

        public int P10_1 { get; set; }
        public int P10_2 { get; set; }
        public int P10_3 { get; set; }
        public int P10_4 { get; set; }

        public int P12_1 { get; set; }
        public int P12_2 { get; set; }
        public int P12_3 { get; set; }
        public int P12_4 { get; set; }
        public int SommeP10PartiellementCodifie { get;set;}
        public int SommeP12PartiellementCodifie { get; set; }
         
    }
}
