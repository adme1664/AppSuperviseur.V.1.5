using Ht.Ihsi.Rgph.Logging.Logs;
using Ht.Ihsil.Rgph.App.Superviseur.Exceptions;
using Ht.Ihsil.Rgph.App.Superviseur.Mapper;
using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Ht.Ihsil.Rgph.App.Superviseur.MVVM;
using Ht.Ihsil.Rgph.App.Superviseur.services;
using Ht.Ihsil.Rgph.App.Superviseur.utils;
using Ht.Ihsil.Rgph.App.Superviseur.viewModels.ContreEnquete;
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

namespace Ht.Ihsil.Rgph.App.Superviseur.views.Contre_Enquete
{
    /// <summary>
    /// Logique d'interaction pour frm_batiment_vide.xaml
    /// </summary>
    public partial class frm_batiment_vide : UserControl
    {
        Logger log;
        TreeCEViewModel model;
        MdfService service = null;
        ContreEnqueteService contreEnqueteService = null;
        private TreeViewItem getTreeviewItem;
        TreeViewItem currentContainer = null;
        ISqliteDataWriter sw = null;
        int typeContreEnquete = 0;
        public TreeViewItem GetTreeviewItem
        {
            get { return getTreeviewItem; }
            set { getTreeviewItem = value; }
        }
        public frm_batiment_vide(int typeContreEnquete)
        {
            InitializeComponent();
            Users.users.SupDatabasePath = AppDomain.CurrentDomain.BaseDirectory + @"Data\";
            this.typeContreEnquete = typeContreEnquete;
            if (typeContreEnquete == (int)Constant.TypeContrEnquete.BatimentVide)
                txt_title.Text = "BATIMAN KI VID";
            if (typeContreEnquete == (int)Constant.TypeContrEnquete.LogementCollectif)
                txt_title.Text = "BATIMAN KI GEN LOJMAN KOLEKTIF KI VID";
            if (typeContreEnquete == (int)Constant.TypeContrEnquete.LogementInvididuelVide)
                txt_title.Text = "BATIMAN KI GEN LOJMAN ENVIDIDYEL KI VID";
            if (typeContreEnquete == (int)Constant.TypeContrEnquete.LogementIndividuelMenage)
                txt_title.Text = "BATIMAN KI GEN MENAJ";
            if (typeContreEnquete == (int)Constant.TypeContrEnquete.LogementOccupantAbsent)
                txt_title.Text = "BATIMAN KI GEN LOJMAN KI OKIPE YON LE KONSA";
            log = new Logger();
            contreEnqueteService = new ContreEnqueteService(Users.users.SupDatabasePath);
            service = new MdfService();
            SdeModel[] sdeModel = service.getAllSde();
            List<SdeModel> sdes = new List<SdeModel>();
            foreach (SdeModel ss in sdeModel.ToList())
            {
                SdeModel s = ss;
                s.TypeContreEnquete = typeContreEnquete;
                sdes.Add(s);
            }
            model = new TreeCEViewModel(sdes.ToArray());
            trg_main.ItemsSource = model.Sdes;
            currentContainer = new TreeViewItem();
            sw = new SqliteDataWriter();
        }

