using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Ht.Ihsil.Rgph.App.Superviseur.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Interactivity.InteractionRequest;
using Ht.Ihsil.Rgph.App.Superviseur.services;
using Ht.Ihsil.Rgph.App.Superviseur.Mapper;
using Ht.Ihsil.Rgph.App.Superviseur.utils;
using Com.Controls.MessageBox;
using System.Windows;


namespace Ht.Ihsil.Rgph.App.Superviseur.viewModels
{
    public class BatimentViewModel : TreeViewItemViewModel
    {
        private BatimentModel _batiment;
        ContreEnqueteService serviceCe = null;
        SqliteDataReaderService service = null;
        private readonly InteractionRequest<Notification> _notificationInteractionRequest;
        public BatimentModel Batiment
        {
            get { return _batiment; }
            set { _batiment = value; }
        }


        public BatimentViewModel(BatimentModel batiment, SdeViewModel parentRegion):base(parentRegion,true)
        {
            this._batiment = batiment;
            serviceCe = new ContreEnqueteService(batiment.SdeId);
            _notificationInteractionRequest = new InteractionRequest<Notification>();
            if (batiment.Statut == (int)Constant.StatutModule.MalRempli)
            {
                Status = true;
                Tip = Constant.GetStringValue(Constant.ToolTipMessage.MalRempli);
                ImageSource = Constant.GetStringValue(Constant.ImagePath.MalRempli);
            }
            if (batiment.Statut == (int)Constant.StatutModule.PasFini)
            {
                Status = true;
                Tip = Constant.GetStringValue(Constant.ToolTipMessage.PasFini);
                ImageSource = Constant.GetStringValue(Constant.ImagePath.PasFini);
            }
            if (batiment.Statut == (int)Constant.StatutModule.Fini)
            {
                Status = true;
                Tip = Constant.GetStringValue(Constant.ToolTipMessage.Fini);
                ImageSource = Constant.GetStringValue(Constant.ImagePath.Fini);
            }
            if (batiment.IsValidated == Convert.ToBoolean(Constant.StatutValide.Valide))
            {
                Status = true;
                Tip = Constant.GetStringValue(Constant.ToolTipMessage.Valide_deja);
                ImageSource = Constant.GetStringValue(Constant.ImagePath.Valide);
            }
            BatimentCEModel bat = ModelMapper.MapToBatimentCEModel(serviceCe.daoCE.getBatiment(Convert.ToInt32(batiment.BatimentId), batiment.SdeId));
            if (bat.BatimentId != 0)
            {
                Status = true;
                Tip = Constant.GetStringValue(Constant.ToolTipMessage.Kont_Anket_IN);
                ImageSource = Constant.GetStringValue(Constant.ImagePath.ContreEnquete);
            }
        }
        public void openConnection()
        {
            service = new SqliteDataReaderService(Utilities.getConnectionString(Users.users.DatabasePath, _batiment.SdeId));
        }
        

        public String BatimentName
        {
            get
            {
                return _batiment.BatimentName;
            }
        }
        public string SdeName
        {
            get
            {
                return _batiment.SdeId;
            }
        }
        public bool IsCheched
        {
            get
            {
                return _batiment.IsChecked;
            }
        }
        public long BatimentId
        {
            get { return _batiment.BatimentId; }
        }
        protected override void LoadChildren()
        {
            BatimentCEModel bat = ModelMapper.MapToBatimentCEModel(serviceCe.daoCE.getBatiment(Convert.ToInt32(_batiment.BatimentId), _batiment.SdeId));
            if (bat.BatimentId != 0)
            {
                _notificationInteractionRequest.Raise(
                    new Notification 
                    {
                        Title=Constant.WINDOW_TITLE,
                        Content = new MessageBoxConent
                        {
                            Message="Batiman sa a gentan chwazi nan yon kont ankèt.",
                            MessageBoxButton=MessageBoxButtons.OK,
                            MessageBoxImage=MessageBoxIcon.Error,
                            IsModalWindow=true,
                            ParentWindow=Application.Current.MainWindow
                
                        }
                    },
                    delegate { }
                );
            }
            else
            {
                openConnection();
                foreach (LogementTypeModel logement in service.getLogementType())
                {
                    logement.BatimentId = BatimentId;
                    base.Children.Add(new LogementTypeViewModel(logement, this));
                }
            }
           
        }
    }
}
