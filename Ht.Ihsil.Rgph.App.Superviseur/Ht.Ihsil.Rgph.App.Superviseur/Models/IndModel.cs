using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Models
{
   public class IndModel
    {
        private string nom;

        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }
        private string prenom;

        public string Prenom
        {
            get { return prenom; }
            set { prenom = value; }
        }
        private string sexe;

        public string Sexe
        {
            get { return sexe; }
            set { sexe = value; }
        }
        private string age;

        public string Age
        {
            get { return age; }
            set { age = value; }
        }

        public IndModel(string nom, string prenom, string sexe, string age)
        {
            this.nom = nom;
            this.sexe = sexe;
            this.prenom = prenom;
            this.age = age;
        }

    }
}
