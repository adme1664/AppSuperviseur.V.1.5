using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Ht.Ihsil.Rgph.App.Superviseur.MVVM;
using Ht.Ihsil.Rgph.App.Superviseur.services;
using Ht.Ihsil.Rgph.App.Superviseur.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.viewModels
{
    public class LogementTypeViewModel:TreeViewItemViewModel
    {
        readonly LogementTypeModel _logementType;

        public LogementTypeModel LogementType
        {
            get { return _logementType; }
        } 

        private BatimentViewModel _parent;
        private SqliteDataReaderService service=null;
        public LogementTypeViewModel(LogementTypeModel _logementType, BatimentViewModel _parentView)
            : base(_parentView, true)
        {
            this._logementType = _logementType;
            _parent = _parentView;
            
        }
        

        public string LogementName
        {
            get
            {
                return LogementType.LogementName;
            }
        }

        public long BatimentId
        {
            get
            {
                return LogementType.BatimentId;
            }
        }
        protected override void LoadChildren()
        {
            foreach (LogementModel logement in _parent.service.getAllLogement(_parent.Batiment,LogementType))
            {
                logement.SdeId = _parent.SdeName;
                base.Children.Add(new LogementViewModel(logement,this));
            }
        }
    }
}
