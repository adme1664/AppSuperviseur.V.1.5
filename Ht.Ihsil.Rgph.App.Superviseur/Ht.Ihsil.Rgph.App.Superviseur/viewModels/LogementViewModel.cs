using Ht.Ihsi.Rgph.Logging.Logs;
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
    public class LogementViewModel : TreeViewItemViewModel
        
    {
        readonly LogementModel _logement;
        Logger log;
        ISqliteReader sr;
        public LogementModel Logement
        {
            get { return _logement; }
        } 

        private LogementTypeViewModel parentView;
        public LogementViewModel(LogementModel _logement, LogementTypeViewModel _parent):base(_parent,true)
        {
            this._logement = _logement;
            parentView = _parent;
            log = new Logger();
            sr = new SqliteReader(Utilities.getConnectionString(Users.users.DatabasePath, _logement.SdeId));
            if(_logement.Statut==(int)Constant.StatutModule.MalRempli)
            {
                Status = true;
                Tip = Constant.GetStringValue(Constant.ToolTipMessage.MalRempli);
                ImageSource = Constant.GetStringValue(Constant.ImagePath.MalRempli);
            }
            if (_logement.Statut == (int)Constant.StatutModule.Fini)
            {
                Status = true;
                Tip = Constant.GetStringValue(Constant.ToolTipMessage.Fini);
                ImageSource = Constant.GetStringValue(Constant.ImagePath.Fini);
            }
            if (_logement.Statut == (int)Constant.StatutModule.PasFini)
            {
                Status = true;
                Tip = Constant.GetStringValue(Constant.ToolTipMessage.PasFini);
                ImageSource = Constant.GetStringValue(Constant.ImagePath.PasFini);
            }
            
        }

        public string LogementName
        {
            get { return "Lojman-" +_logement.Qlin1NumeroOrdre.ToString(); }
        }
        public long LogementId
        {
            get { return _logement.LogeId; }
        }
        public string NumSde
        {
            get { return _logement.SdeId; }
        }
        public long BatimentId
        {
            get { return parentView.BatimentId; }
        }
        protected override void LoadChildren()
        {
            if (parentView.LogementType.LogementName == "Lojman kolektif")
            {
                log.Info("<>==================:Im here at logement collectif");
                foreach (IndividuModel ind in sr.GetIndividuByLoge(_logement.LogeId))
                {
                    MenageDetailsModel details = new MenageDetailsModel();
                    details.Name = ind.IndividuName;
                    details.SdeId = ind.SdeId;
                    details.IsContreEnqueteMade = ind.IsContreEnqueteMade;
                    details.BatimentId = ind.BatimentId;
                    details.LogementId = ind.LogeId;
                    details.Statut = ind.Statut;
                    details.Id = ind.IndividuId.ToString();
                    details.Type = Constant.CODE_TYPE_ENVDIVIDI;
                    base.Children.Add(new MenageDetailsViewModel(details, this));
                }
            }
            else
            {
                foreach (MenageModel _menage in base.service.getAllMenage(_logement))
                {
                    _menage.SdeId = _logement.SdeId;
                    base.Children.Add(new MenageViewModel(_menage, this));
                }
            }
       }
    }
}
