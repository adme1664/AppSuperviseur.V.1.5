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
   public class MenageCEViewModel:TreeViewItemViewModel
    {
        private MenageCEModel _menage;
        private LogementCEViewModel _parent;
        public MenageCEModel Menage
        {
            get { return _menage; }
            set { _menage = value; }
        }
       private IContreEnqueteService service_ce;

       public MenageCEViewModel(MenageCEModel model, LogementCEViewModel parentRegion)
           :base(parentRegion,true)
       {
           _menage = model;
           service_ce = new ContreEnqueteService();
           service = new SqliteDataReaderService();
           _parent = parentRegion;
           if (model.IsContreEnqueteMade.GetValueOrDefault() == true && model.IsValidated.GetValueOrDefault() == false)
           {
               Status = true;
               Tip = Constant.GetStringValue(Constant.ToolTipMessage.Kont_anket_fet);
               ImageSource = Constant.GetStringValue(Constant.ImagePath.Fini);
           }
           if (model.IsContreEnqueteMade.GetValueOrDefault() == true && model.IsValidated.GetValueOrDefault() == true)
           {
               Status = true;
               Tip = Constant.GetStringValue(Constant.ToolTipMessage.Valide_deja);
               ImageSource = Constant.GetStringValue(Constant.ImagePath.Valide);
           }
           if (model.IsContreEnqueteMade.GetValueOrDefault() == false && model.IsValidated.GetValueOrDefault() == false)
           {
               Status = true;
               Tip = Constant.GetStringValue(Constant.ToolTipMessage.Kont_Anket_Not_Made);
               ImageSource = Constant.GetStringValue(Constant.ImagePath.MalRempli);
           }
       }
       public string MenageName
       {
           get { return _menage.MenageName; }
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
       
       protected override void LoadChildren()
       {
           this.Children.Clear();
           foreach (MenageTypeModel men in base.service.getAllinMenageCE())
           {
               base.Children.Add(new MenageTypeViewModel(men, this));
           }
       }
    }
}
