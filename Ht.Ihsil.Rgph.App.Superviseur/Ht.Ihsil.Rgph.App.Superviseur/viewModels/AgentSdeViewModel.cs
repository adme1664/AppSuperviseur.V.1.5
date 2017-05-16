using Ht.Ihsil.Rgph.App.Superviseur.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.viewModels
{
   public  class AgentSdeViewModel : ViewModelBase
    {
       private int agentSdeId;
       private string codeUtilisateur;
       private string motDePasse;
       private string nom;
       private string prenom;
       private string sexe;
       private string nif;
       private string cin;
       private string telephone;
       private string email;
       private string numSde;
        public int AgentSdeId 
        {
            get { return AgentSdeId; }
            set { NotifyPropertyChanged(ref agentSdeId, value); }
        }
        public string CodeUtilisateur {
            get { return codeUtilisateur; }
            set { NotifyPropertyChanged(ref codeUtilisateur, value); }
        }
        public string MotDePasse { get { return motDePasse; } set { NotifyPropertyChanged(ref motDePasse, value); } }
        public string Nom { get { return nom; } set { NotifyPropertyChanged(ref nom, value); } }
        public string Prenom { get { return prenom; } set { NotifyPropertyChanged(ref prenom, value); } }
        public string Sexe { get { return sexe; } set { NotifyPropertyChanged(ref sexe, value); } }
        public string Nif { get { return nif; } set { NotifyPropertyChanged(ref nif, value); } }
        public string Cin { get { return cin; } set { NotifyPropertyChanged(ref cin, value); } }
        public string Telephone { get { return telephone; } set { NotifyPropertyChanged(ref telephone, value); } }
        public string Email { get { return email; } set { NotifyPropertyChanged(ref email, value); } }
        public string NumSde { get { return numSde; } set { NotifyPropertyChanged(ref numSde, value); } }
    }
}
