using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Models
{
   public class SdeModel
    {
        public string SdeId { get; set; }
        public string CodeDistrict { get; set; }
        public int AgentId { get; set; }
        public string NoOrdre { get; set; }
        public Nullable<int> StatutContreEnquete { get; set; }
        public Nullable<int> StatutCollecte { get; set; }
        public Nullable<int> RaisonCouverture { get; set; }
        public Nullable<int> TotalBatCartographie { get; set; }
        public Nullable<int> TotalBatRecense { get; set; }
        public Nullable<int> TotalLogeCRecense { get; set; }
        public Nullable<int> TotalLogeIRecense { get; set; }
        public Nullable<int> TotalLogeIOccupeRecense { get; set; }
        public Nullable<int> TotalLogeIVideRecense { get; set; }
        public Nullable<int> TotalLogeIUsageTemporelRecense { get; set; }
        public Nullable<int> TotalMenageRecense { get; set; }
        public Nullable<int> TotalBatRecenseParJour { get; set; }
        public Nullable<int> TotalLogeRecenseParJour { get; set; }
        public Nullable<int> TotalMenageRecenseParJour { get; set; }
        public Nullable<int> TotalBatRecenseV { get; set; }
        public Nullable<int> TotalLogeCRecenseV { get; set; }
        public Nullable<int> TotalLogeIRecenseV { get; set; }
        public Nullable<int> TotalLogeIOccupeRecenseV { get; set; }
        public Nullable<int> TotalLogeIVideRecenseV { get; set; }
        public Nullable<int> TotalLogeIUsageTemporelRecenseV { get; set; }
        public Nullable<int> TotalMenageRecenseV { get; set; }
        public Nullable<int> TotalBatRecenseParJourV { get; set; }
        public Nullable<int> TotalLogeRecenseParJourV { get; set; }
        public Nullable<int> TotalMenageRecenseParJourV { get; set; }
        public Nullable<int> TotalBatRecenseNV { get; set; }
        public Nullable<int> TotalLogeCRecenseNV { get; set; }
        public Nullable<int> TotalLogeIRecenseNV { get; set; }
        public Nullable<int> TotalLogeIOccupeRecenseNV { get; set; }
        public Nullable<int> TotalLogeIVideRecenseNV { get; set; }
        public Nullable<int> TotalLogeIUsageTemporelRecenseNV { get; set; }
        public Nullable<int> TotalMenageRecenseNV { get; set; }
        public Nullable<int> TotalBatRecenseParJourNV { get; set; }
        public Nullable<int> TotalLogeRecenseParJourNV { get; set; }
        public Nullable<int> TotalMenageRecenseParJourNV { get; set; }
        public Nullable<int> TotalIndRecense { get; set; }
        public Nullable<int> TotalIndFRecense { get; set; }
        public Nullable<int> TotalIndGRecense { get; set; }
        public Nullable<int> TotalEmigreRecense { get; set; }
        public Nullable<int> TotalEmigreFRecense { get; set; }
        public Nullable<int> TotalEmigreGRecense { get; set; }
        public Nullable<int> TotalDecesRecense { get; set; }
        public Nullable<int> TotalDecesFRecense { get; set; }
        public Nullable<int> TotalDecesGRecense { get; set; }
        public Nullable<int> TotalIndRecenseParJour { get; set; }
        public Nullable<double> IndiceMasculinite { get; set; }
        public Nullable<int> TotalEnfantDeMoinsDe5Ans { get; set; }
        public Nullable<int> TotalIndividu18AnsEtPlus { get; set; }
        public Nullable<int> TotalIndividu10AnsEtPlus { get; set; }
        public Nullable<int> TotalIndividu65AnsEtPlus { get; set; }
        public Nullable<int> TotalMenageUnipersonnel { get; set; }
        public Nullable<int> TotalMenageDe6IndsEtPlus { get; set; }
        public SdeModel()
        {

        }

        public int TypeContreEnquete { get; set; }

        readonly List<BatimentModel> _batiments=new List<BatimentModel>();

        public SdeModel(string sdeName)
        {
            this.SdeId = sdeName;
        }
        public List<BatimentModel> Batiments
        {
            get
            {
                return _batiments;
            }
            
        }
        public bool IsWaitVisible { get; set; }

    }
}
