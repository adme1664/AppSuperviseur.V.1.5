using Ht.Ihsi.Rgph.Logging.Logs;
using Ht.Ihsil.Rgph.App.Superviseur.services;
using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Ht.Ihsil.Rgph.App.Superviseur.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ht.Ihsil.Rgph.App.Superviseur.utils;
using System.Threading;
using System.Windows.Threading;

namespace Ht.Ihsil.Rgph.App.Superviseur.viewModels
{
    public class SdeViewModel : TreeViewItemViewModel
    {
        private SdeModel _sde;
        private bool isWaitVisible;
        bool _isLoading;
        Thread t;
        ThreadStart ths;
        string pathDefaultConfigurationFile = AppDomain.CurrentDomain.BaseDirectory + @"App_data\";
        string file = "";
        XmlUtils configuration = null;

        private bool _semaine1 = true;

        public bool Semaine1
        {
            get { return _semaine1; }
            set 
            {
                if (_semaine1 != value)
                {
                    _semaine1 = value;
                    this.OnPropertyChanged("Semaine1");
                }
                
            }
        }
        private bool _semaine2 = true;

        public bool Semaine2
        {
            get { return _semaine2; }
            set
            {
                if (_semaine2 != value)
                {
                    _semaine2 = value;
                    this.OnPropertyChanged("Semaine2");
                }

            }
        }
        private bool _semaine3 = true;

        public bool Semaine3
        {
            get { return _semaine3; }
            set
            {
                if (_semaine3 != value)
                {
                    _semaine3 = value;
                    this.OnPropertyChanged("Semaine3");
                }

            }
        }

        bool _allreadyLoad;

        public bool AllreadyLoad
        {
            get { return _allreadyLoad; }
            set 
            {
                if (_allreadyLoad != value)
                {
                    _allreadyLoad = value;
                    this.OnPropertyChanged("AllreadyLoad");
                }
            }
        }
        public bool IsLoading
        {
            get { return this._isLoading; }
            set
            {
                if (value != _isLoading)
                {
                    _isLoading = value;
                    this.OnPropertyChanged("IsLoading");
                }
            }
        }
        public SdeModel Sde
        {
            get { return _sde; }
            set { _sde = value; }
        }
       Logger log;
        
       public SdeViewModel(SdeModel sde) 
            : base(null, true)
        {
            _sde = sde;
            log = new Logger();
            service = new SqliteDataReaderService(Utilities.getConnectionString(Users.users.DatabasePath, _sde.SdeId));
            activerMenuSemaine();
        }

       public bool IsWaitVisible
       {
           get
           {
               return this.isWaitVisible;
           }
           set { this.isWaitVisible = value; }
       }
        public string SdeName
        {
            get { return _sde.SdeId; }

        }
        public override  bool IsExpand()
        {
            if (base.IsExpanded == true){
                IsWaitVisible = true;
                log.Info("IsExpanded: true");
                return true;
            }
            else
                IsWaitVisible = false;
                log.Info("IsExpanded: false");
                return false;
        }
        protected override void LoadChildren()
        {
            try
            {
                BatimentModel[] batiments = base.service.getAllBatiments();
                if (batiments == null)
                {

                }
                else
                {

                    foreach (BatimentModel batiment in batiments)
                    {
                        batiment.SdeId = _sde.SdeId;
                        Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, (Action)(() => base.Children.Add(new BatimentViewModel(batiment, this))));
                    }
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, (Action)(() => IsLoading = false));
                Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, (Action)(() => AllreadyLoad = true));
                
            }
            
        }

        public void getAllBatiments()
        {
            BatimentModel[] batiments = base.service.getAllBatiments();
            foreach (BatimentModel batiment in batiments)
            {
                if (batiment.Statut == (int)Constant.StatutModule.MalRempli)
                {
                    batiment.IsMalRempli = true;
                }
                if (batiment.Statut == (int)Constant.StatutModule.PasFini)
                {
                    batiment.IsNotFinished = true;
                }
                if (batiment.Statut == (int)Constant.StatutModule.Fini)
                {
                    batiment.IsFinished = true;
                }
                batiment.SdeId = _sde.SdeId;
                base.Children.Add(new BatimentViewModel(batiment, this));
            }
        }
        protected override void Selected()
        {
            if (base.IsSelected == true)
            {
                log.Info("<>===========Selected");
                log.Info("<>===========SDE:" + _sde.SdeId);
            }
        }

        public void activerMenuSemaine()
        {
            file = pathDefaultConfigurationFile + "contreenquete.xml";
            configuration = new XmlUtils(file);
            //Activer les contrenquetes des batiments ayant des logements individuels complets
            List<KeyValue> listOf = configuration.getSemaineOfContreEnquete();
            foreach (KeyValue semaine in listOf)
            {
                if (semaine.Key == 1 && semaine.Value == "false")
                    Semaine1 = false;
                if (semaine.Key == 2 && semaine.Value == "false")
                    Semaine2 = false;
                if (semaine.Key == 3 && semaine.Value == "false")
                    Semaine3 = false;
            }
        }
       
    }
}
