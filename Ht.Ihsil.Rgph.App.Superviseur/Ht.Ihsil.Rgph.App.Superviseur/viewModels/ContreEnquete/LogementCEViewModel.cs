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
   public class LogementCEViewModel:TreeViewItemViewModel
    {
        private LogementCEModel _logement;
        private BatimentCEViewModel _parentView;
        public LogementCEModel Logement
        {
            get { return _logement; }
            set { _logement = value; }
        }
       private ContreEnqueteService service_ce;

       public LogementCEViewModel(LogementCEModel model, BatimentCEViewModel parentRegion)
           :base(parentRegion, true)
       {
           _logement = model;
           service_ce = new ContreEnqueteService();
           service = new SqliteDataReaderService();
           _parentView = parentRegion;

           if (model.IsContreEnqueteMade.GetValueOrDefault() == 1 && model.IsValidated.GetValueOrDefault() == 0)
           {
               Status = true;
               Tip = Constant.GetStringValue(Constant.ToolTipMessage.Kont_anket_fet);
               ImageSource = Constant.GetStringValue(Constant.ImagePath.Fini);
           }
           if (model.IsContreEnqueteMade.GetValueOrDefault() == 1 && model.IsValidated.GetValueOrDefault() == 1)
           {
               Status = true;
               Tip = Constant.GetStringValue(Constant.ToolTipMessage.Valide_deja);
               ImageSource = Constant.GetStringValue(Constant.ImagePath.Valide);
           }
           if (model.IsContreEnqueteMade.GetValueOrDefault() == 0 && model.IsValidated.GetValueOrDefault() == 0)
           {
               Status = true;
               Tip = Constant.GetStringValue(Constant.ToolTipMessage.Kont_Anket_Not_Made);
               ImageSource = Constant.GetStringValue(Constant.ImagePath.MalRempli);
           }
       }
       public string LogementName
       {
           get { return _logement.Qlin1NumeroOrdre.ToString(); }
       }
       public bool IsChecked
       {
           get
           {
               return _logement.IsChecked;
           }
       }
       public bool IsValidate
       {
           get
           {
               return _logement.IsValidate.GetValueOrDefault();
           }
       }
       public long LogementId
       {
           get { return _logement.LogeId; }
       }
       public string Name
       {
           get { return "Lojman -" + _logement.Qlin1NumeroOrdre.GetValueOrDefault(); }
       }
       public string NumSde
       {
           get { return _logement.SdeId; }
       }
       public long BatimentId
       {
           get { return _parentView.BatimentId; }
       }
       protected override void LoadChildren()
       {
           this.Children.Clear();
           if (_parentView.Batiment.TypeContreEnquete == (int)Constant.TypeContrEnquete.LogementCollectif)
           {
               foreach (IndividuCEModel ind in service_ce.searchAllIndividuCE(_logement.LogeId))
               {
                   MenageDetailsModel details = new MenageDetailsModel();
                   details.SdeId = ind.SdeId;
                   details.BatimentId = ind.BatimentId;
                   details.LogementId = ind.LogeId;
                   details.Id = ind.IndividuId.ToString();
                   details.Name = "Endividi-" + ind.Qp1NoOrdre;
                   details.Type = Constant.CODE_TYPE_ENVDIVIDI;
                   details.IsContreEnqueteMade = Convert.ToBoolean(ind.IsContreEnqueteMade.GetValueOrDefault());
                   details.Valide = Convert.ToBoolean(ind.IsValidated.GetValueOrDefault());
                   base.Children.Add(new MenageDetailsViewModel(details, this));
               }
           }
           if (_parentView.Batiment.TypeContreEnquete == (int)Constant.TypeContrEnquete.LogementIndividuelMenage)
           {
               foreach (MenageCEModel _menage in service_ce.searchAllMenageCE(_logement))
               {
                   _menage.SdeId = _logement.SdeId;
                   if (service_ce.daoCE.getMenageCE(_menage.BatimentId, _menage.LogeId, _menage.SdeId, _menage.MenageId).IsContreEnqueteMade == 1)
                   {
                       _menage.IsChecked = true;
                   }
                   else
                   {
                       _menage.IsChecked = false;
                   }
                   if (service_ce.daoCE.getMenageCE(_menage.BatimentId, _menage.LogeId, _menage.SdeId, _menage.MenageId).IsValidated == 1)
                   {
                       _menage.IsValidate = true;
                   }
                   else
                   {
                       _menage.IsValidate = false;
                   }
                   base.Children.Add(new MenageCEViewModel(_menage, this));
               }
           }
       }
    }
}
