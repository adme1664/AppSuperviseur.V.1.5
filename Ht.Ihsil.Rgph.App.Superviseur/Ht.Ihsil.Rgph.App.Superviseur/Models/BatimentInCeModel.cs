using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Models
{
    public class BatimentInCeModel
    {
        private string batimentId;

        public string BatimentId
        {
            get { return batimentId; }
            set { batimentId = value; }
        }
        private string sdeId;

        public string SdeId
        {
            get { return sdeId; }
            set { sdeId = value; }
        }

        public string DepId { get; set; }
        public string ComId { get; set; }
        public string VqseId { get; set; }
        public string DistrictId { get; set; }
        public string Localite { get; set; }
        public string Habitation { get; set; }


        private string rec;

        public string Rec
        {
            get { return rec; }
            set { rec = value; }
        }
        private string rgph;

        public string Rgph
        {
            get { return rgph; }
            set { rgph = value; }
        }
        private string adresse;

        public string Adresse
        {
            get { return adresse; }
            set { adresse = value; }
        }
        private string nomChefMenage;

        public string NomChefMenage
        {
            get { return nomChefMenage; }
            set { nomChefMenage = value; }
        }
    }
}
