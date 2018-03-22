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
   public class BatimentCEViewModel:TreeViewItemViewModel
    {
        private BatimentCEModel _batiment;

        public BatimentCEModel Batiment
        {
            get { return _batiment; }
            set { _batiment = value; }
        }
       private ContreEnqueteService service_ce;

       public BatimentCEViewModel(BatimentCEModel batiment, ContreEnqueteViewModel parentRegion)
           : base(parentRegion, true)
       {
           this._batiment = batiment;
           service_ce = new ContreEnqueteService();
           if (batiment.IsContreEnqueteMade.GetValueOrDefault() == true && batiment.IsValidated.GetValueOrDefault()==false)
           {
               Status = true;
               Tip = Constant.GetStringValue(Constant.ToolTipMessage.Kont_anket_fet);
               ImageSource = Constant.GetStringValue(Constant.ImagePath.Fini);
           }
           if (batiment.IsValidated.GetValueOrDefault() == true && batiment.IsContreEnqueteMade.GetValueOrDefault()==true)
           {
               Status = true;
               Tip = Constant.GetStringValue(Constant.ToolTipMessage.Valide_deja);
               ImageSource = Constant.GetStringValue(Constant.ImagePath.Valide);
           }
           if (batiment.IsValidated.GetValueOrDefault() == false && batiment.IsContreEnqueteMade.GetValueOrDefault() == false)
           {
               Status = true;
               Tip = Constant.GetStringValue(Constant.ToolTipMessage.Kont_Anket_Not_Made);
               ImageSource = Constant.GetStringValue(Constant.ImagePath.MalRempli);
           }
       }
       public String BatimentName
       {
           get
           {
               return _batiment.BatimentName;
           }
       }
       public bool IsChecked
       {
           get
           {
               return _batiment.IsContreEnqueteMade.GetValueOrDefault();
           }
       }
       public bool IsValidate
       {
           get
           {
               return _batiment.IsValidated.GetValueOrDefault();
           }
       }
       public string SdeName
       {
           get
           {
               return _batiment.SdeId;
           }
       }
       public long BatimentId
       {
           get { return _batiment.BatimentId; }
       }
       protected override void LoadChildren()
       {
           this.Children.Clear();
           if (_batiment.TypeContreEnquete == (int)Constant.TypeContrEnquete.LogementIndividuelMenage 
               || _batiment.TypeContreEnquete == (int)Constant.TypeContrEnquete.LogementCollectif
               || _batiment.TypeContreEnquete == (int)Constant.TypeContrEnquete.LogementInvididuelVide
               || _batiment.TypeContreEnquete==(int)Constant.TypeContrEnquete.LogementOccupantAbsent)
           {
               SdeModel _sde = new SdeModel();
               _sde.SdeId = _batiment.SdeId;
               List<LogementCEModel> listOfLogement = service_ce.searchLogement(Convert.ToInt32(_batiment.BatimentId), _batiment.SdeId);
               foreach (LogementCEModel logement in listOfLogement)
               {
                   logement.LogeId = logement.LogeId;
                   if (service_ce.daoCE.getLogementCE(Convert.ToInt32(logement.BatimentId), logement.SdeId, Convert.ToInt32(logement.LogeId)).IsContreEnqueteMade == 1)
                   {
                       logement.IsChecked = true;
                   }
                   else
                   {
                       logement.IsChecked = false;
                   }
                   if (service_ce.daoCE.getLogementCE(Convert.ToInt32(logement.BatimentId), logement.SdeId, Convert.ToInt32(logement.LogeId)).IsValidated == 1)
                   {
                       logement.IsValidate = true;
                   }
                   else
                   {
                       logement.IsValidate = false;
                   }
                   base.Children.Add(new LogementCEViewModel(logement, this));
               }
           }
           
       }
    }
}
