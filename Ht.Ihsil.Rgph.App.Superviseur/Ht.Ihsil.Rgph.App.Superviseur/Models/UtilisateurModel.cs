using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Models
{
   public class UtilisateurModel
    {
        public int UtilisateurId { get; set; }
        public string CodeUtilisateur { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string MotDePasse { get; set; }
        public Nullable<byte> Statut { get; set; }
        public int ProfileId { get; set; }
    }
}
