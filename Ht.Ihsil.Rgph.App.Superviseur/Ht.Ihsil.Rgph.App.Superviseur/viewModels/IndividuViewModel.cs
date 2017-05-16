using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Ht.Ihsil.Rgph.App.Superviseur.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.viewModels
{
    public class IndividuViewModel : TreeViewItemViewModel
    {
       readonly IndividuModel _model;
       private MenageViewModel _parentView;
       private LogementViewModel _logementCView;
       public string IndividuName
       {
           get { return _model.IndividuName; }
       }
       public IndividuViewModel(IndividuModel individu, MenageViewModel _parent)
           : base(_parent, true)
       {
           this._parentView = _parent;
           this._model = individu;
       }
       public IndividuViewModel(IndividuModel individu, LogementViewModel _logementParent)
           : base(_logementParent, true)
       {
           this._logementCView = _logementParent;
           this._model = individu;
       }
    }
}
