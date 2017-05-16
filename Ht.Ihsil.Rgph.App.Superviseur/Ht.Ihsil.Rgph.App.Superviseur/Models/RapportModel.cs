using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Models
{
    public class RapportModel
    {
        private string _indicateur;
        private string _iD;
        private string _type;

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

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

        public string Indicateur
        {
            get { return _indicateur; }
            set { _indicateur = value; }
        }
        private string _modalite;

        public string Modalite
        {
            get { return _modalite; }
            set { _modalite = value; }
        }
        private string _total;

        public string Total
        {
            get { return _total; }
            set { _total = value; }
        }
        private string _pourcentage;

        public string Pourcentage
        {
            get { return _pourcentage; }
            set { _pourcentage = value; }
        }
    }
}
