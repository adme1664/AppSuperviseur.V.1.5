using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Models
{
   public class MenageDetailsModel
    {
        private string name;
        private string id;
        private int type;
        private string sdeId;
        private bool _isContreEnqueteMade;
        private long batimentId;
        private long logementId;
        private long menageId;
        private int _statut;
        private bool _valide;

        public bool Valide
        {
            get { return _valide; }
            set { _valide = value; }
        }
        

        public int Statut
        {
            get { return _statut; }
            set { _statut = value; }
        }

        public long MenageId
        {
            get { return menageId; }
            set { menageId = value; }
        }

        public long BatimentId
        {
            get { return batimentId; }
            set { batimentId = value; }
        }
        

        public long LogementId
        {
            get { return logementId; }
            set { logementId = value; }
        }

        public bool IsContreEnqueteMade
        {
            get { return _isContreEnqueteMade; }
            set { _isContreEnqueteMade = value; }
        }


        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        

        public string SdeId
        {
            get { return sdeId; }
            set { sdeId = value; }
        }

        

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public int Type
        {
            get { return type; }
            set { type = value; }
        }

    }
}
