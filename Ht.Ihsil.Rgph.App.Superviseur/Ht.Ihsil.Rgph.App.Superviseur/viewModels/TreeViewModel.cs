using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.viewModels
{
   public class TreeViewModel
    {
       readonly ReadOnlyCollection<SdeViewModel> _sdes;
       

       public TreeViewModel(SdeModel[] models)
        {
            List<SdeViewModel> list = new List<SdeViewModel>();
           foreach (SdeModel sde in models.ToList())
            {
                SdeViewModel view = new SdeViewModel(sde);
                view.IsLoading = false;
                list.Add(view);
            }
           _sdes =new ReadOnlyCollection<SdeViewModel>(list);
        }

        public ReadOnlyCollection<SdeViewModel> Sdes
        {
            get { return _sdes; }
        }
    }
}
