using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Models
{
    public class RapportComparaisonModel
    {
        private string _type;

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }
        private string _domaine;

        public string Domaine
        {
            get { return _domaine; }
            set { _domaine = value; }
        }
        private string _question;

        public string Question
        {
            get { return _question; }
            set { _question = value; }
        }
        private string _agent;

        public string Agent
        {
            get { return _agent; }
            set { _agent = value; }
        }
        private string _superviseur;

        public string Superviseur
        {
            get { return _superviseur; }
            set { _superviseur = value; }
        }
        private string _comparaison;

        public string Comparaison
        {
            get { return _comparaison; }
            set { _comparaison = value; }
        }
        private string _iD;

        public string ID
        {
            get { return _iD; }
            set { _iD = value; }
        }
        private string _parentID;

        public string ParentID
        {
            get { return _parentID; }
            set { _parentID = value; }
        }
        
    }
}
