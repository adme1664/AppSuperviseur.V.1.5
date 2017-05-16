using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Ht.Ihsil.Rgph.App.Superviseur.MVVM;
using Ht.Ihsil.Rgph.App.Superviseur.services;
using Ht.Ihsil.Rgph.App.Superviseur.viewModels.ContreEnquete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.viewModels.ContreEnquete
{
    public class SdeCEViewModel:TreeViewItemViewModel
    {
        private SdeModel _sde;

        public SdeModel Sde
        {
            get { return _sde; }
            set { _sde = value; }
        }
        private IContreEnqueteService _service_ce;

        public SdeCEViewModel(SdeModel sde)
            :base(null, true)
        {
            _sde = sde;
            _service_ce = new ContreEnqueteService();
        }
        public string SdeName
        {
            get { return "SDE-" + _sde.SdeId; }
        }
        protected override void LoadChildren()
        {
            this.Children.Clear();
            foreach(ContreEnqueteModel contreEnquete in _service_ce.searchContreEnquete(_sde.TypeContreEnquete,_sde.SdeId))
            {
                contreEnquete.SdeId = _sde.SdeId;
                BatimentCEModel bat= _service_ce.getBatiment(contreEnquete.BatimentId.GetValueOrDefault(), contreEnquete.SdeId);
                if (bat.IsContreEnqueteMade.GetValueOrDefault() == true)
                    contreEnquete.IsTerminate = true;
                if (bat.IsValidated.GetValueOrDefault() == true)
                    contreEnquete.IsValidate = true;
                contreEnquete.TypeContreEnquete = Convert.ToByte(_sde.TypeContreEnquete);
                base.Children.Add(new ContreEnqueteViewModel(contreEnquete, this));
            }
        }
    }
}
