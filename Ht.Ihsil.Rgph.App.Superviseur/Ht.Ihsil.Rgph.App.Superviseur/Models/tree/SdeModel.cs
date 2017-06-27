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
        public Nullable<int> TotalMenageRecense { get; set; }
        public Nullable<int> TotalIndRecense { get; set; }
        public Nullable<int> TotalIndFRecense { get; set; }
        public Nullable<int> TotalIndGRecense { get; set; }
        public Nullable<int> TotalEmigreRecense { get; set; }
        public Nullable<int> TotalEmigreFRecense { get; set; }
        public Nullable<int> TotalEmigreGRecense { get; set; }
        public Nullable<int> TotalDecesRecense { get; set; }
        public Nullable<int> TotalDecesFRecense { get; set; }
        public Nullable<int> TotalDecesGRecense { get; set; }
        public String SdeName { get; set; }
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
