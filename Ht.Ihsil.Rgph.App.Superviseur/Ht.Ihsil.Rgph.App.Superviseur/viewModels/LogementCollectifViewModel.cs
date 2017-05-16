using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Ht.Ihsil.Rgph.App.Superviseur.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.viewModels
{
    public class LogementCollectifViewModel:TreeViewItemViewModel
    {
        readonly LogementModel _logementC;
        public LogementCollectifViewModel(LogementModel _logementC, LogementTypeViewModel _parentView)
            : base(_parentView, true)
        {
            this._logementC = _logementC;
        }
        protected override void LoadChildren()
        {
            //for(LogementModel _logementc in base.service.getAllLogementCollectif())
        }
    }
}
