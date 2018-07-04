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
using System.IO;

namespace Ht.Ihsil.Rgph.App.Superviseur.views
{
    /// <summary>
    /// Logique d'interaction pour frm_visualisation.xaml
    /// </summary>
    public partial class frm_visualisation : UserControl
    {
        #region DECLARATION

        Logger log;
        private static string MAIN_DATABASE_PATH = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\RgphData\Data\Databases\";
        TreeViewModel model;
        SqliteDataReaderService service = null;
        ContreEnqueteService service_ce = null;
        MdfService mdfService = null;
        ConfigurationService confService = null;
        BackgroundWorker bckw;
        ISqliteDataWriter sw;
        TreeViewItem getTreeviewItem;
        string raison = "";
        bool isButtonValidateClick = false;
        string pathDefaultConfigurationFile = AppDomain.CurrentDomain.BaseDirectory + @"App_data\";
        string file = "";
        XmlUtils configuration = null;
        ISqliteReader reader = null;
        ISqliteDataWriter writer = null;
        string sde;
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
            file = pathDefaultConfigurationFile + "contreenquete.xml";
            configuration = new XmlUtils(file);

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
                                                batModel.Adresse = _bat.Qadresse;
                                                batModel.Localite = _bat.Qlocalite;
                                                batModel.Habitation = _bat.Qhabitation;
                                                batModel.DistrictId = _bat.District;
                                                List<IndividuCEModel> individus = ModelMapper.MapToListIndividuCEModel(service_ce.daoCE.searchAllIndividuCE(_bat.BatimentId, _log.LogeId, _bat.SdeId, _men.MenageId));
                                                foreach (IndividuCEModel _ind in individus)
                                                {
                                                    if (_ind.Q3LienDeParente.GetValueOrDefault() == 1)
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
                                            batModel.Adresse = _bat.Qadresse;
                                            batModel.Localite = _bat.Qlocalite;
                                            batModel.Habitation = _bat.Qhabitation;
                                            batModel.DistrictId = _bat.District;
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
                                    batModel.Adresse = _bat.Qadresse;
                                    batModel.Localite = _bat.Qlocalite;
                                    batModel.Habitation = _bat.Qhabitation;
                                    batModel.DistrictId = _bat.District;
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
                                ce.CodeDistrict = batItem.DistrictId;
                                ce.TypeContreEnquete = Convert.ToByte(Constant.TypeContrEnquete.BatimentVide);
                                ce.DateDebut = DateTime.Now.ToString();
                                ce.Statut = (int)Constant.StatutContreEnquete.Selectionee;
                                //ce.CodeSuperviseur = Users.users.CodeUtilisateur;
                                service_ce.saveContreEnquete(ce);
                                BatimentCEModel bat = new BatimentCEModel();
                                bat.BatimentId = Convert.ToInt32(batItem.BatimentId);
                                bat.SdeId = batItem.SdeId;
                                bat.Qrec = batItem.Qrec;
                                bat.Qrgph = batItem.Qrgph;
                                bat.Qhabitation = batItem.Qhabitation;
                                bat.DeptId = batItem.DeptId;
                                bat.District = batItem.DistrictId;
                                bat.ComId = batItem.ComId;
                                bat.VqseId = batItem.VqseId;
                                bat.Qhabitation = batItem.Qhabitation;
                                bat.Qlocalite = batItem.Qlocalite;
                                bat.Qadresse = batItem.Qadresse;
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
                    MessageBox.Show(Constant.MSG_NOT_CLICK, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
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
                        service = new SqliteDataReaderService(Utilities.getConnectionString(MAIN_DATABASE_PATH, _sde.SdeName));
                        batiment = service_ce.getBatimentWithLogementC();
                    }
                    if (args.ProgressPercentage == 30)
                    {
                        if (Convert.ToInt32(batiment.BatimentId) != 0)
                        {
                            if (service_ce.isBatimentExist(Convert.ToInt32(batiment.BatimentId), batiment.SdeId) == false)
                            {
                                BatimentCEModel b = new BatimentCEModel();
                                b.BatimentId = Convert.ToInt32(batiment.BatimentId);
                                b.SdeId = batiment.SdeId;
                                b.Qrgph = batiment.Qrgph;
                                b.Qrec = batiment.Qrec;
                                b.Qadresse = batiment.Qadresse;
                                b.Qhabitation = batiment.Qhabitation;
                                b.Qlocalite = batiment.Qlocalite;
                                b.DeptId = batiment.DeptId;
                                b.District = batiment.DistrictId;
                                b.ComId = batiment.ComId;
                                b.VqseId = batiment.VqseId;
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
                                        //Creation de la contreenquete
                                        ContreEnqueteModel ce = new ContreEnqueteModel();
                                        ce.BatimentId = Convert.ToInt32(batiment.BatimentId);
                                        ce.LogeId = log.LogeId;
                                        ce.SdeId = batiment.SdeId;
                                        ce.NomSuperviseur = Users.users.Nom;
                                        ce.PrenomSuperviseur = Users.users.Prenom;
                                        ce.ModelTirage = 1;
                                        ce.CodeDistrict = batiment.DistrictId;
                                        ce.TypeContreEnquete = Convert.ToByte(Constant.TypeContrEnquete.LogementCollectif);
                                        ce.DateDebut = DateTime.Now.ToString();
                                        //ce.Termine = (int)Constant.StatutContreEnquete.Non_Effectue;
                                        ce.Statut = (int)Constant.StatutContreEnquete.Selectionee;
                                        ce = service_ce.saveContreEnquete(ce);
                                        //Si le logement contient des individus
                                        if (logement.QlcTotalIndividus != 0)
                                        {
                                            List<IndividuModel> listOfInds = service.Sr.GetIndividuByLoge(logement.LogeId);
                                            if (listOfInds.Count != 0)
                                            {
                                                foreach (IndividuModel ind in listOfInds)
                                                {
                                                    IndividuCEModel indCe = new IndividuCEModel();
                                                    indCe.BatimentId = ind.BatimentId;
                                                    indCe.MenageId = ind.MenageId;
                                                    indCe.LogeId = ind.LogeId;
                                                    indCe.SdeId = ind.SdeId;
                                                    indCe.IndividuId = ind.IndividuId;
                                                    indCe.Qp1NoOrdre = ind.Q1NoOrdre;
                                                    indCe.Q2Nom = ind.Qp2BNom;
                                                    indCe.Q3Prenom = ind.Qp2APrenom;
                                                    indCe.Q3LienDeParente = ind.Qp3LienDeParente;
                                                    service_ce.saveIndividuCE(indCe);
                                                }
                                            }
                                        }
                                    }
                                }
                                MessageBox.Show("batiman yo anregistre avek siske.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                                waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                                busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = false));
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
                        if (listOfBat.Count < 3)
                        {
                            MessageBox.Show("Ou pa gen kantite batiman pou fè kont-ankèt sa a.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                            busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = false));
                            waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                            resultat = false;
                        }
                        else
                        {
                            foreach (BatimentModel batiment in listOfBat)
                            {
                                if (Convert.ToInt32(batiment.BatimentId) != 0)
                                {
                                    BatimentCEModel b = new BatimentCEModel();
                                    //ContreEnqueteModel ce = new ContreEnqueteModel();
                                    //ce.BatimentId = Convert.ToInt32(batiment.BatimentId);
                                    //ce.SdeId = batiment.SdeId;
                                    //ce.NomSuperviseur = Users.users.Nom;
                                    //ce.PrenomSuperviseur = Users.users.Prenom;
                                    //ce.ModelTirage = 1;
                                    //ce.TypeContreEnquete = Convert.ToByte(Constant.TypeContrEnquete.LogementInvididuelVide);
                                    //ce.DateDebut = DateTime.Now.ToString();
                                    //ce.Statut = (int)Constant.StatutContreEnquete.Non_Termine;
                                    //service_ce.saveContreEnquete(ce);
                                    b.BatimentId = Convert.ToInt32(batiment.BatimentId);
                                    b.SdeId = batiment.SdeId;
                                    b.Qrgph = batiment.Qrgph;
                                    b.Qrec = batiment.Qrec;
                                    b.Qhabitation = batiment.Qhabitation;
                                    b.Qlocalite = batiment.Qlocalite;
                                    b.DeptId = batiment.DeptId;
                                    b.District = batiment.DistrictId;
                                    b.ComId = batiment.ComId;
                                    b.VqseId = batiment.VqseId;
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
                                            //Creation de la contreenquete
                                            ContreEnqueteModel ce = new ContreEnqueteModel();
                                            ce.BatimentId = Convert.ToInt32(batiment.BatimentId);
                                            ce.LogeId = log.LogeId;
                                            ce.SdeId = batiment.SdeId;
                                            ce.NomSuperviseur = Users.users.Nom;
                                            ce.PrenomSuperviseur = Users.users.Prenom;
                                            ce.ModelTirage = 1;
                                            ce.CodeDistrict = batiment.DistrictId;
                                            ce.TypeContreEnquete = Convert.ToByte(Constant.TypeContrEnquete.LogementInvididuelVide);
                                            ce.DateDebut = DateTime.Now.ToString();
                                            //ce.Termine = (int)Constant.StatutContreEnquete.Selectionee;
                                            ce.Statut = (int)Constant.StatutContreEnquete.Selectionee;
                                            ce = service_ce.saveContreEnquete(ce);
                                        }
                                    }
                                }
                                resultat = true;
                            }
                        }
                    }
                    catch (MessageException ex)
                    {
                        MessageBox.Show(ex.Message, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                        busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = false));
                        waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                        busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = false));
                        waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));

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

        private void cm_batiments_lojman_occupe_occasionnellement_Click(object sender, RoutedEventArgs e)
        {
            busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = true));
            busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.BusyContent = "N'ap chèche batiman ki gen lojman ki okipe yon lè konsa."));
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
                    listOfBat = service_ce.readerService.get3BatimentWithLogementOccupesOccasionnellement();
                }
                if (args.ProgressPercentage == 30)
                {
                    bool resultat = false;
                    try
                    {
                        if (listOfBat.Count < 3)
                        {
                            throw new MessageException("Pa gen kantite batiman pou kont ankèt sa a.");
                        }
                        foreach (BatimentModel batiment in listOfBat)
                        {
                            if (Convert.ToInt32(batiment.BatimentId) != 0)
                            {
                                //if (service_ce.isBatimentExist(Convert.ToInt32(batiment.BatimentId), batiment.SdeId) == false)
                                //{
                                BatimentCEModel b = new BatimentCEModel();
                                //ContreEnqueteModel ce = new ContreEnqueteModel();
                                //ce.BatimentId = Convert.ToInt32(batiment.BatimentId);
                                //ce.SdeId = batiment.SdeId;
                                //ce.NomSuperviseur = Users.users.Nom;
                                //ce.PrenomSuperviseur = Users.users.Prenom;
                                //ce.ModelTirage = 1;
                                //ce.TypeContreEnquete = Convert.ToByte(Constant.TypeContrEnquete.LogementOccupantAbsent);
                                //ce.DateDebut = DateTime.Now.ToString();
                                //ce.Statut = (int)Constant.StatutContreEnquete.Non_Termine;
                                //service_ce.saveContreEnquete(ce);
                                b.BatimentId = Convert.ToInt32(batiment.BatimentId);
                                b.SdeId = batiment.SdeId;
                                b.Qrgph = batiment.Qrgph;
                                b.Qrec = batiment.Qrec;
                                b.Qhabitation = batiment.Qhabitation;
                                b.Qlocalite = batiment.Qlocalite;
                                b.DeptId = batiment.DeptId;
                                b.District = batiment.DistrictId;
                                b.ComId = batiment.ComId;
                                b.VqseId = batiment.VqseId;
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
                                        //Creation de la contreenquete
                                        ContreEnqueteModel ce = new ContreEnqueteModel();
                                        ce.BatimentId = Convert.ToInt32(batiment.BatimentId);
                                        ce.LogeId = log.LogeId;
                                        ce.SdeId = batiment.SdeId;
                                        ce.NomSuperviseur = Users.users.Nom;
                                        ce.PrenomSuperviseur = Users.users.Prenom;
                                        ce.ModelTirage = 1;
                                        ce.CodeDistrict = batiment.DistrictId;
                                        ce.TypeContreEnquete = Convert.ToByte(Constant.TypeContrEnquete.LogementOccupantAbsent);
                                        ce.DateDebut = DateTime.Now.ToString();
                                        //ce.Termine = (int)Constant.StatutContreEnquete.Selectionee;
                                        ce.Statut = (int)Constant.StatutContreEnquete.Selectionee;
                                        ce = service_ce.saveContreEnquete(ce);
                                    }
                                }
                                //}
                                //else
                                //{
                                //    MessageBox.Show("Ou gentan chwazi batiman sa yo deja.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                                //    busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = false));
                                //    waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                                //    resultat = false;
                                //    break;
                                //}
                            }
                            resultat = true;
                        }

                    }
                    catch (MessageException ex)
                    {
                        MessageBox.Show(ex.Message, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                        busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = false));
                        waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                        busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = false));
                        waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));

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

        #region BATIMAN KI GEN MENNAJ
        //private void cm_batiments_menaj_Click(object sender, RoutedEventArgs e)
        //{
        //    bool resultat = false;
        //    busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = true));
        //    busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.BusyContent = "N'ap chèche batiman ki gen lojman ak menaj ladanl."));
        //    waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = true));
        //    if (GetTreeviewItem == null)
        //    {
        //        MessageBox.Show("Ou dwe chwazi yon SDE.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
        //        busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = false));
        //        waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
        //    }
        //    else
        //    {
        //        SdeViewModel _sde = GetTreeviewItem.DataContext as SdeViewModel;
        //        List<BatimentModel> listOfBat = new List<BatimentModel>();
        //        bckw = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
        //        bckw.DoWork += (s, args) =>
        //        {
        //            for (int i = 0; i <= 100; i += 10)
        //            {
        //                Thread.Sleep(1000);
        //                bckw.ReportProgress(i);
        //            }
        //        };
        //        bckw.ProgressChanged += (s, args) =>
        //        {
        //            if (args.ProgressPercentage == 20)
        //            {
        //                busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.BusyContent = "Rechèch la ap fèt"));
        //                service_ce = new ContreEnqueteService(_sde.SdeName);
        //                listOfBat = service_ce.getBatimentWithLogInd();
        //            }
        //            if (args.ProgressPercentage == 30)
        //            {
        //                try
        //                {
        //                    if (listOfBat.Count < 3)
        //                    {
        //                        throw new MessageException("Pa gen ase batiman. Dwe gen pou pi piti 3 batiman.");
        //                    }
        //                    foreach (BatimentModel batiment in listOfBat)
        //                    {
        //                        if (Convert.ToInt32(batiment.BatimentId) != 0)
        //                        {
        //                            //if (service_ce.isBatimentExist(Convert.ToInt32(batiment.BatimentId), batiment.SdeId) == false)
        //                            //{
        //                                BatimentCEModel b = new BatimentCEModel();                                        
        //                                b.BatimentId = Convert.ToInt32(batiment.BatimentId);
        //                                b.SdeId = batiment.SdeId;
        //                                b.Qrgph = batiment.Qrgph;
        //                                b.Qrec = batiment.Qrec;
        //                                b.Qadresse = batiment.Qadresse;
        //                                b.Qhabitation = batiment.Qhabitation;
        //                                b.Qlocalite = batiment.Qlocalite;
        //                                service_ce.saveBatiment(b);
        //                                if (batiment.Logement.Count() != 0)
        //                                {
        //                                    foreach (LogementModel logement in batiment.Logement)
        //                                    {
        //                                        LogementCEModel log = new LogementCEModel();
        //                                        log.BatimentId = Convert.ToInt32(batiment.BatimentId);
        //                                        log.SdeId = batiment.SdeId;
        //                                        log.LogeId = logement.LogeId;
        //                                        log.QlCategLogement = Convert.ToInt32(logement.QlCategLogement);
        //                                        log.Qlin2StatutOccupation = Convert.ToInt32(logement.Qlin2StatutOccupation);
        //                                        log.Qlin1NumeroOrdre = Convert.ToInt32(logement.Qlin1NumeroOrdre);
        //                                        service_ce.saveLogementCE(log);
        //                                        if (logement.Menages.Count() != 0)
        //                                        {
        //                                            foreach (MenageModel menages in logement.Menages)
        //                                            {
        //                                                MenageCEModel _men = new MenageCEModel();
        //                                                _men.BatimentId = menages.BatimentId;
        //                                                _men.SdeId = menages.SdeId;
        //                                                _men.Qm1NoOrdre = Convert.ToInt32(menages.Qm1NoOrdre);
        //                                                _men.MenageId = menages.MenageId;
        //                                                _men.LogeId = menages.LogeId;
        //                                                service_ce.saveMenageCE(_men);
        //                                                //Creation de la contreenquete
        //                                                ContreEnqueteModel ce = new ContreEnqueteModel();
        //                                                ce.BatimentId = Convert.ToInt32(batiment.BatimentId);
        //                                                ce.LogeId = log.LogeId;
        //                                                ce.MenageId = menages.MenageId;
        //                                                ce.SdeId = batiment.SdeId;
        //                                                ce.NomSuperviseur = Users.users.Nom;
        //                                                ce.PrenomSuperviseur = Users.users.Prenom;
        //                                                ce.ModelTirage = 1;
        //                                                ce.CodeDistrict = batiment.DistrictId;
        //                                                ce.TypeContreEnquete = Convert.ToByte(Constant.TypeContrEnquete.BatimentVide);
        //                                                ce.DateDebut = DateTime.Now.ToString();
        //                                                ce.Termine = (int)Constant.StatutContreEnquete.Non_Termine;
        //                                                ce = service_ce.saveContreEnquete(ce);

        //                                                if (menages.Deces.Count() >= 1)
        //                                                {
        //                                                    foreach (DecesModel _deces in menages.Deces)
        //                                                    {
        //                                                        DecesCEModel _dec = new DecesCEModel();
        //                                                        _dec.BatimentId = _deces.BatimentId;
        //                                                        _dec.LogeId = _deces.LogeId;
        //                                                        _dec.MenageId = _deces.MenageId;
        //                                                        _dec.SdeId = menages.SdeId;
        //                                                        _dec.Qd2NoOrdre = Convert.ToInt32(_deces.Qd2NoOrdre);
        //                                                        _dec.DecesId = _deces.DecesId;
        //                                                        service_ce.saveDecesCE(_dec);
        //                                                    }
        //                                                }
        //                                                if (menages.Individus.Count() >= 1)
        //                                                {
        //                                                    foreach (IndividuModel _ind in menages.Individus)
        //                                                    {
        //                                                        IndividuCEModel ind = new IndividuCEModel();
        //                                                        ind.BatimentId = _ind.BatimentId;
        //                                                        ind.LogeId = _ind.LogeId;
        //                                                        ind.SdeId = menages.SdeId;
        //                                                        ind.MenageId = _ind.MenageId;
        //                                                        ind.Qp1NoOrdre = Convert.ToInt32(_ind.Q1NoOrdre);
        //                                                        ind.IndividuId = _ind.IndividuId;
        //                                                        ind.Q2Nom = _ind.Qp2BNom;
        //                                                        ind.Q3Prenom = _ind.Qp2APrenom;
        //                                                        ind.Q5bAge = Convert.ToInt32(_ind.Qp5bAge);
        //                                                        ind.Q2Nom = _ind.Qp2BNom;
        //                                                        ind.Q3Prenom = _ind.Qp2APrenom;
        //                                                        ind.Q3LienDeParente = Convert.ToInt32(_ind.Qp3LienDeParente);
        //                                                        service_ce.saveIndividuCE(ind);
        //                                                    }
        //                                                }
        //                                            }
        //                                        }
        //                                    }
        //                                }
        //                            //}
        //                            //else
        //                            //{
        //                            //    MessageBox.Show("Ou gentan chwazi batiman sa yo.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
        //                            //    busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = false));
        //                            //    waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
        //                            //    resultat = false;
        //                            //    break;
        //                            //}
        //                        }
        //                        //resultat = false;
        //                    }
        //                    resultat = true;

        //                }
        //                catch (MessageException ex)
        //                {
        //                    MessageBox.Show(ex.Message, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
        //                    busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = false));
        //                    waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
        //                }
        //                catch (Exception ex)
        //                {
        //                    resultat = false;
        //                    log.Info("Error:" + ex.Message);
        //                }
        //                if (resultat == true)
        //                {
        //                    MessageBox.Show("Batiman yo anregistre avek siske.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
        //                    busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = false));
        //                    waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
        //                }
        //                else
        //                {
        //                    MessageBox.Show("Gen yon erè pandan operasyon an.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
        //                    busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = false));
        //                    waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
        //                }

        //            }
        //        };
        //        bckw.RunWorkerCompleted += (s, args) =>
        //        {
        //            if (args.Error != null)
        //            {
        //                MessageBox.Show("Gen yon erè pandan operasyon an.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
        //            }
        //            busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = false));
        //            waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
        //        };
        //        bckw.RunWorkerAsync();
        //    }

        //}
        #endregion

        private void semen1_Click(object sender, RoutedEventArgs e)
        {
            contreEnqueteLogementIndividuel(1, "false");
        }
        public void contreEnqueteLogementIndividuel(int id, string statut)
        {
            bool resultat = false;
            List<BatimentModel> listOfBatimentNotInContreEnquete = new List<BatimentModel>();
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

                        //Verifier si les batiments sont deja selectionnes dans une contreenquete
                        foreach (BatimentModel batiment in listOfBat)
                        {
                            if (service_ce.isBatimentExist(Convert.ToInt32(batiment.BatimentId), batiment.SdeId) == false)
                            {
                                listOfBatimentNotInContreEnquete.Add(batiment);
                            }
                        }

                    }
                    if (args.ProgressPercentage == 30)
                    {
                        try
                        {
                            if (listOfBatimentNotInContreEnquete.Count < 3)
                            {
                                throw new MessageException("Pa gen ase batiman. Dwe gen pou pi piti 3 batiman.");
                            }
                            foreach (BatimentModel batiment in listOfBatimentNotInContreEnquete)
                            {
                                if (Convert.ToInt32(batiment.BatimentId) != 0)
                                {
                                    //if (service_ce.isBatimentExist(Convert.ToInt32(batiment.BatimentId), batiment.SdeId) == false)
                                    //{
                                    BatimentCEModel b = new BatimentCEModel();
                                    b.BatimentId = Convert.ToInt32(batiment.BatimentId);
                                    b.SdeId = Utilities.getSdeFormatWithDistrict(_sde.SdeName);
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
                                            log.SdeId = Utilities.getSdeFormatWithDistrict(_sde.SdeName);
                                            log.LogeId = logement.LogeId;
                                            log.QlCategLogement = Convert.ToInt32(logement.QlCategLogement);
                                            log.Qlin2StatutOccupation = Convert.ToInt32(logement.Qlin2StatutOccupation);
                                            log.Qlin1NumeroOrdre = Convert.ToInt32(logement.Qlin1NumeroOrdre);
                                            service_ce.saveLogementCE(log);
                                            if (logement.Menages.Count() != 0)
                                            {
                                                foreach (MenageModel menages in logement.Menages)
                                                {
                                                    MenageCEModel _men = new MenageCEModel();
                                                    _men.BatimentId = menages.BatimentId;
                                                    _men.SdeId = Utilities.getSdeFormatWithDistrict(_sde.SdeName);
                                                    _men.Qm1NoOrdre = Convert.ToInt32(menages.Qm1NoOrdre);
                                                    _men.MenageId = menages.MenageId;
                                                    _men.LogeId = menages.LogeId;
                                                    service_ce.saveMenageCE(_men);
                                                    //Creation de la contreenquete
                                                    ContreEnqueteModel ce = new ContreEnqueteModel();
                                                    ce.BatimentId = Convert.ToInt32(batiment.BatimentId);
                                                    ce.LogeId = log.LogeId;
                                                    ce.MenageId = menages.MenageId;
                                                    ce.SdeId = Utilities.getSdeFormatWithDistrict(_sde.SdeName);
                                                    ce.NomSuperviseur = Users.users.Nom;
                                                    ce.PrenomSuperviseur = Users.users.Prenom;
                                                    ce.ModelTirage = 1;
                                                    ce.CodeDistrict = batiment.DistrictId;
                                                    ce.Statut = 1;
                                                    ce.TypeContreEnquete = Convert.ToByte(Constant.TypeContrEnquete.LogementIndividuelMenage);
                                                    ce.DateDebut = DateTime.Now.ToString();
                                                    //ce.Termine = (int)Constant.StatutContreEnquete.Selectionee;
                                                    ce.Statut = (int)Constant.StatutContreEnquete.Selectionee;
                                                    ce = service_ce.saveContreEnquete(ce);

                                                    if (menages.Deces.Count() >= 1)
                                                    {
                                                        foreach (DecesModel _deces in menages.Deces)
                                                        {
                                                            DecesCEModel _dec = new DecesCEModel();
                                                            _dec.BatimentId = _deces.BatimentId;
                                                            _dec.LogeId = _deces.LogeId;
                                                            _dec.MenageId = _deces.MenageId;
                                                            _dec.SdeId = Utilities.getSdeFormatWithDistrict(_sde.SdeName);
                                                            _dec.Qd2NoOrdre = Convert.ToInt32(_deces.Qd2NoOrdre);
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
                                                            ind.SdeId = Utilities.getSdeFormatWithDistrict(_sde.SdeName);
                                                            ind.MenageId = _ind.MenageId;
                                                            ind.Qp1NoOrdre = Convert.ToInt32(_ind.Q1NoOrdre);
                                                            ind.IndividuId = _ind.IndividuId;
                                                            ind.Q2Nom = _ind.Qp2BNom;
                                                            ind.Q3Prenom = _ind.Qp2APrenom;
                                                            ind.Q5bAge = Convert.ToInt32(_ind.Qp5bAge);
                                                            ind.Q2Nom = _ind.Qp2BNom;
                                                            ind.Q3Prenom = _ind.Qp2APrenom;
                                                            ind.Q3LienDeParente = Convert.ToInt32(_ind.Qp3LienDeParente);
                                                            service_ce.saveIndividuCE(ind);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                }
                            }
                            resultat = true;
                            if (resultat == true)
                            {
                                configuration.updateSemaineContreEnquete(id, statut);
                                if (id == 1)
                                    _sde.Semaine1 = false;
                                if (id == 2)
                                    _sde.Semaine2 = false;
                                if (id == 3)
                                    _sde.Semaine3 = false;
                            }


                        }
                        catch (MessageException ex)
                        {
                            MessageBox.Show(ex.Message, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                            busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = false));
                            waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
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
        }

        private void semen2_Click(object sender, RoutedEventArgs e)
        {
            contreEnqueteLogementIndividuel(2, "false");
        }

        private void semen3_Click(object sender, RoutedEventArgs e)
        {
            contreEnqueteLogementIndividuel(3, "false");
        }
        #endregion

        #region GESTION DES RETOURS
        private void cm_open_batiments_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.GetTreeviewItem != null)
                {
                    showDialogForRaison();
                    if (raison != null && isButtonValidateClick == true)
                    {
                        BatimentViewModel batVM = GetTreeviewItem.DataContext as BatimentViewModel;
                        BatimentModel batiment = new BatimentModel();
                        batiment.BatimentId = batVM.Batiment.BatimentId;
                        batiment.SdeId = batVM.SdeName;
                        batiment.Statut = (int)Constant.StatutModule.MalRempli;
                        batiment.IsValidated = Convert.ToBoolean(Constant.STATUS_NOT_VALIDATED_0);
                        batiment.IsFieldAllFilled = false;
                        //On charge le popup

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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "" + Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
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
                    //On charge le popup
                    showDialogForRaison();
                    //
                    if (raison != null && isButtonValidateClick == true)
                    {
                        LogementViewModel logVM = GetTreeviewItem.DataContext as LogementViewModel;
                        LogementModel lgmntModel = new LogementModel();
                        lgmntModel.LogeId = logVM.LogementId;
                        lgmntModel.BatimentId = logVM.BatimentId;
                        lgmntModel.SdeId = logVM.NumSde;
                        lgmntModel.Statut = (int)Constant.StatutModule.MalRempli;
                        lgmntModel.IsFieldAllFilled = false;

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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "" + Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cm_open_menaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.GetTreeviewItem != null)
                {
                    //On charge le popup pour dire pour quelle raison on veut faire le retour
                    showDialogForRaison();
                    //
                    if (raison != null && isButtonValidateClick == true)
                    {
                        MenageViewModel _menage = this.GetTreeviewItem.DataContext as MenageViewModel;
                        MenageModel menageModel = new MenageModel();
                        menageModel.MenageId = _menage.MenageId;
                        menageModel.SdeId = _menage.NumSde;
                        menageModel.LogeId = _menage.LogementId;
                        menageModel.BatimentId = _menage.BatimentId;
                        menageModel.Statut = Convert.ToByte(Constant.STATUT_MODULE_KI_MAL_RANPLI_2);
                        menageModel.IsFieldAllFilled = false;

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
                    //On charge le popup pour dire pour quelle raison on veut faire le retour
                    showDialogForRaison();
                    //
                    if (raison != null && isButtonValidateClick == true)
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

        #region VALIDATION
        private void cm_valid_batiments_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.GetTreeviewItem != null)
                {
                    BatimentViewModel batVM = GetTreeviewItem.DataContext as BatimentViewModel;
                    if (batVM.Batiment.Statut == (int)Constant.STATUT_MODULE_KI_FINI_1)
                    {
                        BatimentModel batiment = new BatimentModel();
                        batiment.BatimentId = batVM.Batiment.BatimentId;
                        batiment.SdeId = batVM.SdeName;
                        batiment.IsValidated = Convert.ToBoolean(Constant.STATUS_VALIDATED_1);
                        bool result = sw.validate<BatimentModel>(batiment, batiment.SdeId);
                        if (result == true)
                        {
                            MessageBox.Show(Constant.MSG_VALIDATION, "" + Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                            batVM.Status = true;
                            batVM.ImageSource = Constant.GetStringValue(Constant.ImagePath.Valide);
                            batVM.Tip = Constant.GetStringValue(Constant.ToolTipMessage.Valide_deja);
                        }
                    }
                    else
                    {
                        MessageBox.Show(Constant.MSG_NOT_VALIDATE, "" + Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                else
                {
                    MessageBox.Show(Constant.MSG_NOT_CLICK, "" + Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
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
                isButtonValidateClick = popup.IsValidate;
            }
            raison = popup.Raison;
            isButtonValidateClick = popup.IsValidate;
            //
        }

        private void cm_transfert_Click(object sender, RoutedEventArgs e)
        {
            frm_pop_up_transfert popupTransfert = new frm_pop_up_transfert(Constant.TRANSFERT_PC);
            popupTransfert.Closing += popupTransfert_Closing;
            if (popupTransfert.ShowDialog() == true)
            {

            }
        }

        void popupTransfert_Closing(object sender, CancelEventArgs e)
        {

        }

        private void cm_duplicate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.GetTreeviewItem != null)
                {
                    SdeViewModel _sde = GetTreeviewItem.DataContext as SdeViewModel;
                    reader = new SqliteReader(Utilities.getConnectionString(MAIN_DATABASE_PATH, _sde.Sde.SdeId));
                    writer = new SqliteDataWriter(_sde.Sde.SdeId);
                    List<BatimentModel> batiments = reader.GetAllBatimentModel();
                    foreach (BatimentModel b in batiments)
                    {
                        List<BatimentModel> batsDuplicate = reader.GetBatimentByRec(b.Qrec);
                        if (batsDuplicate.Count >= 2)
                        {
                            for (int i = 1; i < batsDuplicate.Count; i++)
                            {
                                BatimentModel bd = batsDuplicate.ElementAt<BatimentModel>(i);
                                bool result = writer.deleteBatiment(EntityMapper.mapTo(bd));
                                if (result == true)
                                    log.Info(" Batiment "+b.BatimentId+"Duplicate erase");
                            }
                        }
                    }
                    //Supprimer les logements
                    List<LogementModel> logements = reader.GetAllLogements();
                    foreach (LogementModel lg in logements)
                    {
                        List<LogementModel> logsDuplicate = reader.GetLogementsByNoOrdre(lg.BatimentId,lg.Qlin1NumeroOrdre);
                        if (logsDuplicate.Count >= 2)
                        {
                            log.Info("Nombre pour batiments " + lg.BatimentId + ":" + logsDuplicate.Count);
                            for (int i = 1; i < logsDuplicate.Count; i++)
                            {
                                LogementModel l = logsDuplicate.ElementAt<LogementModel>(i);
                                bool result = writer.deleteLogement(EntityMapper.mapTo(l));
                                if (result == true)
                                    log.Info(" Batiement/"+l.BatimentId+"Logement/" + l.Qlin1NumeroOrdre + "Duplicate erase");
                            }
                        }
                    }
                    //Supprimer les menages
                    List<MenageModel> menages = reader.GetAllMenages();
                    foreach (MenageModel mn in menages)
                    {
                        List<MenageModel> menagesDuplicate = reader.GetMenagebyNumOrdre(mn.BatimentId,mn.LogeId,mn.Qm1NoOrdre);
                        if (menagesDuplicate.Count >= 2)
                        {
                            log.Info("Nombre pour batiments " + mn.BatimentId + ":" + menagesDuplicate.Count);
                            for (int i = 1; i < menagesDuplicate.Count; i++)
                            {
                                MenageModel l = menagesDuplicate.ElementAt<MenageModel>(i);
                                bool result = writer.deleteMenage(EntityMapper.mapTo(l));
                                if (result == true)
                                    log.Info(" Batiment/" + l.BatimentId + "Logement/" + l.LogeId +"Menage:"+l.Qm1NoOrdre);
                            }
                        }
                    }
                    //Supprimer les deces
                    List<DecesModel> deces = reader.GetAllDeces();
                    foreach (DecesModel dec in deces)
                    {
                        List<DecesModel> decesDuplicate = reader.GetDecesbyNumOrdre(dec.BatimentId,dec.LogeId,dec.MenageId,dec.Qd2NoOrdre);
                        if (decesDuplicate.Count >= 2)
                        {
                            log.Info("Nombre pour batiments " + dec.BatimentId + ":" + decesDuplicate.Count);
                            for (int i = 1; i < decesDuplicate.Count; i++)
                            {
                                DecesModel l = decesDuplicate.ElementAt<DecesModel>(i);
                                bool result = writer.deleteDeces(EntityMapper.mapTo(l));
                                if (result == true)
                                    log.Info(" Batiment/" + l.BatimentId + "Logement/" + l.LogeId + "Menage:" + l.MenageId+"Deces:"+l.Qd2NoOrdre);
                            }
                        }
                    }
                    //Supprimer les emigres
                    List<EmigreModel> emigres = reader.GetAllEmigres();
                    foreach (EmigreModel em in emigres)
                    {
                        List<EmigreModel> emigreDuplicate = reader.GetEmigrebyNumOrdre(em.BatimentId, em.LogeId, em.MenageId, em.Qn1numeroOrdre);
                        if (emigreDuplicate.Count >= 2)
                        {
                            log.Info("Nombre pour batiments " + em.BatimentId + ":" + emigreDuplicate.Count);
                            for (int i = 1; i < emigreDuplicate.Count; i++)
                            {
                                EmigreModel l = emigreDuplicate.ElementAt<EmigreModel>(i);
                                bool result = writer.deleteEmigre(EntityMapper.mapTo(l));
                                if (result == true)
                                    log.Info(" Batiment/" + l.BatimentId + "Logement/" + l.LogeId + "Menage:" + l.MenageId + "Emigre:" + l.Qn1numeroOrdre);
                            }
                        }
                    }
                    //Supprimer les individus
                    List<IndividuModel> individus = reader.GetAllIndividus();
                    foreach (IndividuModel ind in individus)
                    {
                        List<IndividuModel> individuDuplicate = reader.GetIndividuByNumOrdre(ind.BatimentId, ind.LogeId, ind.MenageId, ind.Q1NoOrdre);
                        if (individuDuplicate.Count >= 2)
                        {
                            log.Info("Nombre pour batiments " + ind.BatimentId + ":" + individuDuplicate.Count);
                            for (int i = 1; i < individuDuplicate.Count; i++)
                            {
                                IndividuModel l = individuDuplicate.ElementAt<IndividuModel>(i);
                                bool result = writer.deleteIndividu(EntityMapper.mapTo(l));
                                if (result == true)
                                    log.Info(" Batiment/" + l.BatimentId + "Logement/" + l.LogeId + "Menage:" + l.MenageId + "Individu:" + l.Q1NoOrdre);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

    }

}
