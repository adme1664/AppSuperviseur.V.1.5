using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Ht.Ihsil.Rgph.App.Superviseur.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.viewModels
{
   public class EmigreViewModel:TreeViewItemViewModel
    {
        private MenageViewModel _parentView;
        readonly EmigreModel _emigre;
        public EmigreViewModel(EmigreModel _emigre, MenageViewModel _parent)
            : base(_parent, true)
       {
           this._parentView = _parent;
           this._emigre = _emigre;
       }

        //public string EmigreName
        //{
        //    get { return ""}
        //}
    }
}
