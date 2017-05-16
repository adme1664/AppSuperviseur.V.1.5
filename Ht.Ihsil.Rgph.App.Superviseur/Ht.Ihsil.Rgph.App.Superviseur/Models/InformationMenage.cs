using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Models
{
    public class InformationMenage
    {
        public string Enfomasyon { get; set; }
        public string Gason { get; set; }
        public string Fi { get; set; }
        public string Total { get; set; }

        public InformationMenage(string enfomasyon,string total, string fi, string gason)
        {
            this.Enfomasyon = enfomasyon;
            this.Total = total;
            this.Fi = fi;
            this.Gason = gason;
        }
        public InformationMenage()
        {

        }
        public InformationMenage(string enfomasyon, string total)
        {
            this.Enfomasyon = enfomasyon;
            this.Total = total;
        }
    }
}
