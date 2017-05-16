using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Ht.Ihsil.Rgph.App.Superviseur.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.viewModels
{
    public class MenageTypeViewModel:TreeViewItemViewModel
    {
        private MenageTypeModel _model;
        private MenageViewModel _parentView;
        public MenageTypeViewModel(MenageTypeModel _model, MenageViewModel _parent)
            : base(_parent, true)
        {
            this._model = _model;
            _parentView = _parent;
        }

        public string NodeName
        {
            get { return this._model.Name; }
        }

        public string BatimanName
        {
            get { return _parentView.BatimanName.ToString(); }
        }
        public string LogementName
        {
            get { return _parentView.LogementName; }
        }
        public string MenageName
        {
            get { return _parentView.MenageName; }
        }
        protected override void LoadChildren()
        {
            foreach (MenageDetailsModel m in _parentView.service.getDetailsMenage(_parentView.Model, _model))
            {
                m.SdeId = _model.SdeId;
                base.Children.Add(new MenageDetailsViewModel(m, this));
            }
        }
    }
}
