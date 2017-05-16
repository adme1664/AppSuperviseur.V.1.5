using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Models
{
    public class DecesModel
    {

        //
        public long DecesId { get; set; }
        public long MenageId { get; set; }
        public long LogeId { get; set; }
        public long BatimentId { get; set; }
        public string SdeId { get; set; }
        public short Qd2NoOrdre { get; set; }
        public short Qd2aSexe { get; set; }
        public string Qd2bAgeDecede { get; set; }
        public short Qd2c1CirconstanceDeces { get; set; }
        public short Qd2c2CauseDeces { get; set; }
        public short Statut { get; set; }
        public bool IsFieldAllFilled { get; set; }
        public string DateDebutCollecte { get; set; }
        public string DateFinCollecte { get; set; }
        public int DureeSaisie { get; set; }
        public bool IsContreEnqueteMade { get; set; }
        public string CodeAgentRecenceur { get; set; }

        public string Header { get; set; }
   
        private string decesName;

        public string DecesName
        {
            get { return "Dese "+DecesId; }
           
        }

        public DecesModel(int decesId, byte noOdre)
        {
            this.DecesId = decesId;
            this.Qd2NoOrdre = noOdre;
        }
        public DecesModel()
        {

        }
    }
}
