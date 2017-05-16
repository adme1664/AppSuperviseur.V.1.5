using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Models
{
   public  class QuestionsModel
    {
        public string CodeQuestion { get; set; }
        public string Libelle { get; set; }
        public string NomObjet { get; set; }
        public string DetailsQuestion { get; set; }
        public string CodeCategorie { get; set; }
        public string NomChamps { get; set; }
        public Nullable<int> TypeQuestion { get; set; }
        public Nullable<int> ContrainteQuestion { get; set; }
        public Nullable<int> ValeurMaxPrChiffre { get; set; }
        public Nullable<int> NbreCaratereMaximal { get; set; }
        public Nullable<bool> EstSautReponse { get; set; }
        public string QPrecedent { get; set; }
        public string QSuivant { get; set; }
    }
}
