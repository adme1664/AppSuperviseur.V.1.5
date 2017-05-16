using Ht.Ihsi.Rgph.Logging.Logs;
using Ht.Ihsil.Rgph.App.Superviseur.services;
using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Ht.Ihsil.Rgph.App.Superviseur.utils;
using Ht.Ihsil.Rgph.App.Superviseur.viewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ht.Ihsil.Rgph.App.Superviseur.MVVM;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using Ht.Ihsi.Rgph.Utility.Utils;
using System.ComponentModel;
using System.Threading;
using Ht.Ihsil.Rgph.App.Superviseur.Exceptions;
using Ht.Ihsil.Rgph.App.Superviseur.Mapper;
using Ht.Ihsil.Rgph.App.Superviseur.views.Contre_Enquete;

namespace Ht.Ihsil.Rgph.App.Superviseur.views
{
    /// <summary>
    /// Logique d'interaction pour frm_visualisation.xaml
    /// </summary>
    public partial class frm_visualisation : UserControl
    {
        #region DECLARATION

        Logger log;
        TreeViewModel model;
        SqliteDataReaderService service = null;
        ContreEnqueteService service_ce = null;
        MdfService mdfService = null;
        ConfigurationService confService = null;
        BackgroundWorker bckw;
        ISqliteDataWriter sw;
        TreeViewItem getTreeviewItem;
        string raison = "";
        #endregion

        #region PROPERTIES
        public TreeViewItem GetTreeviewItem
        {
            get { return getTreeviewItem; }
            set { getTreeviewItem = value; }
        }
        #endregion

        #region CONSTRUCTORS
        public frm_visualisation()
        {
            InitializeComponent();
            log = new Logger();
            service = new SqliteDataReaderService();
            bckw = new BackgroundWorker();
            Users.users.SupDatabasePath = AppDomain.CurrentDomain.BaseDirectory + @"Data\";
            service_ce = new ContreEnqueteService(Users.users.SupDatabasePath);
            mdfService = new MdfService();
            SdeModel[] sdeModel = mdfService.getAllSde();
            model = new TreeViewModel(sdeModel);
            base.DataContext = model;
            sw = new SqliteDataWriter();
        }
        #endregion

