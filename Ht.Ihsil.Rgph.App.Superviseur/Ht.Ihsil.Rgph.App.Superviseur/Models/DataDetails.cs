using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Models
{
  public  class DataDetails
    {
        private string kesyon;
        private string repons;
        private string kategori;

        public string Kategori
        {
            get { return kategori; }
            set { kategori = value; }
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

        public DataDetails(string q, string r)
        {
            this.kesyon = q;
            this.repons = r;
        }
        public DataDetails(string q, string r,string k)
        {
            this.kesyon = q;
            this.repons = r;
            this.kategori = k;
        }
    }
}
