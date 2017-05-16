using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Ht.Ihsil.Rgph.App.Superviseur.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.viewModels
{
   public class DecesViewModel:TreeViewItemViewModel
    {
       readonly DecesModel _model;
       private MenageViewModel _parentView;
       public DecesViewModel(DecesModel _deces, MenageViewModel _parent):base(_parent, true)
       {
           this._parentView = _parent;
           this._model = _deces;
       }
    }
}