        #region AFFICHAGE
        private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            try
            {
                TreeViewItem currentContainer = e.OriginalSource as TreeViewItem;
                this.GetTreeviewItem = currentContainer;
                SdeViewModel _sde;
                BatimentViewModel _batiment;
                LogementViewModel _logement;
                MenageViewModel _menage;
                MenageDetailsViewModel _menageDetails;
                IndividuViewModel _individu;
                scrl_bar_1.Visibility = Visibility.Visible;
                string name = "";
                while (currentContainer != null)
                {
                    if (currentContainer.DataContext.ToString() == Constant.DATACONTEXT_INDIVIDUVIEWMODEL)
                    {
                        _individu = currentContainer.DataContext as IndividuViewModel;
                    }
                    if (currentContainer.DataContext.ToString() == Constant.DATACONTEXT_SDEVIEWMODEL)
                    {
                        _sde = currentContainer.DataContext as SdeViewModel;
                        frm_details_sdes fr_sde = new frm_details_sdes(_sde.Sde);
                        fr_sde.lbl_details_sde.Text = Utilities.getGeoInformation(_sde.SdeName);
                        Dispatcher.Invoke(new Action(() =>
                        {
                            wInd.DeferedVisibility = true;
                        }));
                        Utilities.showControl(fr_sde, grd_details);
                    }
                    else
                        if (currentContainer.DataContext.ToString() == Constant.DATACONTEXT_BATIMENTVIEWMODEL)
                        {
                            _batiment = currentContainer.DataContext as BatimentViewModel;
                            BatimentCEModel _bat = ModelMapper.MapToBatimentCEModel(service_ce.daoCE.getBatiment(Convert.ToInt32(_batiment.BatimentId), _batiment.SdeName));
                            if (_bat.BatimentId == 0)
                            {
                                frm_view_ce fr_bat = new frm_view_ce(_batiment.Batiment, _batiment.SdeName);
                                Utilities.showControl(fr_bat, grd_details);
                            }
                            else
                            {
                                List<LogementCEModel> logements = service_ce.searchLogement(Convert.ToInt32(_bat.BatimentId), _bat.SdeId);
                                BatimentInCeModel batModel = new BatimentInCeModel();
                                if (logements.Count() != 0)
                                {
                                    foreach (LogementCEModel _log in logements)
                                    {
                                        List<MenageCEModel> menages = ModelMapper.MapToListMenageCEModel(service_ce.daoCE.searchAllMenageCE(Convert.ToInt32(_bat.BatimentId), _log.LogeId, _bat.SdeId));
                                        if (menages.Count() != 0)
                                        {
                                            foreach (MenageCEModel _men in menages)
                                            {

                                                batModel.BatimentId = _bat.BatimentId.ToString();
                                                batModel.Rec = _bat.Qrec;
                                                batModel.Rgph = _bat.Qrgph;
                                                batModel.SdeId = _bat.SdeId;
                                                List<IndividuCEModel> individus = ModelMapper.MapToListIndividuCEModel(service_ce.daoCE.searchAllIndividuCE(_bat.BatimentId, _log.LogeId, _bat.SdeId, _men.MenageId));
                                                foreach (IndividuCEModel _ind in individus)
                                                {
                                                    if (_ind.Q6LienDeParente.GetValueOrDefault() == 1)
                                                    {
                                                        batModel.NomChefMenage = _ind.Q2Nom + " " + _ind.Q3Prenom;
                                                        frm_view_ce view = new frm_view_ce(batModel);
                                                        Utilities.showControl(view, grd_details);
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            batModel.BatimentId = _bat.BatimentId.ToString();
                                            batModel.Rec = _bat.Qrec;
                                            batModel.Rgph = _bat.Qrgph;
                                            batModel.SdeId = _bat.SdeId;
                                            frm_view_ce view = new frm_view_ce(batModel);
                                            Utilities.showControl(view, grd_details);
                                        }
                                    }
                                }
                                else
                                {
                                    batModel.BatimentId = _bat.BatimentId.ToString();
                                    batModel.Rec = _bat.Qrec;
                                    batModel.Rgph = _bat.Qrgph;
                                    batModel.SdeId = _bat.SdeId;
                                    frm_view_ce view = new frm_view_ce(batModel);
                                    Utilities.showControl(view, grd_details);
                                }
                            }
                        }
                        else if (currentContainer.DataContext.ToString() == Constant.DATACONTEXT_LOGEMENTVIEWMODEL)
                        {
                            _logement = currentContainer.DataContext as LogementViewModel;
                            frm_view_ce fr_logement = new frm_view_ce(_logement.Logement, _logement.Logement.SdeId);
                            Utilities.showControl(fr_logement, grd_details);
                        }
                        else if (currentContainer.DataContext.ToString() == Constant.DATACONTEXT_MENAGEVIEWMODEL)
                        {
                            _menage = currentContainer.DataContext as MenageViewModel;
                            frm_view_ce fr_menage = new frm_view_ce(_menage.Model, _menage.NumSde);
                            Utilities.showControl(fr_menage, grd_details);
                        }
                        else if (currentContainer.DataContext.ToString() == Constant.DATACONTEXT_MENAGEDETAILSVIEWMODEL)
                        {
                            _menageDetails = currentContainer.DataContext as MenageDetailsViewModel;
                            if (_menageDetails.Menage.Type == Constant.CODE_TYPE_EMIGRE)
                            {
                                frm_view_ce frm_emigre = new frm_view_ce(_menageDetails.Menage, _menageDetails.Menage.SdeId);
                                Utilities.showControl(frm_emigre, grd_details);
                            }
                            else if (_menageDetails.Menage.Type == Constant.CODE_TYPE_DECES)
                            {
                                frm_view_ce frm_deces = new frm_view_ce(_menageDetails.Menage, _menageDetails.Menage.SdeId);
                                Utilities.showControl(frm_deces, grd_details);
                            }
                            else
                            {
                                frm_view_ce frm_individu = new frm_view_ce(_menageDetails.Menage, _menageDetails.Menage.SdeId);
                                Utilities.showControl(frm_individu, grd_details);
                            }
                            name = _menageDetails.MenageDetailsId;
                        }
                    currentContainer = getParent(currentContainer);
                }
            }
            catch (MessageException ex)
            {
                MessageBox.Show(ex.Message, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                log.Info("<>===Exception:" + ex.Message);
                log.Info("<>===Exception:" + ex.StackTrace);
            }
        }
        #endregion

        #region EVENTS ON CONTROL
        public void TreeViewItem_Expanded(object sender, RoutedEventArgs e)
        {

            TreeViewItem currentContainer = e.OriginalSource as TreeViewItem;
            SdeViewModel _sde;
            if (currentContainer.DataContext.ToString() == Constant.DATACONTEXT_SDEVIEWMODEL)
            {
                _sde = currentContainer.DataContext as SdeViewModel;
                if (_sde.AllreadyLoad == false)
                {
                    _sde.IsLoading = true;
                }
            }
        }
        private TreeViewItem getParent(TreeViewItem container)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(container);
            while (parent != null && (parent is TreeViewItem))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            return parent as TreeViewItem;
        }

        private void scrl_view_tree_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private void scrl_bar_1_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }


        #endregion

        #region Right click on tree level BATIMENT
        private void cm_refresh_Click_1(object sender, RoutedEventArgs e)
        {
            SdeModel[] sdeModel = mdfService.getAllSde();
            model = new TreeViewModel(sdeModel);
            trg_main.ItemsSource = model.Sdes;
        }
        #endregion

        #region SELECTION ALEATOIRE CONTRE-ENQUETE
        private void cm_batiments_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.GetTreeviewItem != null)
                {

                    SdeViewModel _sde = GetTreeviewItem.DataContext as SdeViewModel;

                    List<BatimentModel> listOfBat = service_ce.getBatimentVideInCE(_sde.SdeName);
                    if (listOfBat.Count != 0)
                    {
                        MessageBox.Show("Ou gentan chwazi batiman pou SDE sa a deja.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                    else
                    {
                        service_ce = new ContreEnqueteService(_sde.SdeName);
                        List<BatimentModel> listOfBat1 = service_ce.getBatimentVide();
                        if (Utils.IsNotNull(listOfBat1))
                        {
                            foreach (BatimentModel batItem in listOfBat1)
                            {
                                ContreEnqueteModel ce = new ContreEnqueteModel();
                                ce.BatimentId = Convert.ToInt32(batItem.BatimentId);
                                ce.SdeId = batItem.SdeId;
                                ce.NomSuperviseur = Users.users.Nom;
                                ce.PrenomSuperviseur = Users.users.Prenom;
                                ce.ModelTirage = 1;
                                ce.CodeDistrict = "007-098-789";
                                ce.TypeContreEnquete = Convert.ToByte(Constant.TypeContrEnquete.BatimentVide);
                                ce.DateDebut = DateTime.Now.ToString();
                                ce.Statut = (int)Constant.StatutContreEnquete.Non_Termine;
                                service_ce.saveContreEnquete(ce);
                                BatimentCEModel bat = new BatimentCEModel();
                                bat.BatimentId = Convert.ToInt32(batItem.BatimentId);
                                bat.SdeId = batItem.SdeId;
                                bat.Qrec = batItem.Qrec;
                                bat.Qrgph = batItem.Qrgph;
                                bat.Qhabitation = batItem.Qhabitation;
                                service_ce.saveBatiment(bat);
                            }
                            MessageBox.Show("Batiman yo anrejistre avek siksè.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Pa gen batiman ki vid nan SDE sa a.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                    }

                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult result in ex.EntityValidationErrors)
                {
                    string entityName = result.Entry.Entity.GetType().Name;
                    foreach (DbValidationError error in result.ValidationErrors)
                    {
                        log.Info("Error: Entity Name:" + entityName + ":Property:" + error.PropertyName + " Message:" + error.ErrorMessage);
                    }
                }
            }
            catch (MessageException ex)
            {
                MessageBox.Show(ex.Message, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cm_batiments_lojman_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (GetTreeviewItem.DataContext == null)
                {
                    return;
                }
                SdeViewModel _sde = GetTreeviewItem.DataContext as SdeViewModel;
                busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = true));
                busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.BusyContent = "N'ap chèche lojman kolektif."));
                waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = true));
                BatimentModel batiment = new BatimentModel();
                bckw = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
                bckw.DoWork += (s, args) =>
                {
                    for (int i = 0; i <= 100; i += 10)
                    {
                        Thread.Sleep(1000);
                        bckw.ReportProgress(i);
                    }
                };
                bckw.ProgressChanged += (s, args) =>
                {
                    if (args.ProgressPercentage == 20)
                    {
                        service_ce = new ContreEnqueteService(_sde.SdeName);
                        batiment = service_ce.getBatimentWithLogementC();
                    }
                    if (args.ProgressPercentage == 30)
                    {
                        if (Convert.ToInt32(batiment.BatimentId) != 0)
                        {
                            if (service_ce.isBatimentExist(Convert.ToInt32(batiment.BatimentId), batiment.SdeId) == false)
                            {
                                BatimentCEModel b = new BatimentCEModel();
                                ContreEnqueteModel ce = new ContreEnqueteModel();
                                ce.BatimentId = Convert.ToInt32(batiment.BatimentId);
                                ce.SdeId = batiment.SdeId;
                                ce.NomSuperviseur = Users.users.Nom;
                                ce.PrenomSuperviseur = Users.users.Prenom;
                                ce.ModelTirage = 1;
                                ce.TypeContreEnquete = Convert.ToByte(Constant.TypeContrEnquete.LogementCollectif);
                                ce.DateDebut = DateTime.Now.ToString();
                                ce.Statut = (int)Constant.StatutContreEnquete.Non_Termine;
                                service_ce.saveContreEnquete(ce);
                                b.BatimentId = Convert.ToInt32(batiment.BatimentId);
                                b.SdeId = batiment.SdeId;
                                b.Qrgph = batiment.Qrgph;
                                b.Qrec = batiment.Qrec;
                                b.Qadresse = batiment.Qadresse;
                                b.Qhabitation = batiment.Qhabitation;
                                b.Qlocalite = batiment.Qlocalite;
                                service_ce.saveBatiment(b);
                                if (batiment.Logement.Count() != 0)
                                {
                                    foreach (LogementModel logement in batiment.Logement)
                                    {
                                        LogementCEModel log = new LogementCEModel();
                                        log.BatimentId = logement.BatimentId;
                                        log.SdeId = logement.SdeId;
                                        log.LogeId = logement.LogeId;
                                        log.QlCategLogement = Convert.ToByte(logement.QlCategLogement);
                                        log.Qlin2StatutOccupation = Convert.ToByte(logement.Qlin2StatutOccupation);
                                        log.Qlin1NumeroOrdre = Convert.ToByte(logement.Qlin1NumeroOrdre);
                                        service_ce.saveLogementCE(log);
                                    }
                                }
                                MessageBox.Show("batiman yo anregistre avek siske.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                                waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                            }
                        }
                        else
                        {
                            MessageBox.Show("Pa gen batiman ki gen  lojman kolektif nan SDE sa a.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                            waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                            busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = false));
                        }
                    }

                };
                bckw.RunWorkerCompleted += (s, args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show("Gen yon erè pandan operasyon an.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = false));
                    waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                };
                bckw.RunWorkerAsync();
            }
            catch (MessageException ex)
            {
                MessageBox.Show(ex.Message, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cm_batiments_lojman_vid_Click(object sender, RoutedEventArgs e)
        {

            busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = true));
            busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.BusyContent = "N'ap chèche batiman ki gen lojman ki vid."));
            waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = true));
            SdeViewModel _sde = GetTreeviewItem.DataContext as SdeViewModel;
            List<BatimentModel> listOfBat = new List<BatimentModel>();
            bckw = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
            bckw.DoWork += (s, args) =>
            {
                for (int i = 0; i <= 100; i += 10)
                {
                    Thread.Sleep(1000);
                    bckw.ReportProgress(i);
                }
            };
            bckw.ProgressChanged += (s, args) =>
            {
                if (args.ProgressPercentage == 20)
                {
                    busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.BusyContent = "Rechèch la ap fèt"));
                    service_ce = new ContreEnqueteService(_sde.SdeName);
                    listOfBat = service_ce.searchBatimentWithLogementIndVide();
                }
                if (args.ProgressPercentage == 30)
                {
                    bool resultat = false;
                    try
                    {
                        foreach (BatimentModel batiment in listOfBat)
                        {
                            if (Convert.ToInt32(batiment.BatimentId) != 0)
                            {
                                if (service_ce.isBatimentExist(Convert.ToInt32(batiment.BatimentId), batiment.SdeId) == false)
                                {
                                    BatimentCEModel b = new BatimentCEModel();
                                    ContreEnqueteModel ce = new ContreEnqueteModel();
                                    ce.BatimentId = Convert.ToInt32(batiment.BatimentId);
                                    ce.SdeId = batiment.SdeId;
                                    ce.NomSuperviseur = Users.users.Nom;
                                    ce.PrenomSuperviseur = Users.users.Prenom;
                                    ce.ModelTirage = 1;
                                    ce.TypeContreEnquete = Convert.ToByte(Constant.TypeContrEnquete.LogementInvididuelVide);
                                    ce.DateDebut = DateTime.Now.ToString();
                                    ce.Statut = (int)Constant.StatutContreEnquete.Non_Termine;
                                    service_ce.saveContreEnquete(ce);
                                    b.BatimentId = Convert.ToInt32(batiment.BatimentId);
                                    b.SdeId = batiment.SdeId;
                                    b.Qrgph = batiment.Qrgph;
                                    b.Qrec = batiment.Qrec;
                                    b.Qadresse = batiment.Qadresse;
                                    b.Qhabitation = batiment.Qhabitation;
                                    b.Qlocalite = batiment.Qlocalite;
                                    service_ce.saveBatiment(b);
                                    if (batiment.Logement.Count() != 0)
                                    {
                                        foreach (LogementModel logement in batiment.Logement)
                                        {
                                            LogementCEModel log = new LogementCEModel();
                                            log.BatimentId = Convert.ToInt32(batiment.BatimentId);
                                            log.SdeId = batiment.SdeId;
                                            log.LogeId = logement.LogeId;
                                            log.QlCategLogement = Convert.ToByte(logement.QlCategLogement);
                                            log.Qlin2StatutOccupation = Convert.ToByte(logement.Qlin2StatutOccupation);
                                            log.Qlin1NumeroOrdre = Convert.ToByte(logement.Qlin1NumeroOrdre.ToString());
                                            service_ce.saveLogementCE(log);
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Ou gentan chwazi batiman sa yo deja.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                                    busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = false));
                                    waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                                    resultat = false;
                                    break;
                                }
                            }
                            resultat = true;
                        }

                    }
                    catch (MessageException ex)
                    {
                        MessageBox.Show(ex.Message, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                    if (resultat == true)
                    {
                        MessageBox.Show("Batiman yo anregistre avek siske.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                        busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = false));
                        waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                    }
                }
            };
            bckw.RunWorkerCompleted += (s, args) =>
            {
                if (args.Error != null)
                {
                    MessageBox.Show("Gen yon erè pandan operasyon an.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = false));
                waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
            };
            bckw.RunWorkerAsync();
        }

        private void cm_batiments_menaj_Click(object sender, RoutedEventArgs e)
        {
            bool resultat = false;
            busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = true));
            busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.BusyContent = "N'ap chèche batiman ki gen lojman ak menaj ladanl."));
            waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = true));
            if (GetTreeviewItem == null)
            {
                MessageBox.Show("Ou dwe chwazi yon SDE.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = false));
                waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
            }
            else
            {
                SdeViewModel _sde = GetTreeviewItem.DataContext as SdeViewModel;
                List<BatimentModel> listOfBat = new List<BatimentModel>();
                bckw = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
                bckw.DoWork += (s, args) =>
                {
                    for (int i = 0; i <= 100; i += 10)
                    {
                        Thread.Sleep(1000);
                        bckw.ReportProgress(i);
                    }
                };
                bckw.ProgressChanged += (s, args) =>
                {
                    if (args.ProgressPercentage == 20)
                    {
                        busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.BusyContent = "Rechèch la ap fèt"));
                        service_ce = new ContreEnqueteService(_sde.SdeName);
                        listOfBat = service_ce.getBatimentWithLogInd();
                    }
                    if (args.ProgressPercentage == 30)
                    {
                        try
                        {
                            foreach (BatimentModel batiment in listOfBat)
                            {
                                if (Convert.ToInt32(batiment.BatimentId) != 0)
                                {
                                    if (service_ce.isBatimentExist(Convert.ToInt32(batiment.BatimentId), batiment.SdeId) == false)
                                    {
                                        BatimentCEModel b = new BatimentCEModel();
                                        ContreEnqueteModel ce = new ContreEnqueteModel();
                                        ce.BatimentId = Convert.ToInt32(batiment.BatimentId);
                                        ce.SdeId = batiment.SdeId;
                                        ce.NomSuperviseur = Users.users.Nom;
                                        ce.PrenomSuperviseur = Users.users.Prenom;
                                        ce.ModelTirage = 1;
                                        ce.TypeContreEnquete = Convert.ToByte(Constant.TypeContrEnquete.LogementIndividuelMenage);
                                        ce.DateDebut = DateTime.Now.ToString();
                                        ce.Statut = (int)Constant.StatutContreEnquete.Non_Termine;
                                        service_ce.saveContreEnquete(ce);
                                        b.BatimentId = Convert.ToInt32(batiment.BatimentId);
                                        b.SdeId = batiment.SdeId;
                                        b.Qrgph = batiment.Qrgph;
                                        b.Qrec = batiment.Qrec;
                                        b.Qadresse = batiment.Qadresse;
                                        b.Qhabitation = batiment.Qhabitation;
                                        b.Qlocalite = batiment.Qlocalite;
                                        service_ce.saveBatiment(b);
                                        if (batiment.Logement.Count() != 0)
                                        {
                                            foreach (LogementModel logement in batiment.Logement)
                                            {
                                                LogementCEModel log = new LogementCEModel();
                                                log.BatimentId = Convert.ToInt32(batiment.BatimentId);
                                                log.SdeId = batiment.SdeId;
                                                log.LogeId = logement.LogeId;
                                                log.QlCategLogement = Convert.ToByte(logement.QlCategLogement);
                                                log.Qlin2StatutOccupation = Convert.ToByte(logement.Qlin2StatutOccupation);
                                                log.Qlin1NumeroOrdre = Convert.ToByte(logement.Qlin1NumeroOrdre);
                                                service_ce.saveLogementCE(log);
                                                if (logement.Menages.Count() != 0)
                                                {
                                                    foreach (MenageModel menages in logement.Menages)
                                                    {
                                                        MenageCEModel _men = new MenageCEModel();
                                                        _men.BatimentId = menages.BatimentId;
                                                        _men.SdeId = menages.SdeId;
                                                        _men.Qm1NoOrdre = Convert.ToByte(menages.Qm1NoOrdre);
                                                        _men.MenageId = menages.MenageId;
                                                        _men.LogeId = menages.LogeId;
                                                        service_ce.saveMenageCE(_men);
                                                        if (menages.Desces.Count() >= 1)
                                                        {
                                                            foreach (DecesModel _deces in menages.Desces)
                                                            {
                                                                DecesCEModel _dec = new DecesCEModel();
                                                                _dec.BatimentId = _deces.BatimentId;
                                                                _dec.LogeId = _deces.LogeId;
                                                                _dec.MenageId = _deces.MenageId;
                                                                _dec.SdeId = menages.SdeId;
                                                                _dec.Qd2NoOrdre = Convert.ToByte(_deces.Qd2NoOrdre);
                                                                _dec.DecesId = _deces.DecesId;
                                                                service_ce.saveDecesCE(_dec);
                                                            }
                                                        }
                                                        if (menages.Individus.Count() >= 1)
                                                        {
                                                            foreach (IndividuModel _ind in menages.Individus)
                                                            {
                                                                IndividuCEModel ind = new IndividuCEModel();
                                                                ind.BatimentId = _ind.BatimentId;
                                                                ind.LogeId = _ind.LogeId;
                                                                ind.SdeId = menages.SdeId;
                                                                ind.MenageId = _ind.MenageId;
                                                                ind.Qp1NoOrdre = Convert.ToByte(_ind.Q1NoOrdre);
                                                                ind.IndividuId = _ind.IndividuId;
                                                                ind.Q2Nom = _ind.Qp2BNom;
                                                                ind.Q3Prenom = _ind.Qp2APrenom;
                                                                ind.Q5bAge = Convert.ToByte(_ind.Qp5bAge);
                                                                ind.Q2Nom = _ind.Qp2BNom;
                                                                ind.Q3Prenom = _ind.Qp2APrenom;
                                                                ind.Q6LienDeParente = Convert.ToByte(_ind.Qp3LienDeParente);
                                                                service_ce.saveIndividuCE(ind);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Ou gentan chwazi batiman sa yo.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                                        busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = false));
                                        waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                                        resultat = false;
                                        break;
                                    }
                                }
                                //resultat = false;
                            }
                            resultat = true;
                        }
                        catch (MessageException ex)
                        {
                            resultat = false;
                            log.Info("Error:" + ex.Message);
                        }
                        catch (Exception ex)
                        {
                            resultat = false;
                            log.Info("Error:" + ex.Message);
                        }
                        if (resultat == true)
                        {
                            MessageBox.Show("Batiman yo anregistre avek siske.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                            busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = false));
                            waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                        }
                        else
                        {
                            MessageBox.Show("Gen yon erè pandan operasyon an.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                            busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = false));
                            waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                        }

                    }
                };
                bckw.RunWorkerCompleted += (s, args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show("Gen yon erè pandan operasyon an.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = false));
                    waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                };
                bckw.RunWorkerAsync();
            }
            //}
            //catch (MessageException ex)
            //{
            //    MessageBox.Show(ex.Message, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            //}


        }
        #endregion

        #region GESTION DES RETOURS
        private void cm_open_batiments_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.GetTreeviewItem != null)
                {
                    BatimentViewModel batVM = GetTreeviewItem.DataContext as BatimentViewModel;
                    BatimentModel batiment = new BatimentModel();
                    batiment.BatimentId = batVM.Batiment.BatimentId;
                    batiment.SdeId = batVM.SdeName;
                    batiment.Statut = (int)Constant.StatutModule.MalRempli;
                    batiment.IsValidated = Convert.ToBoolean(Constant.STATUS_NOT_VALIDATED_0);
                    batiment.IsFieldAllFilled = false;
                    //On charge le popup
                    showDialogForRaison();
                    //
                    bool result = sw.changeStatus<BatimentModel>(batiment, batiment.SdeId);
                    if (result == true)
                    {
                        //On garde une historique du retour;
                        confService = new ConfigurationService();
                        RetourModel retour = new RetourModel();
                        retour.BatimentId = batiment.BatimentId;
                        retour.SdeId = batiment.SdeId;
                        retour.DateRetour = DateTime.Now.ToString();
                        retour.Raison = raison;
                        retour.Statut = Constant.STATUT_NON_EFFECTUE;
                        bool save = confService.saveRetour(retour);
                        //
                        MessageBox.Show(Constant.MSG_MODULE_KI_MAL_RANPLI, "" + Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                        batVM.Status = true;
                        batVM.ImageSource = Constant.GetStringValue(Constant.ImagePath.MalRempli);
                        batVM.Tip = Constant.GetStringValue(Constant.ToolTipMessage.MalRempli);
                    }
                }
                else
                {
                    MessageBox.Show("Chwazi yon batiman.", "" + Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (MessageException ex)
            {
                MessageBox.Show(ex.Message, "" + Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {

            }
        }

        void popup_Closing(object sender, CancelEventArgs e)
        {
            string rasion = (sender as frm_raison_popup).Raison;
        }

        private void cm_open_lojman_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.GetTreeviewItem != null)
                {
                    LogementViewModel logVM = GetTreeviewItem.DataContext as LogementViewModel;
                    LogementModel lgmntModel = new LogementModel();
                    lgmntModel.LogeId = logVM.LogementId;
                    lgmntModel.BatimentId = logVM.BatimentId;
                    lgmntModel.SdeId = logVM.NumSde;
                    lgmntModel.Statut = (int)Constant.StatutModule.MalRempli;
                    lgmntModel.IsFieldAllFilled = false;
                    //On charge le popup
                    showDialogForRaison();
                    //
                    bool result = sw.changeStatus<LogementModel>(lgmntModel, lgmntModel.SdeId);
                    if (result == true)
                    {
                        //On garde une historique du retour;
                        confService = new ConfigurationService();
                        RetourModel retour = new RetourModel();
                        retour.BatimentId = lgmntModel.BatimentId;
                        retour.LogementId = lgmntModel.LogeId;
                        retour.SdeId = lgmntModel.SdeId;
                        retour.DateRetour = DateTime.Now.ToString();
                        retour.Raison = raison;
                        retour.Statut = Constant.STATUT_NON_EFFECTUE;
                        bool save = confService.saveRetour(retour);
                        //
                        MessageBox.Show(Constant.MSG_MODULE_KI_MAL_RANPLI, "" + Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                        logVM.Status = true;
                        logVM.Tip = Constant.GetStringValue(Constant.ToolTipMessage.MalRempli);
                        logVM.ImageSource = Constant.GetStringValue(Constant.ImagePath.MalRempli);
                    }
                }
                else
                {
                    MessageBox.Show("Chwazi yon lojman.", "" + Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (MessageException ex)
            {
                MessageBox.Show(ex.Message, "" + Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {

            }
        }

        private void cm_open_menaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.GetTreeviewItem != null)
                {
                    MenageViewModel _menage = this.GetTreeviewItem.DataContext as MenageViewModel;
                    MenageModel menageModel = new MenageModel();
                    menageModel.MenageId = _menage.MenageId;
                    menageModel.SdeId = _menage.NumSde;
                    menageModel.LogeId = _menage.LogementId;
                    menageModel.BatimentId = _menage.BatimentId;
                    menageModel.Statut = Convert.ToByte(Constant.STATUT_MODULE_KI_MAL_RANPLI_2);
                    menageModel.IsFieldAllFilled = false;
                    //On charge le popup pour dire pour quelle raison on veut faire le retour
                    showDialogForRaison();
                    //
                    bool result = sw.changeStatus<MenageModel>(menageModel, menageModel.SdeId);
                    if (result == true)
                    {
                        //On garde une historique du retour;
                        confService = new ConfigurationService();
                        RetourModel retour = new RetourModel();
                        retour.BatimentId = menageModel.BatimentId;
                        retour.LogementId = menageModel.LogeId;
                        retour.MenageId = menageModel.MenageId;
                        retour.SdeId = menageModel.SdeId;
                        retour.DateRetour = DateTime.Now.ToString();
                        retour.Raison = raison;
                        retour.Statut = Constant.STATUT_NON_EFFECTUE;
                        bool save = confService.saveRetour(retour);
                        //
                        MessageBox.Show(Constant.MSG_MODULE_KI_MAL_RANPLI, "" + Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                        _menage.Status = true;
                        _menage.Tip = Constant.GetStringValue(Constant.ToolTipMessage.MalRempli);
                        _menage.ImageSource = Constant.GetStringValue(Constant.ImagePath.MalRempli);
                    }
                }
                else
                {
                    MessageBox.Show("Chwazi yon menaj.", "" + Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (MessageException ex)
            {
                MessageBox.Show(ex.Message, "" + Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "" + Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cm_open_details_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.GetTreeviewItem != null)
                {
                    bool result = false;
                    MenageDetailsViewModel menageDetails = this.GetTreeviewItem.DataContext as MenageDetailsViewModel;
                    if (menageDetails.Type == Constant.CODE_TYPE_DECES)
                    {
                        DecesModel model = new DecesModel();
                        model.DecesId = Convert.ToInt32(menageDetails.MenageDetailsId);
                        model.BatimentId = menageDetails.Menage.BatimentId;
                        model.MenageId = menageDetails.Menage.MenageId;
                        model.LogeId = menageDetails.Menage.LogementId;
                        model.SdeId = menageDetails.Menage.SdeId;
                        model.Statut = Convert.ToByte(Constant.STATUT_MODULE_KI_MAL_RANPLI_2);
                        model.IsFieldAllFilled = false;
                        //On charge le popup pour dire pour quelle raison on veut faire le retour
                        showDialogForRaison();
                        //
                        result = sw.changeStatus<DecesModel>(model, model.SdeId);
                        //On garde une historique du retour;
                        confService = new ConfigurationService();
                        RetourModel retour = new RetourModel();
                        retour.BatimentId = model.BatimentId;
                        retour.LogementId = model.LogeId;
                        retour.MenageId = model.MenageId;
                        retour.IndividuId = model.DecesId;
                        retour.SdeId = model.SdeId;
                        retour.DateRetour = DateTime.Now.ToString();
                        retour.Raison = raison;
                        retour.Statut = Constant.STATUT_NON_EFFECTUE;
                        bool save = confService.saveRetour(retour);
                        //
                    }
                    if (menageDetails.Type == Constant.CODE_TYPE_EMIGRE)
                    {
                        EmigreModel model = new EmigreModel();
                        model.EmigreId = Convert.ToInt32(menageDetails.MenageDetailsId);
                        model.BatimentId = menageDetails.Menage.BatimentId;
                        model.MenageId = menageDetails.Menage.MenageId;
                        model.LogeId = menageDetails.Menage.LogementId;
                        model.SdeId = menageDetails.Menage.SdeId;
                        model.Statut = Convert.ToByte(Constant.STATUT_MODULE_KI_MAL_RANPLI_2);
                        model.IsFieldAllFilled = false;
                        //On charge le popup pour dire pour quelle raison on veut faire le retour
                        showDialogForRaison();
                        //
                        result = sw.changeStatus<EmigreModel>(model, model.SdeId);
                        //On garde une historique du retour;
                        confService = new ConfigurationService();
                        RetourModel retour = new RetourModel();
                        retour.BatimentId = model.BatimentId;
                        retour.LogementId = model.LogeId;
                        retour.MenageId = model.MenageId;
                        retour.IndividuId = model.EmigreId;
                        retour.SdeId = model.SdeId;
                        retour.DateRetour = DateTime.Now.ToString();
                        retour.Raison = raison;
                        retour.Statut = Constant.STATUT_NON_EFFECTUE;
                        bool save = confService.saveRetour(retour);
                        //
                    }
                    if (menageDetails.Type == Constant.CODE_TYPE_ENVDIVIDI)
                    {
                        IndividuModel model = new IndividuModel();
                        model.IndividuId = Convert.ToInt32(menageDetails.MenageDetailsId);
                        model.BatimentId = menageDetails.Menage.BatimentId;
                        model.MenageId = menageDetails.Menage.MenageId;
                        model.LogeId = menageDetails.Menage.LogementId;
                        model.SdeId = menageDetails.Menage.SdeId;
                        model.Statut = Convert.ToByte(Constant.STATUT_MODULE_KI_MAL_RANPLI_2);
                        model.IsFieldAllFilled = false;
                        //On charge le popup pour dire pour quelle raison on veut faire le retour
                        showDialogForRaison();
                        //
                        result = sw.changeStatus<IndividuModel>(model, model.SdeId);
                        //On garde une historique du retour;
                        confService = new ConfigurationService();
                        RetourModel retour = new RetourModel();
                        retour.BatimentId = model.BatimentId;
                        retour.LogementId = model.LogeId;
                        retour.MenageId = model.MenageId;
                        retour.IndividuId = model.IndividuId;
                        retour.SdeId = model.SdeId;
                        retour.DateRetour = DateTime.Now.ToString();
                        retour.Raison = raison;
                        retour.Statut = Constant.STATUT_NON_EFFECTUE;
                        bool save = confService.saveRetour(retour);
                        //
                    }
                    if (result == true)
                    {
                        MessageBox.Show(Constant.MSG_MODULE_KI_MAL_RANPLI, "" + Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                        menageDetails.Status = true;
                        menageDetails.Tip = Constant.GetStringValue(Constant.ToolTipMessage.MalRempli);
                        menageDetails.ImageSource = Constant.GetStringValue(Constant.ImagePath.MalRempli);
                    }
                }
                else
                {
                    MessageBox.Show("Chwazi yon eleman.", "" + Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (MessageException ex)
            {
                MessageBox.Show(ex.Message, "" + Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {

            }
        }

        #endregion

        #region VALIDATION
        private void cm_valid_batiments_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.GetTreeviewItem != null)
                {
                    BatimentViewModel batVM = GetTreeviewItem.DataContext as BatimentViewModel;
                    BatimentModel batiment = new BatimentModel();
                    batiment.BatimentId = batVM.Batiment.BatimentId;
                    batiment.SdeId = batVM.SdeName;
                    batiment.IsValidated = Convert.ToBoolean(Constant.STATUS_VALIDATED_1);
                    bool result = sw.validate<BatimentModel>(batiment, batiment.SdeId);
                    if (result == true)
                    {
                        MessageBox.Show(Constant.MSG_VALIDATION, "" + Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Chwazi yon eleman.", "" + Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (MessageException ex)
            {
                MessageBox.Show(ex.Message, "" + Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "" + Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        public void showDialogForRaison()
        {
            //On charge le popup pour dire pour quelle raison on veut faire le retour
            frm_raison_popup popup = new frm_raison_popup();
            popup.Closing += popup_Closing;
            if (popup.ShowDialog() == true)
            {
                raison = popup.Raison;
            }
            raison = popup.Raison;
            //
        }

        private void cm_transfert_Click(object sender, RoutedEventArgs e)
        {
            frm_pop_up_transfert popupTransfert = new frm_pop_up_transfert();
            popupTransfert.Closing += popupTransfert_Closing;
            if (popupTransfert.ShowDialog() == true)
            {

            }
        }

        void popupTransfert_Closing(object sender, CancelEventArgs e)
        {
            
        }
    }

}
