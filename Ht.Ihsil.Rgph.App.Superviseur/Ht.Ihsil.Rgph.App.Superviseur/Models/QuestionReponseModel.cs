using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Models
{
    public class QuestionReponseModel
    {
        public string CodeQuestion { get; set; }
        public string CodeUniqueReponse { get; set; }
        public Nullable<bool> EstEnfant { get; set; }
        public Nullable<bool> AvoirEnfant { get; set; }
        public string CodeParent { get; set; }
        public string QPrecedent { get; set; }
        public string QSuivant { get; set; }
    }
}
