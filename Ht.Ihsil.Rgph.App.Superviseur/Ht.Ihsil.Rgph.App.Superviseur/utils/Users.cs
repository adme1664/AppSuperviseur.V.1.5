using Ht.Ihsil.Rgph.App.Superviseur.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.utils
{
    class Users
    {
        public static Users users;
        private string nom;
        private string profile;
        private string codeUtilisateur;
        private string databasePath;
        private string supDatabasePath;
        private string appExecutionPath;
        private UtilisateurModel _utilisateur;
        

        public UtilisateurModel Utilisateur
        {
            get { return _utilisateur; }
            set { _utilisateur = value; }
        }
        private int _iD;

        public string AppExecutionPath
        {
            get { return appExecutionPath; }
            set { appExecutionPath = value; }
        }
        
        public int ID
        {
            get { return _iD; }
            set { _iD = value; }
        }

        public string SupDatabasePath
        {
            get { return supDatabasePath; }
            set { supDatabasePath = value; }
        }
        private string appDirectoryPath;

        public string AppDirectoryPath
        {
            get { return appDirectoryPath; }
            set { appDirectoryPath = value; }
        }

        public string DatabasePath
        {
            get { return databasePath; }
            set { databasePath = value; }
        }
       
        public string CodeUtilisateur
        {
            get { return codeUtilisateur; }
            set { codeUtilisateur = value; }
        }
        private string prenom;


        public string Profile
        {
            get { return profile; }
            set { profile = value; }
        }

        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }
        
        public string Prenom
        {
            get { return prenom; }
            set { prenom = value; }
        }

    }
}
