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
    public class MenageDetailsViewModel:TreeViewItemViewModel
    {
        private MenageDetailsModel menage;
        private MenageTypeViewModel _parentView;
        private LogementViewModel _logementParentView;
        public MenageDetailsModel Menage
        {
            get { return menage; }
            set { menage = value; }
        }
        public MenageDetailsViewModel(MenageDetailsModel model, MenageTypeViewModel _parent)
            : base(_parent, true)
        {
            this.menage = model;
            _parentView = _parent;
            if (model.Statut == (int)Constant.StatutModule.MalRempli)
            {
                Status = true;
                Tip = Constant.GetStringValue(Constant.ToolTipMessage.MalRempli);
                ImageSource = Constant.GetStringValue(Constant.ImagePath.MalRempli);
            }
            if (model.Statut == (int)Constant.StatutModule.Fini)
            {
                Status = true;
                Tip = Constant.GetStringValue(Constant.ToolTipMessage.Fini);
                ImageSource = Constant.GetStringValue(Constant.ImagePath.Fini);
            }
            if (model.Statut == (int)Constant.StatutModule.PasFini)
            {
                Status = true;
                Tip = Constant.GetStringValue(Constant.ToolTipMessage.PasFini);
                ImageSource = Constant.GetStringValue(Constant.ImagePath.PasFini);
            }
        }
        public MenageDetailsViewModel(MenageDetailsModel model, LogementViewModel _parent)
            : base(_parent, true)
        {
            this.menage = model;
            _logementParentView = _parent;
            if (model.Statut == (int)Constant.StatutModule.MalRempli)
            {
                Status = true;
                Tip = Constant.GetStringValue(Constant.ToolTipMessage.MalRempli);
                ImageSource = Constant.GetStringValue(Constant.ImagePath.MalRempli);
            }
            if (model.Statut == (int)Constant.StatutModule.Fini)
            {
                Status = true;
                Tip = Constant.GetStringValue(Constant.ToolTipMessage.Fini);
                ImageSource = Constant.GetStringValue(Constant.ImagePath.Fini);
            }
            if (model.Statut == (int)Constant.StatutModule.PasFini)
            {
                Status = true;
                Tip = Constant.GetStringValue(Constant.ToolTipMessage.PasFini);
                ImageSource = Constant.GetStringValue(Constant.ImagePath.PasFini);
            }
        }

        public string NodeName
        {
            get { return menage.Name; }
        }
        public string MenageDetailsId
        {
            get { return menage.Id; }
        }
        public int Type
        {
            get { return menage.Type; }
        }
        public string Batiname
        {
            get { return _parentView.BatimanName; }
        }
        public string LogementName
        {
            get { return _parentView.LogementName; }
        }
        public string MenageName
        {
            get { return _parentView.MenageName; }
        }
    }
}
