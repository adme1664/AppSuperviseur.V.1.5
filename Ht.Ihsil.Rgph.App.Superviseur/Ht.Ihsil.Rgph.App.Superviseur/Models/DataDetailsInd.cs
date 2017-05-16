using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Models
{
    public class DataDetailsInd
    {
        private string kesyon;
        private string repons;
        private string tipAktivite;
        private string koz;


        public DataDetailsInd(string kesyon, string repons, string tipAktivite, string koz)
        {
            this.kesyon = kesyon;
            this.repons = repons;
            this.tipAktivite = tipAktivite;
            this.koz = koz;
        }
        public string Kesyon
        {
            get { return kesyon; }
            set { kesyon = value; }
        }
        

        public string Repons
        {
            get { return repons; }
            set { repons = value; }
        }
        

        public string TipAktivite
        {
            get { return tipAktivite; }
            set { tipAktivite = value; }
        }
        

        public string Koz
        {
            get { return koz; }
            set { koz = value; }
        }
    }
}
