using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.viewModels
{
    public class SdeSumModel
    {
        public string SdeId { get; set; }
        public int CountBatiment { get; set; }
        public int CountMenage { get; set; }
        public int CountIndividu { get; set; }
        public int CountEmigre { get; set; }
        public int CountDeces { get; set; }
        public int CountBatimentC { get; set; }
        public int Pourcentage { get; set; }
    }
}
