using Ht.Ihsil.Rgph.App.Superviseur.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.viewModels.ContreEnquete
{
   public class TreeCEViewModel
    {
        readonly ReadOnlyCollection<SdeCEViewModel> _sdes;

        public TreeCEViewModel(SdeModel[] models)
        {
            _sdes = new ReadOnlyCollection<SdeCEViewModel>(
                (from sde in models
                 select new SdeCEViewModel(sde))
                .ToList());
        }

        public ReadOnlyCollection<SdeCEViewModel> Sdes
        {
            get { return _sdes; }
        }
    }
}
