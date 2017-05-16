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
   public class ContreEnqueteViewModel:TreeViewItemViewModel
    {
        private ContreEnqueteModel _contreEnquete;
        private string _contreEnqueteName;

        public string ContreEnqueteName
        {
            get { return "Kont-ankèt-"+_contreEnquete.ContreEnqueteId; }
            set { _contreEnqueteName = value; }
        }

        public bool IsTerminate
        {
            get { return _contreEnquete.IsTerminate; }
        }
        public bool IsValidate
        {
            get { return _contreEnquete.IsValidate; }
        }
        public ContreEnqueteModel ContreEnquete
        {
            get { return _contreEnquete; }
            set { _contreEnquete = value; }
        }
       private IContreEnqueteService _service_ce;

       public ContreEnqueteViewModel(ContreEnqueteModel model, SdeCEViewModel parentRegion)
           :base(parentRegion, true)
       {
           this._contreEnquete = model;
           service = new SqliteDataReaderService();
           _service_ce = new ContreEnqueteService();
           BatimentCEModel bat = _service_ce.getBatiment(model.BatimentId.GetValueOrDefault(), model.SdeId);
           if (bat != null)
           {
               if (bat.IsContreEnqueteMade.GetValueOrDefault() == true && bat.IsValidated.GetValueOrDefault()==false)
               {
                   Status = true;
                   Tip = Constant.GetStringValue(Constant.ToolTipMessage.Kont_anket_fet);
                   ImageSource = Constant.GetStringValue(Constant.ImagePath.Fini);
               }
               if (bat.IsValidated.GetValueOrDefault() == true && bat.IsContreEnqueteMade.GetValueOrDefault() == true)
               {
                   Status = true;
                   Tip = Constant.GetStringValue(Constant.ToolTipMessage.Valide_deja);
                   ImageSource = Constant.GetStringValue(Constant.ImagePath.Valide);
               }
               if (bat.IsValidated.GetValueOrDefault() == false && bat.IsContreEnqueteMade.GetValueOrDefault() == false)
               {
                   Status = true;
                   Tip = Constant.GetStringValue(Constant.ToolTipMessage.Kont_Anket_Not_Made);
                   ImageSource = Constant.GetStringValue(Constant.ImagePath.MalRempli);
               }
           }
       }
      
       protected override void LoadChildren()
       {
           this.Children.Clear();
           BatimentCEModel bat = _service_ce.getBatiment(_contreEnquete.BatimentId.GetValueOrDefault(), _contreEnquete.SdeId);
           bat.TypeContreEnquete = _contreEnquete.TypeContreEnquete.GetValueOrDefault();
           base.Children.Add(new BatimentCEViewModel(bat, this)); 
       }
    }
}
