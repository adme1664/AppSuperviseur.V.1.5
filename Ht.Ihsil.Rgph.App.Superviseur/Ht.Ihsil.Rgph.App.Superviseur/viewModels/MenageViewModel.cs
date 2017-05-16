using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Ht.Ihsil.Rgph.App.Superviseur.MVVM;
using Ht.Ihsil.Rgph.App.Superviseur.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.viewModels
{
    public class MenageViewModel:TreeViewItemViewModel
    {
        private MenageModel _menage;
        private LogementViewModel _parent;

        public MenageModel Model
        {
            get { return _menage; }
            set { _menage = value; }
        }


        public MenageViewModel(MenageModel _menage, LogementViewModel _parentView):base(_parentView,true)
        {
            this._menage = _menage;
            this._parent = _parentView;
            if (_menage.Statut == (int)Constant.StatutModule.MalRempli)
            {
                Status = true;
                Tip = Constant.GetStringValue(Constant.ToolTipMessage.MalRempli);
                ImageSource = Constant.GetStringValue(Constant.ImagePath.MalRempli);
            }
            if (_menage.Statut == (int)Constant.StatutModule.Fini)
            {
                Status = true;
                Tip = Constant.GetStringValue(Constant.ToolTipMessage.Fini);
                ImageSource = Constant.GetStringValue(Constant.ImagePath.Fini);
            }
            if (_menage.Statut == (int)Constant.StatutModule.PasFini)
            {
                Status = true;
                Tip = Constant.GetStringValue(Constant.ToolTipMessage.PasFini);
                ImageSource = Constant.GetStringValue(Constant.ImagePath.PasFini);
            }
        }

        public string MenageName
        {
            get { return _menage.MenageName; }
        }

        public string LogementName
        {
            get { return _parent.LogementName; }
        }
        public long MenageId
        {
            get { return _menage.MenageId; }
        }
        public long LogementId
        {
            get { return _menage.LogeId; }
        }
        public string NumSde
        {
            get { return _menage.SdeId; }
        }
        public long BatimentId
        {
            get { return _menage.BatimentId; }
        }
        public long BatimanName
        {
            get { return _parent.BatimentId; }
        }
        protected override void LoadChildren()
        {
            foreach (MenageTypeModel model in base.service.getAllInMenage())
            {
                model.SdeId = _menage.SdeId;
                base.Children.Add(new MenageTypeViewModel(model, this));
            }
        }

    }
}
