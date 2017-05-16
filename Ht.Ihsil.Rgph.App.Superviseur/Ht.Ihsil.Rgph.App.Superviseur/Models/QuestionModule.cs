using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Models
{
    public class QuestionModule
    {
        public string ordre { get; set; }
        public string codeModule { get; set; }
        public string codeQuestion { get; set; }
        public Nullable<long> estDebut { get; set; }
    }
}
