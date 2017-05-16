using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Ht.Ihsil.Rgph.App.Superviseur.MVVM;
using Ht.Ihsil.Rgph.App.Superviseur.services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ht.Ihsil.Rgph.App.Superviseur.viewModels
{
   public class UtilisateurViewModel : ViewModelBase,INotifyPropertyChanged
    {
        //private ICommand _saveUserCommand;
        public RelayCommand SaveUserCommand{get; private set;}
        private UtilisateurModel model;
        public UtilisateurService service;
        private string codeUtilisateur;
        private string motDePasse;
        private int statut;
        private string profile;
        private string nom;
        private string prenom;

        public ObservableCollection<SdeModel> listOfSdes { get; private set; }
        public UtilisateurViewModel()
        {
            service = new UtilisateurService();
            SaveUserCommand = new RelayCommand(param=>ajouterUtilisateur());
        }

        public UtilisateurModel Model
        {
            get { return model; }
            set 
            {
                if (model != null)
                {
                    NotifyPropertyChanged(ref model, value);
                }
            }
        }

     
        public string MotDePasse
        {
            get { return motDePasse; }
            set { NotifyPropertyChanged(ref this.motDePasse,  value); }
        }
        public int Statut
        {
            get { return statut; }
            set { NotifyPropertyChanged(ref this.statut ,value); }
        }
        
        public string Profile
        {
            get { return profile; }
            set { NotifyPropertyChanged(ref this.profile ,value); }
        }

        public string CodeUtilisateur
        {
            get { return codeUtilisateur; }
            set { NotifyPropertyChanged(ref codeUtilisateur, value); }
        }
        public string Nom
        {
            get { return nom; }
            set { NotifyPropertyChanged(ref nom , value); }
        }
       
        public string Prenom
        {
            get { return prenom; }
            set { NotifyPropertyChanged(ref prenom,value); }
        }

        private void ajouterUtilisateur()
        {
            
                UtilisateurModel user = new UtilisateurModel();
                user.CodeUtilisateur = CodeUtilisateur;
                user.CodeUtilisateur = "001";
                user.Nom = Nom;
                user.Prenom = Prenom;
                user.ProfileId = Convert.ToInt32(Profile);
                user.Statut = Convert.ToByte(Statut);
                service.insertUser(user);
       }
        private void synchroniser()
        {

        }

    }
}
