using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Models
{
    public class EmigreModel
    {
        //
        public long EmigreId { get; set; }
        public long MenageId { get; set; }
        public long LogeId { get; set; }
        public long BatimentId { get; set; }
        public string SdeId { get; set; }
        public short Qn1numeroOrdre { get; set; }
        public string Qn2aNomComplet { get; set; }
        public short Qn2bSexe { get; set; }
        public string Qn2cAgeAuMomentDepart { get; set; }
        public short Qn2dVivantToujours { get; set; }
        public short Qn2eDernierPaysResidence { get; set; }
        public short Statut { get; set; }
        public bool IsFieldAllFilled { get; set; }
        public string DateDebutCollecte { get; set; }
        public string DateFinCollecte { get; set; }
        public int DureeSaisie { get; set; }
        public long IsVerified { get; set; }
        public string CodeAgentRecenceur { get; set; }
        public string Header { get; set; }
     
        public string EmigreName
        {
            get { return "Emigre " + EmigreId; }

        }

        public EmigreModel(int emigreId, int noOrdre)
        {
            this.EmigreId = emigreId;
            Qn1numeroOrdre = Convert.ToByte(noOrdre);
        }
        public EmigreModel()
        {

        }
    }
}
