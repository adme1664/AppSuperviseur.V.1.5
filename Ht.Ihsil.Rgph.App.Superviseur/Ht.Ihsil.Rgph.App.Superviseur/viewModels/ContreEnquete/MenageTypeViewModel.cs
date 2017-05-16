using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Ht.Ihsil.Rgph.App.Superviseur.MVVM;
using Ht.Ihsil.Rgph.App.Superviseur.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.viewModels.ContreEnquete
{
    public class MenageTypeViewModel:TreeViewItemViewModel
    {
        private MenageTypeModel menage;
        private MenageCEViewModel _parentView;
        IContreEnqueteService service_ce;
        public MenageTypeModel Menage
        {
            get { return menage; }
            set { menage = value; }
        }
        
        public MenageTypeViewModel(MenageTypeModel model,MenageCEViewModel parentRegion)
            :base(parentRegion,true)
        {
            menage = model;
            service_ce = new ContreEnqueteService();
            _parentView = parentRegion;
        }

        public string NodeName
        {
            get { return menage.Name; }
        }
        protected override void LoadChildren()
        {
            foreach(MenageDetailsModel model in service_ce.searchAllInMenage(_parentView.Menage,menage.Name))
            {
                base.Children.Add(new MenageDetailsViewModel(model,this));
            }
        }
    }
}