        private void scrl_view_tree_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }
        private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            try
            {
                TreeViewItem currentContainer = e.OriginalSource as TreeViewItem;
                this.GetTreeviewItem = currentContainer;
                SdeCEViewModel _sde;
                ContreEnqueteViewModel _contreEnquete;
                while (currentContainer != null)
                {
                    if (currentContainer.DataContext.ToString() == Constant.DATACONTEXT_SDECEVIEWMODEL)
                    {
                        _sde = currentContainer.DataContext as SdeCEViewModel;
                        frm_details_sdes fr_sde = new frm_details_sdes(_sde.Sde);
                        fr_sde.lbl_details_sde.Text = Utilities.getGeoInformation(_sde.Sde.SdeId);
                        Utilities.showControl(fr_sde, grd_details);
                    }
                    if (currentContainer.DataContext.ToString() == Constant.DATACONTEXT_CONTREENQUETECEVIEWMODEL)
                    {
                        _contreEnquete = currentContainer.DataContext as ContreEnqueteViewModel;
                        frm_save_CE frm = new frm_save_CE(_contreEnquete.ContreEnquete);
                        Utilities.showControl(frm, grd_details);
                    }
                    currentContainer = getParent(currentContainer);
                }
            }
            catch (Exception ex)
            {
                log.Info("<>===Exception:" + ex.Message);
                log.Info("<>===Exception:" + ex.StackTrace);
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
        private void trg_main_Loaded(object sender, RoutedEventArgs e)
        {
            if (trg_main.Items.Count != 0)
                (trg_main.ItemContainerGenerator.ContainerFromIndex(0) as TreeViewItem).IsSelected = true;
        }

        #region BATIMENTS
        private void cm_batiments_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.GetTreeviewItem != null)
                {
                    SdeCEViewModel _sde = this.GetTreeviewItem.DataContext as SdeCEViewModel;
                    Random random = new Random();
                    List<TreeViewItemViewModel> treeItems = new List<TreeViewItemViewModel>();
                    for (int i = 0; i <= 3; i++)
                    {
                        if (_sde.Children.Count() != 4)
                        {
                            treeItems.Add(_sde.Children.ElementAt(random.Next(1, _sde.Children.Count())));
                        }
                    }
                    if (treeItems.Count() >= 1)
                    {
                        _sde.Children.Clear();
                        foreach (TreeViewItemViewModel item in treeItems)
                        {
                            _sde.Children.Add(item);
                        }
                        string msg = "Voulez-vous enregistrer les batiments selectionnés?";
                        MessageBoxButton button = MessageBoxButton.YesNo;
                        MessageBoxResult result = MessageBox.Show(msg, Constant.WINDOW_TITLE, button);
                        switch (result)
                        {
                            case MessageBoxResult.Yes:
                                MessageBox.Show("OUI");
                                break;
                            case MessageBoxResult.No:
                                MessageBox.Show("NON");
                                break;

                        }
                    }

                }
                else
                {
                    MessageBox.Show("Not yetSelected");
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void cm_batiments_questions_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.GetTreeviewItem == null)
                {
                    MessageBox.Show("Ou dwe klike sou yon batiman.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (this.GetTreeviewItem.DataContext.ToString() == Constant.DATACONTEXT_BATIMENTCEVIEWMODEL)
                    {
                        BatimentCEViewModel batViewModel = this.GetTreeviewItem.DataContext as BatimentCEViewModel;
                        BatimentModel model = new BatimentModel();
                        model.BatimentId = batViewModel.Batiment.BatimentId;
                        model.SdeId = batViewModel.Batiment.SdeId;
                        BatimentCEModel batiment = ModelMapper.MapToBatimentCEModel(contreEnqueteService.daoCE.getBatiment(Convert.ToInt32(model.BatimentId), model.SdeId));
                        if (batiment.IsContreEnqueteMade.GetValueOrDefault() == true)
                        {
                            MessageBox.Show(Constant.MSG_KE_IS_MADE, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            if (batiment.Statut.GetValueOrDefault() != 0)
                            {
                                MessageBox.Show("Ou gentan chwazi batiman sa a deja", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else
                            {
                                frm_save_questions bat = new frm_save_questions(batViewModel);
                                Utilities.showControl(bat, grd_details);
                            }
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                log.Info("Erreur:/" + ex.Message);
            }
        }
        private void cm_valide_bat_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.GetTreeviewItem == null)
                {
                    MessageBox.Show("Ou dwe kilke sou yon batiman.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (this.GetTreeviewItem.DataContext.ToString() == Constant.DATACONTEXT_BATIMENTCEVIEWMODEL)
                    {
                        BatimentCEViewModel batViewModel = this.GetTreeviewItem.DataContext as BatimentCEViewModel;
                        BatimentModel model = new BatimentModel();
                        model.BatimentId = batViewModel.Batiment.BatimentId;
                        model.SdeId = batViewModel.Batiment.SdeId;
                        BatimentCEModel batiment = ModelMapper.MapToBatimentCEModel(contreEnqueteService.daoCE.getBatiment(Convert.ToInt32(model.BatimentId), model.SdeId));
                        if (batiment.BatimentId != 0)
                        {
                            if (batiment.IsContreEnqueteMade.GetValueOrDefault() == false)
                            {
                                throw new MessageException(Constant.GetStringValue(Constant.ContreEnqueteNotMade.Batiment));
                            }
                            else
                            {
                                batiment.IsValidated = true;
                                contreEnqueteService.updateBatiment(batiment);
                                BatimentModel bat = new BatimentModel();
                                bat.BatimentId = batiment.BatimentId;
                                bat.SdeId = batiment.SdeId;
                                bat.IsValidated = true;
                                sw.validate<BatimentModel>(bat, bat.SdeId);
                                MessageBox.Show(Constant.GetStringValue(Constant.ValidateConfirm.Batiment), Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                                //Fire l'IHM  pour afficher l'etat du batiment en question
                                batViewModel.Status = true;
                                batViewModel.Tip = Constant.GetStringValue(Constant.ToolTipMessage.Valide_deja);
                                batViewModel.ImageSource = Constant.GetStringValue(Constant.ImagePath.Valide);
                            }
                        }

                    }
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Ou dwe chwazi yon menaj anvan.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (MessageException ex)
            {
                MessageBox.Show(ex.Message, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {

            }
        }
        private void cm_view_bat_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (GetTreeviewItem == null)
                {
                    MessageBox.Show("Ou dwe kilke sou yon batiman.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (this.GetTreeviewItem.DataContext.ToString() == Constant.DATACONTEXT_BATIMENTCEVIEWMODEL)
                    {
                        BatimentCEViewModel batViewModel = this.GetTreeviewItem.DataContext as BatimentCEViewModel;
                        BatimentCEModel batiment = ModelMapper.MapToBatimentCEModel(contreEnqueteService.daoCE.getBatiment(Convert.ToInt32(batViewModel.Batiment.BatimentId), batViewModel.Batiment.SdeId));
                        if (batiment.IsContreEnqueteMade.GetValueOrDefault() == true)
                        {
                            Dispatcher.Invoke(new Action(() =>
                            {
                                frm_view_ce frm = new frm_view_ce(batiment);
                                Utilities.showControl(frm, grd_details);
                            }));
                        }
                        else
                        {
                            MessageBox.Show("Ou poko fe kont-anket sou batiman sa a.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                        }


                    }
                }
            }
            catch (Exception)
            {

            }
        }
        private void cm_bat_compare_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.GetTreeviewItem == null)
                {
                    MessageBox.Show("Ou dwe klike sou yon batiman avan.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    BatimentCEViewModel batViewModel = this.GetTreeviewItem.DataContext as BatimentCEViewModel;
                    BatimentModel model = new BatimentModel();
                    model.BatimentId = batViewModel.Batiment.BatimentId;
                    model.SdeId = batViewModel.Batiment.SdeId;
                    BatimentCEModel batiment = ModelMapper.MapToBatimentCEModel(contreEnqueteService.daoCE.getBatiment(Convert.ToInt32(model.BatimentId), model.SdeId));
                    if (batiment.IsContreEnqueteMade.GetValueOrDefault() == true)
                    {
                        frm_rpt_comparaison frm = new frm_rpt_comparaison(batiment);
                        Utilities.showControl(frm, grd_details);
                    }
                    else
                    {
                        MessageBox.Show("Ou dwe fe kont-anket sou batiman sa a avan.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
            }
            catch (Exception)
            {

            }
        }
        #endregion

        #region LOGEMENTS
        private void cm_lojman_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.GetTreeviewItem == null)
                {
                    MessageBox.Show("Ou dwe klike sou yon lojman.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    LogementCEViewModel logViewModel = this.GetTreeviewItem.DataContext as LogementCEViewModel;
                    if (logViewModel != null)
                    {
                        if (logViewModel.Logement.IsContreEnqueteMade.GetValueOrDefault() == 1)
                        {
                            MessageBox.Show(Constant.MSG_KE_IS_MADE, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            LogementModel model = new LogementModel();
                            model.LogeId = logViewModel.Logement.LogeId;
                            model.BatimentId = Convert.ToInt32(logViewModel.Logement.BatimentId);
                            model.SdeId = logViewModel.Logement.SdeId;
                            model.Qlin1NumeroOrdre = Convert.ToByte(logViewModel.Logement.Qlin1NumeroOrdre.GetValueOrDefault());
                            BatimentCEModel batiment = ModelMapper.MapToBatimentCEModel(contreEnqueteService.daoCE.getBatiment(model.BatimentId, model.SdeId));
                            if (batiment.IsContreEnqueteMade.GetValueOrDefault() == false)
                            {
                                throw new MessageException(Constant.GetStringValue(Constant.ContreEnqueteNotMade.Batiment));
                            }
                            else
                            {
                                frm_save_questions viewLogement = new frm_save_questions(logViewModel);
                                Utilities.showControl(viewLogement, grd_details);
                            }
                        }
                        
                    }
                    else
                    {
                        MessageBox.Show("Ou dwe klike sou yon lojman.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (MessageException ex)
            {
                MessageBox.Show("" + ex.Message, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                log.Info("Error:" + ex.Message);
            }
        }
        private void cm_valide_log_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.GetTreeviewItem == null)
                {
                    MessageBox.Show("Ou dwe klike sou yon lojman.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    LogementCEViewModel logViewModel = this.GetTreeviewItem.DataContext as LogementCEViewModel;
                    if (logViewModel != null)
                    {
                        LogementCEModel _log = contreEnqueteService.getLogementCE(Convert.ToInt32(logViewModel.Logement.BatimentId), logViewModel.Logement.SdeId, Convert.ToInt32(logViewModel.Logement.LogeId));
                        if (_log != null)
                        {
                            bool isMenageValidate = false;
                            bool isBatimentContreEnqueteMade = false;
                            bool isLogementContreEnqueteMade = false;
                            //
                            //Test pour voir si le batiment dans lequel se trouve ce logement est valide\\
                            BatimentCEModel _bat = ModelMapper.MapToBatimentCEModel(contreEnqueteService.daoCE.getBatiment(_log.BatimentId, _log.SdeId));
                            if (_bat.IsContreEnqueteMade.GetValueOrDefault() == false)
                            {
                                throw new MessageException(Constant.GetStringValue(Constant.ContreEnqueteNotMade.Batiment));
                            }
                            else
                            {
                                isBatimentContreEnqueteMade = true;
                            }
                            //Test si le logement est deja contreenquete
                            if (_log.IsContreEnqueteMade.GetValueOrDefault() == 0)
                            {
                                throw new MessageException(Constant.GetStringValue(Constant.ContreEnqueteNotMade.Logement));
                            }
                            else
                                isLogementContreEnqueteMade = true;
                            //
                            //Test pour voir si le menage est valide si c'est logement individuel qui n'est pas vide
                            if (typeContreEnquete == (int)Constant.TypeContrEnquete.LogementIndividuelMenage)
                            {
                                List<MenageCEModel> _menages = ModelMapper.MapToListMenageCEModel(contreEnqueteService.daoCE.searchAllMenageCE(_bat.BatimentId, _log.LogeId, _log.SdeId));
                                foreach (MenageCEModel _men in _menages)
                                {
                                    if (_men.IsValidate == true)
                                        isMenageValidate = true;
                                    else
                                        throw new MessageException(Constant.GetStringValue(Constant.ContreEnqueteNotMade.Menage));
                                }
                            }
                            //
                            if (isBatimentContreEnqueteMade == true && isLogementContreEnqueteMade == true
                                || isBatimentContreEnqueteMade == true && isLogementContreEnqueteMade == true && isMenageValidate == true)
                            {
                                _log.IsValidated = 1;
                                contreEnqueteService.updateLogement(_log);
                                LogementModel lg = new LogementModel();
                                lg.LogeId = _log.LogeId;
                                lg.SdeId = _log.SdeId;
                                lg.IsValidated = true;
                                sw.validate<LogementModel>(lg, lg.SdeId);
                                MessageBox.Show(Constant.GetStringValue(Constant.ValidateConfirm.Logement), Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                                //Fire l'IHM  pour afficher l'etat du batiment en question
                                logViewModel.Status = true;
                                logViewModel.Tip = Constant.GetStringValue(Constant.ToolTipMessage.Valide_deja);
                                logViewModel.ImageSource = Constant.GetStringValue(Constant.ImagePath.Valide);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ou dwe klike sou yon lojman.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (MessageException ex)
            {
                MessageBox.Show(ex.Message, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {

            }
        }
        private void cm_view_log_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.GetTreeviewItem == null)
                {
                    MessageBox.Show("Ou dwe klike sou yon lojman.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    LogementCEViewModel logViewModel = this.GetTreeviewItem.DataContext as LogementCEViewModel;
                    if (logViewModel != null)
                    {
                        LogementCEModel logement = ModelMapper.MapToLogementCEModel(contreEnqueteService.daoCE.getLogementCE(Convert.ToInt32(logViewModel.Logement.BatimentId), logViewModel.Logement.SdeId, Convert.ToInt32(logViewModel.Logement.LogeId)));
                        if (logement.IsContreEnqueteMade == 1)
                        {
                            Dispatcher.Invoke(new Action(() =>
                            {
                                frm_view_ce frm = new frm_view_ce(logement);
                                Utilities.showControl(frm, grd_details);
                            }));
                        }
                        else
                        {
                            MessageBox.Show("Ou poko fe kont-anket sou lojman sa a.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ou dwe klike sou yon lojman.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (MessageException)
            {

            }
            catch (Exception ex)
            {
                log.Info("Erreur:" + ex.Message);
            }
        }
        private void cm_compare_log_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.GetTreeviewItem == null)
                {
                    MessageBox.Show("Ou dwe klike sou yon batiman avan.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    LogementCEViewModel logViewModel = this.GetTreeviewItem.DataContext as LogementCEViewModel;
                    if (logViewModel != null)
                    {
                        LogementModel logement = new LogementModel();
                        logement.BatimentId = logViewModel.Logement.BatimentId;
                        logement.LogeId = logViewModel.Logement.LogeId;
                        logement.SdeId = logViewModel.Logement.SdeId;
                        LogementCEModel model = contreEnqueteService.getLogementCE(Convert.ToInt32(logement.BatimentId), logement.SdeId, Convert.ToInt32(logement.LogeId));
                        if (model.IsContreEnqueteMade.GetValueOrDefault() == 1)
                        {
                            frm_rpt_comparaison frm = new frm_rpt_comparaison(model);
                            Utilities.showControl(frm, grd_details);
                        }
                        else
                        {
                            MessageBox.Show("Ou dwe fe kont-anket sou lojman sa a avan.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ou dwe klike sou yon lojman.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
            }
            catch (Exception)
            {

            }
        }
        #endregion

        #region MENAGES
        private void cm_menaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.GetTreeviewItem == null)
                {
                    MessageBox.Show("Ou dwe kilke sou yon menaj.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MenageCEViewModel menageCEViewModel = this.GetTreeviewItem.DataContext as MenageCEViewModel;
                    if (menageCEViewModel != null)
                    {
                        if (menageCEViewModel.Menage.IsContreEnqueteMade.GetValueOrDefault() == true)
                        {
                            MessageBox.Show(Constant.MSG_KE_IS_MADE, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            MenageModel model = new MenageModel();
                            model.MenageId = menageCEViewModel.MenageId;
                            model.LogeId = menageCEViewModel.LogementId;
                            model.BatimentId = menageCEViewModel.BatimentId;
                            model.SdeId = menageCEViewModel.NumSde;
                            model.Qm1NoOrdre = Convert.ToByte(menageCEViewModel.Menage.Qm1NoOrdre.GetValueOrDefault());
                            LogementCEModel logement = ModelMapper.MapToLogementCEModel(contreEnqueteService.daoCE.getLogementCE(Convert.ToInt32(model.BatimentId), model.SdeId, Convert.ToInt32(model.LogeId)));
                            if (logement.IsContreEnqueteMade.GetValueOrDefault() == 0)
                            {
                                MessageBox.Show(Constant.GetStringValue(Constant.ContreEnqueteNotMade.Logement), Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else
                            {
                                frm_save_questions viewMenage = new frm_save_questions(menageCEViewModel);
                                Utilities.showControl(viewMenage, grd_details);
                            }
                        }
                        
                    }
                    else
                    {
                        MessageBox.Show("Ou dwe chwazi yon menaj.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        private void cm_view_menaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.GetTreeviewItem == null)
                {
                    MessageBox.Show("Ou dwe klike sou yon menaj.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MenageCEViewModel menageCEViewModel = this.GetTreeviewItem.DataContext as MenageCEViewModel;
                    MenageCEModel _menage = ModelMapper.MapToMenageCEModel(contreEnqueteService.daoCE.getMenageCE(Convert.ToInt32(menageCEViewModel.BatimentId), Convert.ToInt32(menageCEViewModel.LogementId), menageCEViewModel.NumSde, menageCEViewModel.MenageId));
                    if (_menage.IsContreEnqueteMade == true)
                    {
                        Dispatcher.Invoke(new Action(() =>
                        {
                            frm_view_ce frm = new frm_view_ce(_menage);
                            Utilities.showControl(frm, grd_details);
                        }));

                    }
                    else
                    {
                        MessageBox.Show("Ou poko fe kont-anket sou menaj sa a.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        private void cm_view_ce_comparaison_Click(object sender, RoutedEventArgs e)
        {
            if (this.GetTreeviewItem == null)
            {
                MessageBox.Show("Ou dwe klike sou yon menaj.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MenageCEViewModel menageCEViewModel = this.GetTreeviewItem.DataContext as MenageCEViewModel;
                MenageCEModel _menage = ModelMapper.MapToMenageCEModel(contreEnqueteService.daoCE.getMenageCE(Convert.ToInt32(menageCEViewModel.BatimentId), Convert.ToInt32(menageCEViewModel.LogementId), menageCEViewModel.NumSde, menageCEViewModel.MenageId));
                if (_menage.IsContreEnqueteMade.GetValueOrDefault() == true)
                {
                    frm_rpt_comparaison frm = new frm_rpt_comparaison(_menage);
                    Utilities.showControl(frm, grd_details);
                }
                else
                {
                    MessageBox.Show("Ou dwe fe kont-anket sou mennaj sa a avan.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void cm_valide_menaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.GetTreeviewItem == null)
                {
                    MessageBox.Show("Ou dwe kilke sou yon menaj.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MenageCEViewModel menageCEViewModel = this.GetTreeviewItem.DataContext as MenageCEViewModel;
                    if (menageCEViewModel != null)
                    {
                        MenageCEModel _men = ModelMapper.MapToMenageCEModel(contreEnqueteService.daoCE.getMenageCE(menageCEViewModel.BatimentId, menageCEViewModel.LogementId, menageCEViewModel.NumSde, menageCEViewModel.MenageId));
                        bool isLogementValidate = false;
                        bool isIndividuValidate = false;
                        bool isDecesValidate = false;
                        if (_men.IsContreEnqueteMade.GetValueOrDefault() == false)
                        {
                            throw new MessageException(Constant.GetStringValue(Constant.ContreEnqueteNotMade.Menage));
                        }
                        else
                        {
                            //Recherche des individus contre-enquetes et valides
                            List<IndividuCEModel> listOfInd = contreEnqueteService.searchAllIndividuCE(_men);
                            if (listOfInd.Count != 0)
                            {
                                foreach (IndividuCEModel _ind in listOfInd)
                                {
                                    if (_ind.IsContreEnqueteMade.GetValueOrDefault() == 0)
                                    {
                                        throw new MessageException(Constant.GetStringValue(Constant.ContreEnqueteNotMade.Individu) + " #" + _ind.Qp1NoOrdre.GetValueOrDefault() + " anvan.");
                                    }
                                    else
                                    {
                                        if (_ind.IsValidated.GetValueOrDefault() == 0)
                                        {
                                            throw new MessageException(Constant.GetStringValue(Constant.ValidateNotMade.Individu) + " #" + _ind.Qp1NoOrdre.GetValueOrDefault() + " anvan.");
                                        }
                                        else
                                        {
                                            isIndividuValidate = true;
                                        }
                                    }
                                }
                            }
                            //
                            //Recherche des deces contre-enquetes et validees
                            List<DecesCEModel> listOfDeces = contreEnqueteService.searchAllDecesCE(_men);
                            if (listOfDeces.Count != 0)
                            {
                                foreach (DecesCEModel _dec in listOfDeces)
                                {
                                    if (_dec.IsContreEnqueteMade.GetValueOrDefault() == 0)
                                    {
                                        throw new MessageException(Constant.GetStringValue(Constant.ContreEnqueteNotMade.Deces) + " #" + _dec.Qd2NoOrdre.GetValueOrDefault() + " anvan.");
                                    }
                                    else
                                    {
                                        if (_dec.IsValidate == false)
                                        {
                                            throw new MessageException(Constant.GetStringValue(Constant.ValidateNotMade.Deces) + " #" + _dec.Qd2NoOrdre.GetValueOrDefault() + " anvan.");
                                        }
                                        else
                                        {
                                            isDecesValidate = true;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                isDecesValidate = true;
                            }
                            //
                            //Rechere de logement dans lequel se trouve le Menage
                            LogementCEModel _log = contreEnqueteService.getLogementCE(Convert.ToInt32(_men.BatimentId), _men.SdeId, Convert.ToInt32(_men.LogeId));
                            if (_log.IsContreEnqueteMade.GetValueOrDefault() == 0)
                            {
                                throw new MessageException(Constant.GetStringValue(Constant.ContreEnqueteNotMade.Logement));
                            }
                            else
                            {
                                isLogementValidate = true;
                            }
                            //
                            //Test if logement/Individu/deces est Ok(Contre-enquete et valide fait)
                            if (isLogementValidate == true && isIndividuValidate == true && isDecesValidate == true)
                            {
                                _men.IsValidated = true;
                                contreEnqueteService.updateMenageCE(_men);
                                MenageModel men = new MenageModel();
                                men.MenageId = _men.MenageId;
                                men.SdeId = _men.SdeId;
                                men.IsValidated = true;
                                sw.validate<MenageModel>(men, men.SdeId);
                                MessageBox.Show(Constant.GetStringValue(Constant.ValidateConfirm.Menage), Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                                //Fire l'IHM pour afficher le statut valide
                                menageCEViewModel.Status = true;
                                menageCEViewModel.Tip = Constant.GetStringValue(Constant.ToolTipMessage.Valide_deja);
                                menageCEViewModel.ImageSource = Constant.GetStringValue(Constant.ImagePath.Valide);
                            }

                        }
                    }
                }
            }
            catch (MessageException ex)
            {
                MessageBox.Show(ex.Message, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {

            }
        }
        #endregion

        #region MEANGE DETAILS
        private void cm_menageDetails_valide_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.GetTreeviewItem == null)
                {
                    MessageBox.Show("Ou dwe kilke sou yon endividi/dese.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MenageDetailsViewModel menageDetails = this.GetTreeviewItem.DataContext as MenageDetailsViewModel;
                    if (menageDetails != null)
                    {
                        if (menageDetails.Type == Constant.CODE_TYPE_ENVDIVIDI)
                        {
                            IndividuCEModel _ind = new IndividuCEModel();
                            _ind = contreEnqueteService.getIndividuCEModel(Convert.ToInt32(menageDetails.MenageType.Id), menageDetails.MenageType.SdeId);
                            _ind = contreEnqueteService.getIndividuCEModel(_ind.BatimentId, _ind.LogeId, _ind.MenageId, _ind.IndividuId, _ind.SdeId);
                            if (_ind.IsContreEnqueteMade.GetValueOrDefault() == 0)
                            {
                                throw new MessageException(Constant.GetStringValue(Constant.ContreEnqueteNotMade.Individu));
                            }
                            else
                            {
                                _ind.IsValidated = 1;
                                contreEnqueteService.updateIndividuCE(_ind);
                                MessageBox.Show(Constant.GetStringValue(Constant.ValidateConfirm.Individu), Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                                //Fire l'IHM pour afficher le statut valide
                                menageDetails.Status = true;
                                menageDetails.Tip = Constant.GetStringValue(Constant.ToolTipMessage.Valide_deja);
                                menageDetails.ImageSource = Constant.GetStringValue(Constant.ImagePath.Valide);
                            }
                        }
                        else
                        {
                            DecesCEModel _dec = new DecesCEModel();
                            _dec = ModelMapper.MapToDecesCEModel(contreEnqueteService.daoCE.getDecesCEModel(Convert.ToInt32(menageDetails.MenageType.Id), menageDetails.MenageType.SdeId));
                            if (_dec.IsContreEnqueteMade.GetValueOrDefault() == 0)
                            {
                                throw new MessageException(Constant.GetStringValue(Constant.ContreEnqueteNotMade.Deces));
                            }
                            else
                            {
                                _dec.IsValidated = 1;
                                contreEnqueteService.updateDecesCE(_dec);
                                MessageBox.Show(Constant.GetStringValue(Constant.ValidateConfirm.Deces), Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                                //Fire l'IHM pour afficher le statut valide
                                menageDetails.Status = true;
                                menageDetails.Tip = Constant.GetStringValue(Constant.ToolTipMessage.Valide_deja);
                                menageDetails.ImageSource = Constant.GetStringValue(Constant.ImagePath.Valide);
                            }
                        }
                    }
                }
            }
            catch (MessageException ex)
            {
                MessageBox.Show(ex.Message, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {

            }
        }
        private void cm_menageDetails_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.GetTreeviewItem == null)
                {
                    MessageBox.Show("Ou dwe kilke sou yon endividi/dese.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MenageDetailsViewModel menageDetails = this.GetTreeviewItem.DataContext as MenageDetailsViewModel;
                    if (menageDetails != null)
                    {
                        if (menageDetails.Type == Constant.CODE_TYPE_ENVDIVIDI)
                        {
                            IndividuCEModel model = new IndividuCEModel();
                            model = contreEnqueteService.getIndividuCEModel(Convert.ToInt32(menageDetails.MenageType.Id), menageDetails.MenageType.SdeId);
                            if (model != null)
                            {
                                if (model.IsContreEnqueteMade.GetValueOrDefault() == 1)
                                {
                                    MessageBox.Show(Constant.MSG_KE_IS_MADE, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                                else
                                {
                                    IndividuModel ind = new IndividuModel();
                                    ind.BatimentId = model.BatimentId;
                                    ind.LogeId = model.LogeId;
                                    ind.MenageId = model.MenageId;
                                    ind.IndividuId = model.IndividuId;
                                    ind.SdeId = model.SdeId;
                                    ind.Qp2APrenom = model.Q3Prenom;
                                    ind.Qp3LienDeParente = Convert.ToByte(model.Q3LienDeParente.GetValueOrDefault());
                                    LogementCEModel logementCe = contreEnqueteService.getLogementCE(Convert.ToInt32(model.BatimentId), model.SdeId, Convert.ToInt32(model.LogeId));
                                    if (logementCe.LogeId != 0)
                                    {
                                        if (logementCe.IsContreEnqueteMade.GetValueOrDefault() == 0)
                                        {
                                            MessageBox.Show(Constant.GetStringValue(Constant.ContreEnqueteNotMade.Logement), Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                                        }
                                        else
                                        {
                                            MenageCEModel menage = ModelMapper.MapToMenageCEModel(contreEnqueteService.daoCE.getMenageCE(model.BatimentId, model.LogeId, model.SdeId, model.MenageId));
                                            if (menage.MenageId != 0)
                                            {
                                                if (menage.IsContreEnqueteMade.GetValueOrDefault() == false)
                                                {
                                                    MessageBox.Show(Constant.GetStringValue(Constant.ContreEnqueteNotMade.Menage), Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                                                }
                                                else
                                                {
                                                    frm_save_questions_tab_format frm_ind = new frm_save_questions_tab_format(menageDetails);
                                                    Utilities.showControl(frm_ind, grd_details);
                                                }
                                            }
                                            else
                                            {
                                                frm_save_questions_tab_format frm_ind = new frm_save_questions_tab_format(menageDetails);
                                                Utilities.showControl(frm_ind, grd_details);
                                            }
                                        }
                                    }
                                }                             
                            }

                        }
                        if (menageDetails.Type == Constant.CODE_TYPE_DECES)
                        {
                            DecesCEModel model = new DecesCEModel();
                            model = contreEnqueteService.getDecesCEModel(Convert.ToInt32(menageDetails.MenageType.Id), menageDetails.MenageType.SdeId);
                            if (model.IsContreEnqueteMade.GetValueOrDefault() == 1)
                            {
                                MessageBox.Show(Constant.MSG_KE_IS_MADE, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else
                            {
                                if (model != null)
                                {
                                    DecesModel _deces = new DecesModel();
                                    _deces.DecesId = model.DecesId;
                                    _deces.BatimentId = model.BatimentId;
                                    _deces.LogeId = model.LogeId;
                                    _deces.MenageId = model.MenageId;
                                    _deces.SdeId = model.SdeId;
                                    _deces.Qd2NoOrdre = Convert.ToByte(model.Qd2NoOrdre.GetValueOrDefault());
                                    MenageCEModel menage = ModelMapper.MapToMenageCEModel(contreEnqueteService.daoCE.getMenageCE(model.BatimentId, model.LogeId, model.SdeId, model.MenageId));
                                    if (menage.IsContreEnqueteMade.GetValueOrDefault() == false)
                                    {
                                        MessageBox.Show(Constant.GetStringValue(Constant.ContreEnqueteNotMade.Menage), Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                                    }
                                    else
                                    {
                                        frm_save_questions frm_deces = new frm_save_questions(menageDetails);
                                        Utilities.showControl(frm_deces, grd_details);
                                    }
                                }
                            }
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message);
            }
        }
        private void cm_view_des_ind_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.GetTreeviewItem == null)
                {
                    MessageBox.Show("Ou dwe kilke sou yon dese/endividi.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MenageDetailsViewModel menageDetails = this.GetTreeviewItem.DataContext as MenageDetailsViewModel;
                    if (menageDetails != null)
                    {
                        if (menageDetails.Type == Constant.CODE_TYPE_ENVDIVIDI)
                        {
                            IndividuCEModel model = new IndividuCEModel();
                            model = contreEnqueteService.getIndividuCEModel(Convert.ToInt32(menageDetails.MenageType.Id), menageDetails.MenageType.SdeId);
                            if (model.IsContreEnqueteMade.GetValueOrDefault() == 1)
                            {
                                Dispatcher.Invoke(new Action(() =>
                                {
                                    frm_view_ce frm = new frm_view_ce(model);
                                    Utilities.showControl(frm, grd_details);
                                }));
                            }
                            else
                            {
                                MessageBox.Show("Ou poko fe kont-anket sou endividi sa a.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                            }

                        }
                        if (menageDetails.Type == Constant.CODE_TYPE_DECES)
                        {
                            DecesCEModel model = new DecesCEModel();
                            model = contreEnqueteService.getDecesCEModel(Convert.ToInt32(menageDetails.MenageType.Id), menageDetails.MenageType.SdeId);
                            if (model.IsContreEnqueteMade.GetValueOrDefault() == 1)
                            {
                                Dispatcher.Invoke(new Action(() =>
                                {
                                    frm_view_ce frm = new frm_view_ce(model);
                                    Utilities.showControl(frm, grd_details);
                                }));
                            }
                            else
                            {
                                MessageBox.Show("Ou poko fe kont-anket sou dese sa a.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Info("Error:cm_view_des_ind_Click/==========<>" + ex.Message);
            }
        }

        #endregion

        #region CONTRE-ENQUETE
        private void cm_valide_kont_anket_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.GetTreeviewItem == null)
                {
                    MessageBox.Show("Ou dwe kilke sou yon batiman.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    ContreEnqueteViewModel viewModel = this.GetTreeviewItem.DataContext as ContreEnqueteViewModel;
                    if (model != null)
                    {
                        ContreEnqueteModel contre_enquete = viewModel.ContreEnquete;
                        BatimentCEModel batiment = contreEnqueteService.getBatiment(contre_enquete.BatimentId.GetValueOrDefault(), contre_enquete.SdeId);
                        if (batiment.IsValidated.GetValueOrDefault() == true)
                        {
                            contre_enquete.Statut = (int)Constant.StatutContreEnquete.Valide;
                            bool result = contreEnqueteService.daoCE.updateContreEnquete(ModelMapper.MapToTblContreEnquete(contre_enquete));
                            if (result == true)
                            {
                                MessageBox.Show("Kont-ankèt sa a valide", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                                //Fire l'IHM pour afficher le statut valide
                                viewModel.Status = true;
                                viewModel.Tip = Constant.GetStringValue(Constant.ToolTipMessage.Valide_deja);
                                viewModel.ImageSource = Constant.GetStringValue(Constant.ImagePath.Valide);
                            }
                            else
                            {
                                throw new MessageException("Erreur lors de la sauvegarge");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Batiman ki nan kont-ankèt sa a poko valide", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }

                }
            }
            catch (MessageException ex)
            {
                MessageBox.Show(ex.Message, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {

            }
        }
        #endregion

    }
}
