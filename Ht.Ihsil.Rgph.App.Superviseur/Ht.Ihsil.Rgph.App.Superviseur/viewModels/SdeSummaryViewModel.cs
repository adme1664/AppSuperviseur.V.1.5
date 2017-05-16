using Ht.Ihsi.Rgph.DataAccess.Entities;
using Ht.Ihsil.Rgph.App.Superviseur.MVVM;
using Ht.Ihsil.Rgph.App.Superviseur.services;
using Ht.Ihsil.Rgph.App.Superviseur.utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.viewModels
{
    public class SdeSummaryViewModel : ViewModelBase
    {

        public ObservableCollection<SdeSumModel> _listofSde { get; private set; }
        private MdfService service;
        public SdeSummaryViewModel()
        {
            service = new MdfService();
            List<SdeSumModel> _sdes = service.getAllSdeSummary();
            _listofSde = new ObservableCollection<SdeSumModel>();
            foreach (var sde in _sdes)
            {
                _listofSde.Add(sde);
            }


        }
       
    }

}
