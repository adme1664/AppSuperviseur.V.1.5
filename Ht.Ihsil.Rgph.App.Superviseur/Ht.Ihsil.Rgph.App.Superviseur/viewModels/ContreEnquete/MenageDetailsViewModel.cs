using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Ht.Ihsil.Rgph.App.Superviseur.MVVM;
using Ht.Ihsil.Rgph.App.Superviseur.services;
using Ht.Ihsil.Rgph.App.Superviseur.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.viewModels.ContreEnquete
{
   public class MenageDetailsViewModel:TreeViewItemViewModel
    {
        private MenageDetailsModel menage;
        private MenageTypeViewModel _parentView;
        private LogementCEViewModel logementParentView;
        public MenageDetailsModel MenageType
        {
            get { return menage; }
            set { menage = value; }
        }

        public MenageDetailsViewModel(MenageDetailsModel model, LogementCEViewModel parentRegion)
            : base(parentRegion, false)
        {
            menage = model;
            logementParentView = parentRegion;
            if (model.IsContreEnqueteMade == true && model.Valide == false)
            {
                Status = true;
                Tip = Constant.GetStringValue(Constant.ToolTipMessage.Kont_anket_fet);
                ImageSource = Constant.GetStringValue(Constant.ImagePath.Fini);
            }
            if (model.IsContreEnqueteMade == true && model.Valide == true)
            {
                Status = true;
                Tip = Constant.GetStringValue(Constant.ToolTipMessage.Valide_deja);
                ImageSource = Constant.GetStringValue(Constant.ImagePath.Valide);
            }
            if (model.IsContreEnqueteMade == false && model.Valide == false)
            {
                Status = true;
                Tip = Constant.GetStringValue(Constant.ToolTipMessage.Kont_Anket_Not_Made);
                ImageSource = Constant.GetStringValue(Constant.ImagePath.MalRempli);
            }
        }
       public MenageDetailsViewModel(MenageDetailsModel model, MenageTypeViewModel parentRegion)
           :base(parentRegion,false)
       {
           menage = model;
           _parentView = parentRegion;
           if (model.IsContreEnqueteMade == true && model.Valide == false)
           {
               Status = true;
               Tip = Constant.GetStringValue(Constant.ToolTipMessage.Kont_anket_fet);
               ImageSource = Constant.GetStringValue(Constant.ImagePath.Fini);
           }
           if (model.IsContreEnqueteMade == true && model.Valide == true)
           {
               Status = true;
               Tip = Constant.GetStringValue(Constant.ToolTipMessage.Valide_deja);
               ImageSource = Constant.GetStringValue(Constant.ImagePath.Valide);
           }
           if (model.IsContreEnqueteMade == false && model.Valide == false)
           {
               Status = true;
               Tip = Constant.GetStringValue(Constant.ToolTipMessage.Kont_Anket_Not_Made);
               ImageSource = Constant.GetStringValue(Constant.ImagePath.MalRempli);
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
  }

    

}
