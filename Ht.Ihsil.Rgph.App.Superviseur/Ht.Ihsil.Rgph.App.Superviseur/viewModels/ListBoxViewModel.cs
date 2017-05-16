
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.viewModels
{
    public class ListBoxViewModel
    {
        readonly ReadOnlyCollection<SdeSumModel> _sdes;

        public ListBoxViewModel(SdeSumModel[] sdes)
        {
            _sdes = new ReadOnlyCollection<SdeSumModel>(
                (from sde in sdes
                 select new SdeSumModel())
                .ToList());
        }
        public ReadOnlyCollection<SdeSumModel> Sdes
        {
            get { return _sdes; }
        }
    }
}
