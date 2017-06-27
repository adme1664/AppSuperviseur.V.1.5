using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Models
{
   public class MaterielModel
    {
        public long MaterielId { get; set; }
        public string Imei { get; set; }
        public string Serial { get; set; }
        public string Model { get; set; }
        public string Version { get; set; }
        public string Agent{ get; set; }
        public string DateAssignation { get; set; }
        public string Configure { get; set; }
        public string Synchronisation { get; set; }
    }
}
