using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Models
{
    public class VerificationFlag
    {
        private string _theme;

        public string Theme
        {
            get { return _theme; }
            set { _theme = value; }
        }
        private string _sous_Theme;

        public string Sous_Theme
        {
            get { return _sous_Theme; }
            set { _sous_Theme = value; }
        }
        private string _denominateur;

        public string Denominateur
        {
            get { return _denominateur; }
            set { _denominateur = value; }
        }
        private int _compteur;

        public int Compteur
        {
            get { return _compteur; }
            set { _compteur = value; }
        }
        private string flag;

        public string Flag
        {
            get { return flag; }
            set { flag = value; }
        }
        private string _variable1;

        public string Variable1
        {
            get { return _variable1; }
            set { _variable1 = value; }
        }
        private string _variable2;

        public string Variable2
        {
            get { return _variable2; }
            set { _variable2 = value; }
        }
        private int probleme;

        public int Probleme
        {
            get { return probleme; }
            set { probleme = value; }
        }
        private string _indice;

        public string Indice
        {
            get { return _indice; }
            set { _indice = value; }
        }
        private int _iD;

        public int ID
        {
            get { return _iD; }
            set { _iD = value; }
        }
        private int _parentID;

        public int ParentID
        {
            get { return _parentID; }
            set { _parentID = value; }
        }
     
    }
}
