using DevExpress.Xpf.Charts;
using DevExpress.Xpf.Grid;
using Ht.Ihsi.Rgph.Logging.Logs;
using Ht.Ihsil.Rgph.App.Superviseur.entites;
using Ht.Ihsil.Rgph.App.Superviseur.Mapper;
using Ht.Ihsil.Rgph.App.Superviseur.Models;
using Ht.Ihsil.Rgph.App.Superviseur.services;
using Ht.Ihsil.Rgph.App.Superviseur.utils;
using Ht.Ihsil.Rgph.App.Superviseur.views.Contre_Enquete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Ht.Ihsil.Rgph.App.Superviseur.views
{
    /// <summary>
    /// Logique d'interaction pour frm_verification.xaml
    /// </summary>
    public partial class frm_verification : UserControl
    {

        #region  DECLARATIONS
        private static string MAIN_DATABASE_PATH = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\RgphData\Data\Databases\";
        ISqliteReader reader;
        bool tabNotesAlreadyOpen = false;
        private SdeModel sdeSelected = null;
        List<BatimentModel> listOfBatiments;
        NameValue TypeDifficulte;
        ThreadStart ths = null;
        Thread t = null;
        bool IsAllDistrict = false;
        bool isTabCouvertureLoad = false;
        bool tabCodificationFocus = false;
        bool tabFlagCounterFocus = false;
        ProblemeModel problemeRowToUpdate = null;
        frm_view_verification main;
        Logger log;
        #endregion

        #region CONSTRCUTORS
        public frm_verification(SdeModel sde, frm_view_verification mainFrame)
        {
            InitializeComponent();
            main = mainFrame;
            log = new Logger();
            sdeSelected = sde;
            listOfBatiments = new List<BatimentModel>();            
            //Style of the tabcontrol
            #region Ajout des donnees dans les tableaux
            reader = new SqliteReader(Utilities.getConnectionString(MAIN_DATABASE_PATH, sde.SdeId));
            List<TableVerificationModel> verificationsNonReponseTotal = Utilities.getVerificatoinNonReponseTotal(MAIN_DATABASE_PATH, sde.SdeId);
            dtg_non_reponse_totale.ItemsSource = verificationsNonReponseTotal;

            List<TableVerificationModel> verificationsNonReponsePartielle = Utilities.getVerificationNonReponsePartielle(MAIN_DATABASE_PATH, sde.SdeId);
            dtg_non_reponse_partielle.ItemsSource = verificationsNonReponsePartielle;

            List<VerificationFlag> verficationFlags = Utilities.getVerificationNonReponseParVariable(MAIN_DATABASE_PATH, sde.SdeId);
            #endregion

            #region Ajout des images dans les nodes
            //Expand the node in level 1
            foreach (TreeListNode node in treeListView1.Nodes)
            {
                node.IsExpanded = true;
                node.Image = new BitmapImage(new Uri(@"/images/report3.png", UriKind.Relative));
                foreach (TreeListNode childNode in node.Nodes)
                {
                    TableVerificationModel model = childNode.Content as TableVerificationModel;
                    if (model.Niveau == "2")
                    {
                        childNode.Image = new BitmapImage(new Uri(@"/images/malrempli.png", UriKind.Relative));
                    }
                    //Node Batiman adding image Icon
                    foreach (TreeListNode batimanNode in childNode.Nodes)
                    {
                        batimanNode.Image = new BitmapImage(new Uri(@"/images/home.png", UriKind.Relative));
                    }
                }
            }
            //
            foreach (TreeListNode node in treeListView_partielle.Nodes)
            {
                node.IsExpanded = true;
                node.Image = new BitmapImage(new Uri(@"/images/report3.png", UriKind.Relative));
                foreach (TreeListNode childNode in node.Nodes)
                {
                    TableVerificationModel model = childNode.Content as TableVerificationModel;
                    if (model.Niveau == "2")
                    {
                        childNode.IsExpanded = true;
                        childNode.Image = new BitmapImage(new Uri(@"/images/malrempli.png", UriKind.Relative));
                    }
                    //Node logement
                    foreach (TreeListNode logementChild in childNode.Nodes)
                    {
                        TableVerificationModel niveau3 = logementChild.Content as TableVerificationModel;
                        if (niveau3.Niveau == "3")
                        {
                            logementChild.Image = new BitmapImage(new Uri(@"/images/logement.png", UriKind.Relative));
                        }
                        //a l'interieur du node Logement
                        foreach (TreeListNode menageChild in logementChild.Nodes)
                        {
                            TableVerificationModel niveau4 = menageChild.Content as TableVerificationModel;
                            if (niveau4.Niveau == "4")
                            {
                                menageChild.Image = new BitmapImage(new Uri(@"/images/home.png", UriKind.Relative));
                            }
                        }
                    }
                    //Node Menage
                    foreach (TreeListNode menageChild in childNode.Nodes)
                    {
                        TableVerificationModel niveau3 = menageChild.Content as TableVerificationModel;
                        if (niveau3.Niveau == "5")
                        {
                            menageChild.Image = new BitmapImage(new Uri(@"/images/menage.png", UriKind.Relative));
                        }
                        if (niveau3.Niveau == "6")
                        {
                            menageChild.Image = new BitmapImage(new Uri(@"/images/individu1.png", UriKind.Relative));
                        }
                    }

                }
            }
            #endregion
            //           
            tabIndCouverture.Focus();

        }
        public frm_verification(bool isAllDistrict, frm_view_verification mainFrame)
        {
            InitializeComponent();
            main = mainFrame;
            tabGestionNotes.Visibility = Visibility.Hidden;
            this.IsAllDistrict = isAllDistrict;
            tabIndCouverture.Focus();

            List<TableVerificationModel> verificationsNonReponseTotal = Utilities.getVerificatoinNonReponseTotalForAllSdes(MAIN_DATABASE_PATH);
            List<TableVerificationModel> verificationsNonReponsePartielle = Utilities.getVerificationNonReponsePartielleForAllSdes(MAIN_DATABASE_PATH);
            dtg_non_reponse_totale.ItemsSource = verificationsNonReponseTotal;
            dtg_non_reponse_partielle.ItemsSource = verificationsNonReponsePartielle;
            //Expand the node in level 2
            foreach (TreeListNode node in treeListView1.Nodes)
            {
                node.IsExpanded = true;
                node.Image = new BitmapImage(new Uri(@"/images/report3.png", UriKind.Relative));
                foreach (TreeListNode childNode in node.Nodes)
                {
                    TableVerificationModel model = childNode.Content as TableVerificationModel;
                    if (model.Niveau == "2")
                    {
                        childNode.IsExpanded = true;
                        childNode.Image = new BitmapImage(new Uri(@"/images/malrempli.png", UriKind.Relative));
                    }
                    //Node Sde
                    foreach (TreeListNode sdeChild in childNode.Nodes)
                    {
                        TableVerificationModel niveau3 = sdeChild.Content as TableVerificationModel;
                        if (niveau3.Niveau == "3")
                        {
                            sdeChild.Image = new BitmapImage(new Uri(@"/images/database.png", UriKind.Relative));
                        }
                        //Node batiment
                        foreach (TreeListNode batimanChild in sdeChild.Nodes)
                        {
                            TableVerificationModel niveau4 = batimanChild.Content as TableVerificationModel;
                            if (niveau4.Niveau == "4")
                            {
                                batimanChild.Image = new BitmapImage(new Uri(@"/images/home.png", UriKind.Relative));
                            }
                        }
                    }
                }
            }
            //treeListView_partielle
            //
            foreach (TreeListNode node in treeListView_partielle.Nodes)
            {
                node.IsExpanded = true;
                node.Image = new BitmapImage(new Uri(@"/images/report3.png", UriKind.Relative));
                foreach (TreeListNode childNode in node.Nodes)
                {
                    TableVerificationModel model = childNode.Content as TableVerificationModel;
                    if (model.Niveau == "2")
                    {
                        childNode.IsExpanded = true;
                        childNode.Image = new BitmapImage(new Uri(@"/images/malrempli.png", UriKind.Relative));
                    }
                    //Node Sde
                    foreach (TreeListNode sdeChild in childNode.Nodes)
                    {
                        TableVerificationModel niveau3 = sdeChild.Content as TableVerificationModel;
                        if (niveau3.Niveau == "3")
                        {
                            sdeChild.Image = new BitmapImage(new Uri(@"/images/database.png", UriKind.Relative));
                        }
                        //Node batiment
                        foreach (TreeListNode logementChild in sdeChild.Nodes)
                        {
                            TableVerificationModel niveau4 = logementChild.Content as TableVerificationModel;
                            if (niveau4.Niveau == "4")
                            {
                                logementChild.Image = new BitmapImage(new Uri(@"/images/menu-image.png", UriKind.Relative));
                            }
                            foreach (TreeListNode batimanChild in logementChild.Nodes)
                            {
                                TableVerificationModel niveau5 = batimanChild.Content as TableVerificationModel;
                                if (niveau5.Niveau == "5")
                                {
                                    batimanChild.Image = new BitmapImage(new Uri(@"/images/home.png", UriKind.Relative));
                                }
                            }
                        }
                    }
                }
            }
            //

        }
        #endregion

        #region GESTION DES NOTES
        private void tabGestionNotes_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tabNotesAlreadyOpen == false)
            {
                //Creation de la liste des problemes et leurs codes;
                cmbTypeProbleme.ItemsSource = Constant.ListOfDifficultes();
                cmbObjet.ItemsSource = Utilities.getNameOfObjects();
                btn_save.IsEnabled = false;
                tabNotesAlreadyOpen = true;
            }
          
        }

        private void cmbObjet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            NameValue selectedName = (NameValue)cmb.SelectedItem;
           
            listOfBatiments = new List<BatimentModel>();
            if (selectedName != null)
            {
                txtLibelle.Text = "";
                //
                //Si la difficulte selectionnee est de type B3.17
                if (TypeDifficulte.Name == "B3.17")
                {
                    List<QuestionModule> listOfQuestions = ModelMapper.MapToListQuestionModule(reader.listOfQuestionModule(selectedName.Value));
                    List<QuestionsModel> models = new List<QuestionsModel>();
                    foreach (QuestionModule qm in listOfQuestions)
                    {

                        QuestionsModel Q = ModelMapper.MapToQuestionModel(reader.getQuestion(qm.codeQuestion));
                        models.Add(Q);
                    }
                    cmbCodeQuestion.ItemsSource = models;
                }
                //Sinon
                else
                {
                    cmbDomaine.ItemsSource = Constant.searchSectionByObjet(selectedName.Name);
                }
            }
        }

        private void cmbCodeQuestion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            QuestionsModel selectedQuestion = (QuestionsModel)cmb.SelectedItem;
            if (selectedQuestion != null)
            {

                txtLibelle.Text = selectedQuestion.Libelle;
                //cmbBatiment.ItemsSource = reader.GetAllBatimentModel();
            }
        }
        public void blankAndDisabedComponents()
        {
            txtIndicateur.Text = "";
            cmbDomaine.IsEnabled = false;
            cmbObjet.IsEnabled = false;
            cmbCodeQuestion.IsEnabled = false;
            txtNature.IsEnabled = false;
            cmbBatiment.IsEnabled = false;
            cmbBatiment.ItemsSource = new List<BatimentModel>();
            //cmbObjet.ItemsSource = new List<Na>();
            cmbDomaine.ItemsSource = new List<NameValue>();
            cmbCodeQuestion.ItemsSource = new List<QuestionsModel>();
            listBCodeBatiment.Items.Clear();
            txtNature.Text = "";
            txtLibelle.Text = "";
            btn_save.IsEnabled = false;
            listOfBatiments = new List<BatimentModel>();
            //listBCodeBatiment.ItemsSource = new List<BatimentModel>();
        }
        private void cmbBatiment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            BatimentModel batiment = (BatimentModel)cmb.SelectedItem;
            bool equal = false;
            btn_save.IsEnabled = true;
            if (batiment != null)
            {
                if (listOfBatiments.Count == 0)
                {
                    listBCodeBatiment.Items.Add(batiment);
                    listOfBatiments.Add(batiment);
                }
                else
                {
                    foreach (BatimentModel bat in listOfBatiments)
                    {
                        if (batiment.BatimentId == bat.BatimentId)
                        {
                            equal = true;
                            break;
                        }
                    }
                    if (equal == false)
                    {
                        listBCodeBatiment.Items.Add(batiment);
                        listOfBatiments.Add(batiment);
                    }

                }

            }
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IConfigurationService configuration = new ConfigurationService();
                NameValue valueSelected = null;
                QuestionsModel questionSelected = null;
                NameValue domaineSelected = null;
                //
                //Test si le superviseur n'a pas selectionne un objet
                if (cmbObjet.SelectedItem != null)
                {
                    valueSelected = (NameValue)cmbObjet.SelectedItem;
                }
                else
                {
                    MessageBox.Show("Ou dwe chwazi ki objè ajan an gen pwoblèm avèk li a.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                //
                //Test si le superviseur n'a pas selectionne une question
                if (cmbCodeQuestion.SelectedItem != null)
                {
                    questionSelected = (QuestionsModel)cmbCodeQuestion.SelectedItem;
                }
                else
                {
                    MessageBox.Show("Ou dwe chwazi yon kesyon.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                //
                //Si le supervisueur a choisi une domaine
                if (cmbDomaine.SelectedItem != null)
                {
                    domaineSelected = (NameValue)cmbDomaine.SelectedItem;
                }
                //
                //Test si le superviseur n'a pas choisi de batiments
                if (listBCodeBatiment.Items.Count != 0)
                {
                    bool result = false;
                    foreach (BatimentModel batiment in listBCodeBatiment.Items)
                    {
                        ProblemeModel probleme = new ProblemeModel();
                        probleme.Objet = valueSelected.Name;
                        probleme.SdeId = sdeSelected.SdeId;
                        probleme.CodeQuestion = questionSelected.CodeQuestion;
                        probleme.BatimentId = batiment.BatimentId;
                        probleme.Nature = txtNature.Text;
                        if (TypeDifficulte.Name == "B3.18")
                        {
                            probleme.Domaine = domaineSelected.Value;
                        }
                        if (btn_save.Content.ToString() == "Sauvegarder")
                        {
                            result = configuration.saveProbleme(probleme);
                        }
                        else
                        {
                            probleme.ProblemeId = problemeRowToUpdate.ProblemeId;
                            result = configuration.updateProbleme(probleme);
                        }
                    }
                    if (result == true)
                    {
                        MessageBox.Show("Anregistreman an fet ak siksè.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                        tabAjout.Header = "Sauvegarder";
                        blankAndDisabedComponents();
                    }
                    else
                    {
                        MessageBox.Show("Gen yon èrè pandan anregistreman.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                        blankAndDisabedComponents();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gen yon pwoblem pandan anregistreman an ap fet.=>/" + ex.Message, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                blankAndDisabedComponents();
            }
        }

        private void cmbTypeProbleme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            NameValue objet = (NameValue)cmb.SelectedItem;
            txtIndicateur.Text = "";
            cmbDomaine.ItemsSource = new List<NameValue>();
            cmbCodeQuestion.ItemsSource = new List<QuestionsModel>();
            cmbBatiment.ItemsSource = new List<BatimentModel>();
            txtNature.Text = "";
            txtLibelle.Text = "";
            cmbObjet.ItemsSource = new List<NameValue>();
            listBCodeBatiment.Items.Clear();
            btn_save.IsEnabled = false;

            if (objet != null)
            {
                TypeDifficulte = objet;
                if (objet.Name == "B3.17")
                {
                    txtIndicateur.Text = objet.Value;
                    cmbDomaine.IsEnabled = false;
                    cmbObjet.IsEnabled = true;
                    cmbCodeQuestion.IsEnabled = true;
                    txtNature.IsEnabled = true;
                    if (tabAjout.Header.ToString() == "Modifier")
                    {
                        cmbBatiment.IsEnabled = false;
                        btn_save.IsEnabled = true;
                    }
                    else
                    {
                        cmbBatiment.IsEnabled = true;
                        btn_save.IsEnabled = true;
                    }

                    listBCodeBatiment.IsEnabled = true;
                    cmbObjet.ItemsSource = Utilities.getNameOfObjects();
                    cmbBatiment.ItemsSource = reader.GetAllBatimentModel();
                }
                else
                {
                    txtIndicateur.Text = objet.Value;
                    cmbDomaine.IsEnabled = true;
                    cmbObjet.IsEnabled = true;
                    cmbCodeQuestion.IsEnabled = true;
                    txtNature.IsEnabled = true;
                    if (tabAjout.Header.ToString() == "Modifier")
                    {
                        cmbBatiment.IsEnabled = false;
                        btn_save.IsEnabled = true;
                    }
                    else
                    {
                        cmbBatiment.IsEnabled = true;
                        btn_save.IsEnabled = true;
                    }
                        
                    //btn_save.IsEnabled = true;
                    listBCodeBatiment.IsEnabled = true;
                    cmbObjet.ItemsSource = Utilities.getNameOfObjects();
                    cmbBatiment.ItemsSource = reader.GetAllBatimentModel();
                }
            }
        }

        private void cmbDomaine_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            NameValue objet = (NameValue)cmb.SelectedItem;
            if (objet != null)
            {
                List<QuestionsModel> listOfQuestions = ModelMapper.MapToListQuestionModel(reader.searchQuestionByCategorie(objet.Name));
                //
                if (listOfQuestions != null)
                {
                    cmbCodeQuestion.ItemsSource = listOfQuestions;
                }
            }
        }

        private void tabAjout_GotFocus(object sender, RoutedEventArgs e)
        {
            if(tabAjout.Header.ToString()=="Modifier")
                cmbBatiment.IsEnabled = false;
        }
        #endregion

        #region CONTROLS EVENTS
        private void DXTabItem_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                IConfigurationService configuration = new ConfigurationService();
                if (sdeSelected != null)
                    grdDifficulties.ItemsSource = configuration.searchAllProblemesBySdeId(sdeSelected.SdeId);
                else
                {
                    grdDifficulties.ItemsSource = configuration.searchAllProblemes();
                }
            }
            catch (Exception)
            {

            }
        }

        private void updateRowItem_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            problemeRowToUpdate = (ProblemeModel)grdDifficulties.GetRow(grdDifficulties.GetSelectedRowHandles()[0]);
            List<BatimentModel> listOfBat = reader.GetAllBatimentModel();
            
            if (problemeRowToUpdate != null)
            {
                tabAjout.Header = "Modifier";
                tabControlNotes.SelectedItem = tabAjout;
                tabAjout.Focus();
                cmbObjet.IsEnabled = true;
                btn_save.Content = "Modifier";
                cmbBatiment.IsEnabled = false;
                btn_save.IsEnabled = true;

                //Ajout des autres informations necessaires
                cmbTypeProbleme.ItemsSource = Constant.ListOfDifficultes();
                cmbObjet.ItemsSource = Utilities.getNameOfObjects();
                //
                //Selection dy type de difficulte rencontree 
                if (problemeRowToUpdate.Domaine != null)
                {
                    foreach (NameValue type in cmbTypeProbleme.Items)
                    {
                        if (type.Name == "B3.18")
                        {
                            cmbTypeProbleme.SelectedItem = type;
                            break;
                        }

                    }
                    //
                }
                else
                {
                    foreach (NameValue type in cmbTypeProbleme.Items)
                    {
                        if (type.Name == "B3.17")
                        {
                            cmbTypeProbleme.SelectedItem = type;
                            break;
                        }

                    }
                }
                //Selectionne l'objet a modfiee
                foreach (NameValue val in cmbObjet.Items)
                {
                    if (problemeRowToUpdate.Objet == val.Name)
                    {
                        cmbObjet.SelectedItem = val;
                        cmbDomaine.ItemsSource = Constant.searchSectionByObjet(val.Name);
                        break;
                    }

                }
                //Selection du domaine
                foreach (NameValue domaine in cmbDomaine.Items)
                {
                    if (problemeRowToUpdate.Domaine == domaine.Value)
                    {
                        cmbDomaine.SelectedItem = domaine;
                        List<QuestionsModel> listOfQuestions = ModelMapper.MapToListQuestionModel(reader.searchQuestionByCategorie(domaine.Name));
                        //
                        if (listOfQuestions != null)
                        {
                            cmbCodeQuestion.ItemsSource = listOfQuestions;
                        }
                        break;
                    }
                }

                //
                //Selectionne la question a modifiee
                foreach (QuestionsModel question in cmbCodeQuestion.Items)
                {
                    if (question.CodeQuestion == problemeRowToUpdate.CodeQuestion)
                    {
                        cmbCodeQuestion.SelectedItem = question;
                        //Set le libelle de la question
                        txtLibelle.Text = question.Libelle;
                        break;
                        //
                    }

                }
                //
                //Set la nature de la difficultee rencontree
                if (problemeRowToUpdate.Nature != null)
                {
                    txtNature.Text = problemeRowToUpdate.Nature;
                }
                //
                //Set les codes des batiments selectionnes
                if (problemeRowToUpdate.BatimentId != null)
                {
                    listOfBatiments = new List<BatimentModel>();
                    foreach (BatimentModel batiment in listOfBat)
                    {
                        if (batiment.BatimentId == problemeRowToUpdate.BatimentId)
                        {
                            listOfBatiments.Add(batiment);
                            listBCodeBatiment.Items.Add(batiment);
                         }
                    }
                }
            }
        }


        private void deleteDataItem_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            ProblemeModel row = (ProblemeModel)grdDifficulties.GetRow(grdDifficulties.GetSelectedRowHandles()[0]);
            if (row != null)
            {
                IConfigurationService configuration = new ConfigurationService();
                MessageBoxResult confirm = MessageBox.Show("Eske ou vle efase anrejistreman sa a?", Constant.WINDOW_TITLE, MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (confirm == MessageBoxResult.Yes)
                {
                    bool result = configuration.deleteProbleme(Convert.ToInt32(row.ProblemeId));
                    if (result == true)
                    {
                        MessageBox.Show("Anregistreman sa efase.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                        grdDifficulties.ItemsSource = configuration.searchAllProblemes();
                    }
                }
            }
        }

        private void dtg_non_reponse_totale_AutoGeneratingColumn(object sender, DevExpress.Xpf.Grid.AutoGeneratingColumnEventArgs e)
        {
            if (e.Column.FieldName == "Color")
                e.Column.Visible = false;
            if (e.Column.FieldName == "Niveau")
                e.Column.Visible = false;
        }
        #endregion

        #region CHARTS CONTROLS

        public void initializeChartControls()
        {
            try
            {
                pieSeriesNbreFemmes.Dispatcher.BeginInvoke((Action)(() => pieSeriesNbreFemmes.Points.Clear()));
                barSeriesProportionsDetails.Dispatcher.BeginInvoke((Action)(() => barSeriesProportionsDetails.Points.Clear()));
                barSeriesTailleMenage.Dispatcher.BeginInvoke((Action)(() => barSeriesTailleMenage.Points.Clear()));
                barSeriesIndividus.Dispatcher.BeginInvoke((Action)(() => barSeriesIndividus.Points.Clear()));
                pieSeriesNbreMenage.Dispatcher.BeginInvoke((Action)(() => pieSeriesNbreMenage.Points.Clear()));
                if (tabFlagCounterFocus == true)
                {
                    chartFlag0.Dispatcher.BeginInvoke((Action)(() => chartFlag0.Points.Clear()));
                    chartFlag1.Dispatcher.BeginInvoke((Action)(() => chartFlag1.Points.Clear()));
                    chartFlag2.Dispatcher.BeginInvoke((Action)(() => chartFlag2.Points.Clear()));
                    chartFlag3.Dispatcher.BeginInvoke((Action)(() => chartFlag3.Points.Clear()));
                    chartFlag4.Dispatcher.BeginInvoke((Action)(() => chartFlag4.Points.Clear()));
                    chartFlag5.Dispatcher.BeginInvoke((Action)(() => chartFlag5.Points.Clear()));
                    chartFlag6.Dispatcher.BeginInvoke((Action)(() => chartFlag6.Points.Clear()));
                    chartFlag7.Dispatcher.BeginInvoke((Action)(() => chartFlag7.Points.Clear()));
                    chartFlag8.Dispatcher.BeginInvoke((Action)(() => chartFlag8.Points.Clear()));
                    chartFlag9.Dispatcher.BeginInvoke((Action)(() => chartFlag9.Points.Clear()));
                    chartFlag10.Dispatcher.BeginInvoke((Action)(() => chartFlag10.Points.Clear()));
                    chartFlag11.Dispatcher.BeginInvoke((Action)(() => chartFlag11.Points.Clear()));
                    chartFlag12.Dispatcher.BeginInvoke((Action)(() => chartFlag12.Points.Clear()));
                }
                if (tabCodificationFocus == true)
                {
                    entierementPreCodifie.Dispatcher.BeginInvoke((Action)(() => entierementPreCodifie.Points.Clear()));
                    partiellementPreCodifieAutre.Dispatcher.BeginInvoke((Action)(() => partiellementPreCodifieAutre.Points.Clear()));
                    pasDuToutPreCodifie.Dispatcher.BeginInvoke((Action)(() => pasDuToutPreCodifie.Points.Clear()));
                }
            }
            catch (Exception)
            {

            }
        }
        public void createGraphicSocioControls()
        {
            //Initialiser les controls
            initializeChartControls();
            //
            int nbrePersonnes = 0;
            int nbreFemmesRecenses = 0;
            int nbreEnfantMoins1Ans = 0;
            int nbreEnfant10Ans = 0;
            int nbrePersonnes18Ans = 0;
            int nbrePersonnes65Ans = 0;
            int nbreBatiment = 0;
            int nbreLogementC = 0;
            int nbreLogementCD = 0;
            int nbreLogementInd = 0;
            int nbreMenage = 0;
            int nbreLimitation = 0;
            int nbreFemmeChefMenage = 0;
            int nbreMenageUniPersonnel = 0;
            int nbreMenage6Personnes = 0;
            double tailleMoyenneMenage = 0;
            try
            {
                IConfigurationService configuration = new ConfigurationService();
            //Test pour voir si le calcul doit se faire pour tout le district ou pr un SDE
            if (IsAllDistrict == true)
            {
                foreach (SdeModel sde in configuration.searchAllSdes())
                {
                    reader = new SqliteReader(Utilities.getConnectionString(MAIN_DATABASE_PATH, sde.SdeId));
                    //Indicateurs socio-demographiques
                    nbreEnfantMoins1Ans = nbreEnfantMoins1Ans + reader.getTotalEnfantDeMoinsDe1Ans();
                    nbrePersonnes = nbrePersonnes + reader.getTotalIndividusHommes();
                    nbreFemmesRecenses = nbreFemmesRecenses + reader.getTotalIndividusFemmes();
                    nbreEnfant10Ans = nbreEnfant10Ans + reader.getTotalIndividu10AnsEtPlus();
                    nbrePersonnes18Ans = nbrePersonnes18Ans + reader.getTotalIndividu18AnsEtPlus();
                    nbrePersonnes65Ans += reader.getTotalIndividu65AnsEtPlus();
                    tailleMoyenneMenage += reader.tailleMoyenneMenage();
                    //

                    //
                    nbreLogementC += reader.getTotalPersonnesByLogementCollectif();
                    nbreLogementCD += reader.getTotalPersonnesByLogementCollectifDeclare();
                    nbreLimitation += reader.getTotalPersonnesByLimitation();
                    nbreMenage = nbreMenage + reader.getTotalHommeChefMenage();
                    nbreFemmeChefMenage += reader.getTotalFemmeChefMenage();
                    nbreMenageUniPersonnel += reader.getTotalMenageUnipersonnel();
                    nbreMenage6Personnes += nbreMenage6Personnes + reader.getTotalMenageDe6IndsEtPlus();

                }
            }
            else
            {
                reader = new SqliteReader(Utilities.getConnectionString(MAIN_DATABASE_PATH, sdeSelected.SdeId));
                //Indicateurs de couverture
                nbreBatiment = nbreBatiment + reader.GetAllBatimentModel().Count();
                nbreLogementInd = nbreLogementInd + reader.getTotalLogementInds();
                nbreMenage = nbreMenage + reader.getTotalHommeChefMenage();
                //

                //Indicateurs socio-demographiques
                nbreEnfantMoins1Ans = nbreEnfantMoins1Ans + reader.getTotalEnfantDeMoinsDe1Ans();
                nbrePersonnes = nbrePersonnes + reader.getTotalIndividusHommes();
                nbreFemmesRecenses = nbreFemmesRecenses + reader.getTotalIndividusFemmes();
                nbreEnfant10Ans = nbreEnfant10Ans + reader.getTotalIndividu10AnsEtPlus();
                nbrePersonnes18Ans = nbrePersonnes18Ans + reader.getTotalIndividu18AnsEtPlus();
                nbrePersonnes65Ans += reader.getTotalIndividu65AnsEtPlus();
                tailleMoyenneMenage += reader.tailleMoyenneMenage();
                //

                //
                nbreLogementC += reader.getTotalPersonnesByLogementCollectif();
                nbreLogementCD += reader.getTotalPersonnesByLogementCollectifDeclare();
                nbreLimitation += reader.getTotalPersonnesByLimitation();
                nbreFemmeChefMenage += reader.getTotalFemmeChefMenage();
                nbreMenageUniPersonnel += reader.getTotalMenageUnipersonnel();
            }
            pieSeriesNbreFemmes.Dispatcher.BeginInvoke((Action)(() => pieSeriesNbreFemmes.Points.Add(new SeriesPoint("% d'hommes dans la population recensée", nbrePersonnes))));
            pieSeriesNbreFemmes.Dispatcher.BeginInvoke((Action)(() => pieSeriesNbreFemmes.Points.Add(new SeriesPoint("% de femmes dans la population recensée", nbreFemmesRecenses))));

            barSeriesProportionsDetails.Dispatcher.BeginInvoke((Action)(() =>
                barSeriesProportionsDetails.Points.Add(new SeriesPoint("Proportion (%) d´enfants de moins d´un (1) an", nbreEnfantMoins1Ans))));
            barSeriesProportionsDetails.Dispatcher.BeginInvoke((Action)(() =>
                barSeriesProportionsDetails.Points.Add(new SeriesPoint("Proportion (%)  de personnes de 10 ans et plus", nbreEnfant10Ans))));
            barSeriesProportionsDetails.Dispatcher.BeginInvoke((Action)(() =>
                barSeriesProportionsDetails.Points.Add(new SeriesPoint("Proportion (%)  de personnes de 18 ans et plus", nbrePersonnes18Ans))));
            barSeriesProportionsDetails.Dispatcher.BeginInvoke((Action)(() =>
                barSeriesProportionsDetails.Points.Add(new SeriesPoint("Proportion (%)  de personnes de 65 ans plus", nbrePersonnes65Ans))));

            barSeriesIndividus.Dispatcher.BeginInvoke((Action)(() =>
                    barSeriesIndividus.Points.Add(new SeriesPoint("Nombre de personnes recensées dans les logements collectifs", nbreLogementC))));
            barSeriesIndividus.Dispatcher.BeginInvoke((Action)(() =>
                    barSeriesIndividus.Points.Add(new SeriesPoint("Nombre de personnes declarées dans les logements collectifs", nbreLogementCD))));
            barSeriesIndividus.Dispatcher.BeginInvoke((Action)(() =>
                    barSeriesIndividus.Points.Add(new SeriesPoint("Nombre de personnes présentant au moins une limitation dans leurs activités. ", nbreLimitation))));

            barSeriesTailleMenage.Dispatcher.BeginInvoke((Action)(() =>
                    barSeriesTailleMenage.Points.Add(new SeriesPoint("Taille moyenne des ménages", tailleMoyenneMenage))));
            barSeriesTailleMenage.Dispatcher.BeginInvoke((Action)(() =>
                        barSeriesTailleMenage.Points.Add(new SeriesPoint("Proportion (%)  de ménages unipersonnels (1 personne au plus)", nbreMenageUniPersonnel))));
            barSeriesTailleMenage.Dispatcher.BeginInvoke((Action)(() =>
                        barSeriesTailleMenage.Points.Add(new SeriesPoint("Proportion (%)  de ménages de grande taille (6  et plus)", nbreMenage6Personnes))));

            pieSeriesNbreMenage.Dispatcher.BeginInvoke((Action)(() =>
                   pieSeriesNbreMenage.Points.Add(new SeriesPoint("Proportion (%)  de ménages dont le chef est un homme", nbreMenage))));
            pieSeriesNbreMenage.Dispatcher.BeginInvoke((Action)(() =>
                       pieSeriesNbreMenage.Points.Add(new SeriesPoint("Proportion (%)  de ménages dont le chef est une femme. ", nbreFemmeChefMenage))));

            waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));

            }
            catch (Exception)
            {

            }
        }

        public void createGraphicTabCouverture()
        {
            
            int nbrePersonnes = 0;
            int nbrePersonneTermine = 0;
            int nbrePersonneNonTermine = 0;
            int nbrePersonneVerifie = 0;
            int nbrePersonneNonVerifie= 0;
            int nbrePersonneValide = 0;
            int nbreBatimentTotal = 0;
            int nbreBatimentTotalTermine = 0;
            int nbreBatimentTotalNonTermine = 0;
            int nbreBatimentTotalVerifie = 0;
            int nbreBatimentTotalNonVerife = 0;
            int nbreBatimentTotalValide = 0;
            int nbreLogementIndTotal = 0;
            int nbreLogementIndTotalTermine = 0;
            int nbreLogementIndTotalNonTermine = 0;
            int nbreLogementIndTotalVerifie = 0;
            int nbreLogementIndTotalNonVerifie = 0;
            int nbreLogementIndTotalValide = 0;
            int nbreLogementColTotal = 0;
            int nbreLogementColTotalTermine = 0;
            int nbreLogementColTotalNonTermine = 0;
            int nbreLogementColTotalVerifie = 0;
            int nbreLogementColTotalNonVerifie = 0;
            int nbreLogementColTotalValide = 0;
            int nbreMenageTotal = 0;
            int nbreMenageTotalTermine = 0;
            int nbreMenageTotalNonTermine = 0;
            int nbreMenageTotalVerifie = 0;
            int nbreMenageTotalNonVerife = 0;
            int nbreMenageTotalValide = 0;
            int nbreActualisation = 0;
            try
            {


                IConfigurationService configuration = new ConfigurationService();
                List<CouvertureModel> couvertures = new List<CouvertureModel>();

                #region CONSTRUCTION DE LA GRAPHE
                //Test pour verifier si la table st deja telechargee
                if (isTabCouvertureLoad == false)
                {
                    //Test pour voir si le calcul doit se faire pour tout le district ou pr une SDE
                    if (IsAllDistrict == true)
                    {
                        foreach (SdeModel sde in configuration.searchAllSdes())
                        {
                            reader = new SqliteReader(Utilities.getConnectionString(MAIN_DATABASE_PATH, sde.SdeId));

                            //Indicateurs de couverture
                            //Batiments
                            nbreBatimentTotal = nbreBatimentTotal + reader.GetAllBatimentModel().Count();
                            nbreBatimentTotalNonTermine = nbreBatimentTotalNonTermine + reader.GetAllBatimentNotFinished().Count;
                            nbreBatimentTotalTermine = nbreBatimentTotalTermine + reader.GetAllBatimentModelTermine().Count;
                            nbreBatimentTotalVerifie = nbreBatimentTotalVerifie + reader.GetAllBatimentVerifies().Count;
                            nbreBatimentTotalNonVerife = nbreBatimentTotalNonVerife + reader.GetAllBatimentNonVerifies().Count;
                            nbreBatimentTotalValide = nbreBatimentTotalValide + reader.GetAllBatimentModelValide().Count;
                            //
                            //Logements Individuels
                            nbreLogementIndTotal = nbreLogementIndTotal + reader.GetAllLogementsIndividuels().Count;
                            nbreLogementIndTotalNonTermine = nbreLogementIndTotalNonTermine + reader.GetAllLogementIndNonTermines().Count;
                            nbreLogementIndTotalTermine = nbreLogementIndTotalTermine + reader.GetAllLogementsIndTermines().Count;
                            nbreLogementIndTotalVerifie = nbreLogementIndTotalVerifie + reader.GetAllLogementsIndVerifies().Count;
                            nbreLogementIndTotalNonVerifie = nbreLogementIndTotalNonVerifie + reader.GetAllLogementsIndNonVerifies().Count;
                            nbreLogementIndTotalValide = nbreLogementIndTotalValide + reader.GetAllLogementsIndValides().Count;
                            //
                            //Logements Collectifs
                            nbreLogementColTotal = nbreLogementColTotal + reader.GetAllLogementsCollectifs().Count;
                            nbreLogementColTotalNonTermine = nbreLogementColTotalNonTermine + reader.GetAllLogementsColNonTermines().Count;
                            nbreLogementColTotalNonVerifie = nbreLogementColTotalNonVerifie + reader.GetAllLogementsColNonVerifies().Count;
                            nbreLogementColTotalTermine = nbreLogementColTotalTermine + reader.GetAllLogementsColTermines().Count;
                            nbreLogementColTotalValide = nbreLogementColTotalValide + reader.GetAllLogementsColValides().Count;
                            nbreLogementColTotalVerifie = nbreLogementColTotalVerifie + reader.GetAllLogementsColVerifies().Count;
                            //
                            //Menages
                            nbreMenageTotal = nbreMenageTotal + reader.GetAllMenages().Count();
                            nbreMenageTotalNonTermine = nbreMenageTotalNonTermine + reader.GetAllMenageNonTermine().Count;
                            nbreMenageTotalNonVerife = nbreMenageTotalNonVerife + reader.GetAllMenagesNonVerifies().Count;
                            nbreMenageTotalTermine = nbreMenageTotalTermine + reader.GetAllMenageTermine().Count;
                            nbreMenageTotalValide = nbreMenageTotalValide + reader.GetAllMenageValides().Count;
                            nbreMenageTotalVerifie = nbreMenageTotalVerifie + reader.GetAllMenagesVerifies().Count;
                            //
                            //Personnes
                            nbrePersonnes = nbrePersonnes + reader.GetAllIndividus().Count;
                            nbrePersonneNonTermine = nbrePersonneNonTermine + reader.GetAllIndividuNonTermine().Count;
                            nbrePersonneTermine = nbrePersonneTermine + reader.GetAllIndividuTermine().Count;
                            nbrePersonneValide = nbrePersonneValide + reader.GetAllIndividuValide().Count;
                            nbrePersonneVerifie = nbrePersonneVerifie + reader.GetAllIndividuVerifie().Count;
                            nbrePersonneNonVerifie = nbrePersonneNonVerifie + reader.GetAllIndividusNonVerifies().Count;
                            nbreActualisation = nbreActualisation + sde.TotalBatCartographie.GetValueOrDefault();
                            //
                        }
                    }
                    else
                    {
                        reader = new SqliteReader(Utilities.getConnectionString(MAIN_DATABASE_PATH, sdeSelected.SdeId));

                        //Indicateurs de couverture
                            //Batiments
                            nbreBatimentTotal = nbreBatimentTotal + reader.GetAllBatimentModel().Count();
                            nbreBatimentTotalNonTermine = nbreBatimentTotalNonTermine + reader.GetAllBatimentNotFinished().Count;
                            nbreBatimentTotalTermine = nbreBatimentTotalTermine + reader.GetAllBatimentModelTermine().Count;
                            nbreBatimentTotalVerifie = nbreBatimentTotalVerifie + reader.GetAllBatimentVerifies().Count;
                            nbreBatimentTotalNonVerife = nbreBatimentTotalNonVerife + reader.GetAllBatimentNonVerifies().Count;
                            nbreBatimentTotalValide = nbreBatimentTotalValide + reader.GetAllBatimentModelValide().Count;
                            //
                            //Logements Individuels
                            nbreLogementIndTotal = nbreLogementIndTotal + reader.GetAllLogementsIndividuels().Count;
                            nbreLogementIndTotalNonTermine = nbreLogementIndTotalNonTermine + reader.GetAllLogementIndNonTermines().Count;
                            nbreLogementIndTotalTermine = nbreLogementIndTotalTermine + reader.GetAllLogementsIndTermines().Count;
                            nbreLogementIndTotalVerifie = nbreLogementIndTotalVerifie + reader.GetAllLogementsIndVerifies().Count;
                            nbreLogementIndTotalNonVerifie = nbreLogementIndTotalNonVerifie + reader.GetAllLogementsIndNonVerifies().Count;
                            nbreLogementIndTotalValide = nbreLogementIndTotalValide + reader.GetAllLogementsIndValides().Count;
                            //
                            //Logements Collectifs
                            nbreLogementColTotal = nbreLogementColTotal + reader.GetAllLogementsCollectifs().Count;
                            nbreLogementColTotalNonTermine = nbreLogementColTotalNonTermine + reader.GetAllLogementsColNonTermines().Count;
                            nbreLogementColTotalNonVerifie = nbreLogementColTotalNonVerifie + reader.GetAllLogementsColNonVerifies().Count;
                            nbreLogementColTotalTermine = nbreLogementColTotalTermine + reader.GetAllLogementsColTermines().Count;
                            nbreLogementColTotalValide = nbreLogementColTotalValide + reader.GetAllLogementsColValides().Count;
                            nbreLogementColTotalVerifie = nbreLogementColTotalVerifie + reader.GetAllLogementsColVerifies().Count;
                            //
                            //Menages
                            nbreMenageTotal = nbreMenageTotal + reader.GetAllMenages().Count();
                            nbreMenageTotalNonTermine = nbreMenageTotalNonTermine + reader.GetAllMenageNonTermine().Count;
                            nbreMenageTotalNonVerife = nbreMenageTotalNonVerife + reader.GetAllMenagesNonVerifies().Count;
                            nbreMenageTotalTermine = nbreMenageTotalTermine + reader.GetAllMenageTermine().Count;
                            nbreMenageTotalValide = nbreMenageTotalValide + reader.GetAllMenageValides().Count;
                            nbreMenageTotalVerifie = nbreMenageTotalVerifie + reader.GetAllMenagesVerifies().Count;
                            //
                            //Personnes
                            nbrePersonnes = nbrePersonnes + reader.GetAllIndividus().Count;
                            nbrePersonneNonTermine = nbrePersonneNonTermine + reader.GetAllIndividuNonTermine().Count;
                            nbrePersonneTermine = nbrePersonneTermine + reader.GetAllIndividuTermine().Count;
                            nbrePersonneValide = nbrePersonneValide + reader.GetAllIndividuValide().Count;
                            nbrePersonneVerifie = nbrePersonneVerifie + reader.GetAllIndividuVerifie().Count;
                            nbrePersonneNonVerifie = nbrePersonneNonVerifie + reader.GetAllIndividusNonVerifies().Count;
                            nbreActualisation = nbreActualisation + sdeSelected.TotalBatCartographie.GetValueOrDefault();
                            //
                        //
                    }
                    //On cree les graphes
                    barSeriesIndCouverture.Dispatcher.BeginInvoke((Action)(() =>
                            barSeriesIndCouverture.Points.Add(new SeriesPoint("Nombre de Batiments", nbreBatimentTotal))));
                    barSeriesIndCouverture.Dispatcher.BeginInvoke((Action)(() =>
                       barSeriesIndCouverture.Points.Add(new SeriesPoint("Nombre de Logements Individuels", nbreLogementIndTotal))));
                    barSeriesIndCouverture.Dispatcher.BeginInvoke((Action)(() =>
                       barSeriesIndCouverture.Points.Add(new SeriesPoint("Nombre de Menages", nbreMenageTotal))));
                    barSeriesIndCouverture.Dispatcher.BeginInvoke((Action)(() =>
                       barSeriesIndCouverture.Points.Add(new SeriesPoint("Nombre de Personnes", nbrePersonnes))));
                #endregion

                    #region CONSTRUCTION DU TABLEAU
                    CouvertureModel model1 = new CouvertureModel();
                    model1.Couverture = "BATIMENTS";
                    model1.Actualisation = nbreActualisation;
                    model1.Total = nbreBatimentTotal;
                    model1.NonTermine = nbreBatimentTotalNonTermine.ToString();
                    model1.Termine = nbreBatimentTotalTermine.ToString();
                    model1.NonVerifie = nbreBatimentTotalNonVerife.ToString();
                    model1.Verifie = nbreBatimentTotalVerifie.ToString();
                    model1.Valide = nbreBatimentTotalValide.ToString();
                    couvertures.Add(model1);

                    //Nombre de logements Individuels
                    model1 = new CouvertureModel();
                    model1.Couverture = "LOGEMENTS INDIVIDUELS";
                    model1.Actualisation = 0;
                    model1.Total = nbreLogementIndTotal;
                    model1.NonTermine = nbreLogementIndTotalNonTermine.ToString();
                    model1.Termine = nbreLogementIndTotalTermine.ToString();
                    model1.NonVerifie = nbreLogementIndTotalNonVerifie.ToString();
                    model1.Valide = nbreLogementIndTotalValide.ToString();
                    model1.Verifie = nbreLogementIndTotalVerifie.ToString();
                    couvertures.Add(model1);
                    //
                    //Logements collectifs
                    model1 = new CouvertureModel();
                    model1.Couverture = "LOGEMENTS COLLECTIFS";
                    model1.Actualisation = 0;
                    model1.Total = nbreLogementColTotal;
                    model1.NonTermine = nbreLogementColTotalNonTermine.ToString();
                    model1.NonVerifie = nbreLogementColTotalNonVerifie.ToString();
                    model1.Termine = nbreLogementColTotalTermine.ToString();
                    model1.Valide = nbreLogementColTotalValide.ToString();
                    model1.Verifie = nbreLogementColTotalVerifie.ToString();

                    // Nombre de meanges
                    model1 = new CouvertureModel();
                    model1.Couverture = "MENAGES";
                    model1.Actualisation = 0;
                    model1.Total = nbreMenageTotal;
                    model1.Termine = nbreMenageTotalTermine.ToString();
                    model1.NonTermine = nbreMenageTotalNonTermine.ToString();
                    model1.NonVerifie = nbreMenageTotalNonVerife.ToString();
                    model1.Verifie = nbreMenageTotalVerifie.ToString();
                    model1.Valide = nbreMenageTotalValide.ToString();
                    couvertures.Add(model1);

                     //Nombre de personnes
                    model1 = new CouvertureModel();
                    model1.Couverture = "PERSONNES";
                    model1.Actualisation = 0;
                    model1.Total = nbrePersonnes;
                    model1.NonTermine = nbrePersonneNonTermine.ToString();
                    model1.NonVerifie = nbrePersonneNonVerifie.ToString();
                    model1.Termine = nbrePersonneTermine.ToString();
                    model1.Valide = nbrePersonneValide.ToString();
                    model1.Verifie = nbrePersonneVerifie.ToString();
                    couvertures.Add(model1);
                    dataGridCouverture.Dispatcher.BeginInvoke((Action)(() => dataGridCouverture.ItemsSource = couvertures));
                    #endregion

                    isTabCouvertureLoad = true;
                }
            }
            catch (Exception)
            {

            }
        }

        public void createGraphicTabPerformance()
        {

        }

        private void tabSocioDemographiques_GotFocus(object sender, RoutedEventArgs e)
        {
            if (t != null)
            {
                if (t.IsAlive)
                {
                    t.Abort();
                }
            }
            waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = true));
            try
            {
                ths = new ThreadStart(() => createGraphicSocioControls());
                t = new Thread(ths);
                t.Start();
            }
            catch (Exception)
            {
                //log.Info("ERREUR:<>===================<>" + ex.Message);
            }
        }

        private void tabIndCouverture_GotFocus(object sender, RoutedEventArgs e)
        {
            if (t != null)
            {
                if (t.IsAlive)
                {
                    t.Abort();
                }
            }
            waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = true));
            try
            {
                ths = new ThreadStart(() => createGraphicTabCouverture());
                t = new Thread(ths);
                t.Start();
            }
            catch (Exception)
            {
                //log.Info("ERREUR:<>===================<>" + ex.Message);
            }
        }
        #endregion

        public void createIndicateursPerformances(bool isForSde)
        {
            try
            {
                if (isForSde == true)
                {
                    reader = new SqliteReader(Utilities.getConnectionString(MAIN_DATABASE_PATH, sdeSelected.SdeId));
                    double nbreParJourBatiment = reader.getTotalBatRecenseParJourV();
                    double nbreParJourLogement = reader.getTotalLogeRecenseParJourV();
                    double nbreParJourMenage = reader.getTotalMenageRecenseParJourV();
                    double nbreParJourPersonnes = reader.getTotalIndRecenseParJourV();
                    List<KeyValue> list = new List<KeyValue>();
                    list.Add(new KeyValue(nbreParJourBatiment, "Nombre de questionnaires par jour de recensement"));
                    list.Add(new KeyValue(nbreParJourLogement, "Nombre de logements par jour de recensement"));
                    list.Add(new KeyValue(nbreParJourMenage, "Nombre de menages par jour de recensement"));  
                    list.Add(new KeyValue(nbreParJourPersonnes, "Nombre d'individus par jour de recensement"));
                    dataGridIndPerformance.ItemsSource = list;
                    table_view.Dispatcher.BeginInvoke((Action)(() => table_view.BestFitColumns()));
                }
            }
            catch (Exception)
            {

            }
        }

        private void tabPagePerformance_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sdeSelected != null)
            {
                createIndicateursPerformances(true);
            }
        }

        private void dtg_non_reponse_partielle_AutoGeneratingColumn(object sender, AutoGeneratingColumnEventArgs e)
        {
            if (e.Column.FieldName == "Color")
                e.Column.Visible = false;
            if (e.Column.FieldName == "Niveau")
                e.Column.Visible = false;
            if (e.Column.FieldName == "Image")
                e.Column.Visible = false;
        }

        private void tabCodification_GotFocus(object sender, RoutedEventArgs e)
        {
            decorator.Dispatcher.BeginInvoke((Action)(() => decorator.IsSplashScreenShown = true));
            tabCodificationFocus = true;
            initializeChartControls();
            try
            {
                IConfigurationService configuration = new ConfigurationService();
                int nbreTotal = 0;
                Codification _aCodifierTotal = new Codification();
                if (IsAllDistrict == true)
                {
                    foreach (SdeModel sde in configuration.searchAllSdes())
                    {
                        reader = new SqliteReader(Utilities.getConnectionString(MAIN_DATABASE_PATH, sde.SdeId));
                        nbreTotal += nbreTotal + reader.GetAllIndividus().Count();
                        Codification _aCodifier=reader.getInformationForCodification();
                        _aCodifierTotal.A5Autre += _aCodifier.A5Autre;
                        _aCodifierTotal.A5Codifie += _aCodifier.A5Codifie;
                        _aCodifierTotal.A5NeSaitPas += _aCodifier.A5NeSaitPas;
                        _aCodifierTotal.A7Autre += _aCodifier.A7Autre;
                        _aCodifierTotal.A7Codifie += _aCodifier.A7Codifie;
                        _aCodifierTotal.A7NeSaitPas += _aCodifier.A7NeSaitPas;
                        _aCodifierTotal.P10_1 += _aCodifier.P10_1;
                        _aCodifierTotal.P10_2 += _aCodifier.P10_2;
                        _aCodifierTotal.P10_3 += _aCodifier.P10_3;
                        _aCodifierTotal.P10_4 += _aCodifier.P10_4;
                        _aCodifierTotal.P12_1 += _aCodifier.P12_1;
                        _aCodifierTotal.P12_2 += _aCodifier.P12_2;
                        _aCodifierTotal.P12_3 += _aCodifier.P12_3;
                        _aCodifierTotal.P12_4 += _aCodifier.P12_4;
                        
                   }
                }
                else
                {
                    reader = new SqliteReader(Utilities.getConnectionString(MAIN_DATABASE_PATH, sdeSelected.SdeId));
                    nbreTotal = reader.GetAllIndividus().Count;
                    _aCodifierTotal = reader.getInformationForCodification();
                    gridFlagCodification.Dispatcher.BeginInvoke((Action)(() => gridFlagCodification.ItemsSource = Utilities.getListOfIndividuCodification(MAIN_DATABASE_PATH, sdeSelected.SdeId)));
                }
                
                 
                entierementPreCodifie.Dispatcher.BeginInvoke((Action)(() =>
                            entierementPreCodifie.Points.Add(new SeriesPoint("P10/Lieu de naissance", Utilities.getPourcentage(_aCodifierTotal.P10_1, nbreTotal)))));
                entierementPreCodifie.Dispatcher.BeginInvoke((Action)(() =>
                            entierementPreCodifie.Points.Add(new SeriesPoint("P12/Lieu de residence", Utilities.getPourcentage(_aCodifierTotal.P12_1, nbreTotal)))));
                entierementPreCodifie.Dispatcher.BeginInvoke((Action)(() =>
                            entierementPreCodifie.Points.Add(new SeriesPoint("A5/Branche d'activite", Utilities.getPourcentage(_aCodifierTotal.A5Codifie, nbreTotal)))));
                entierementPreCodifie.Dispatcher.BeginInvoke((Action)(() =>
                            entierementPreCodifie.Points.Add(new SeriesPoint("A7/Occupation", Utilities.getPourcentage(_aCodifierTotal.A7Codifie, nbreTotal)))));

                partiellementPreCodifie.Dispatcher.BeginInvoke((Action)(() =>
                            partiellementPreCodifie.Points.Add(new SeriesPoint("P10/Lieu de naissance", Utilities.getPourcentage(_aCodifierTotal.SommeP10PartiellementCodifie, nbreTotal)))));
                partiellementPreCodifie.Dispatcher.BeginInvoke((Action)(() =>
                            partiellementPreCodifie.Points.Add(new SeriesPoint("P12/Lieu de residence", Utilities.getPourcentage(_aCodifierTotal.SommeP12PartiellementCodifie, nbreTotal)))));

                partiellementPreCodifieAutre.Dispatcher.BeginInvoke((Action)(() =>
                            partiellementPreCodifie.Points.Add(new SeriesPoint("A5/Branche d'activite", Utilities.getPourcentage(_aCodifierTotal.A5Autre, nbreTotal)))));
                partiellementPreCodifieAutre.Dispatcher.BeginInvoke((Action)(() =>
                            partiellementPreCodifie.Points.Add(new SeriesPoint("A7/Occupation", Utilities.getPourcentage(_aCodifierTotal.A7Autre, nbreTotal)))));

                pasDuToutPreCodifie.Dispatcher.BeginInvoke((Action)(() =>
                            pasDuToutPreCodifie.Points.Add(new SeriesPoint("P10/Lieu de naissance", Utilities.getPourcentage(_aCodifierTotal.P10_4, nbreTotal)))));
                pasDuToutPreCodifie.Dispatcher.BeginInvoke((Action)(() =>
                            pasDuToutPreCodifie.Points.Add(new SeriesPoint("P12/Lieu de residence", Utilities.getPourcentage(_aCodifierTotal.P12_4, nbreTotal)))));
                pasDuToutPreCodifie.Dispatcher.BeginInvoke((Action)(() =>
                            pasDuToutPreCodifie.Points.Add(new SeriesPoint("A5/Branche d'activite", Utilities.getPourcentage(_aCodifierTotal.A5NeSaitPas, nbreTotal)))));
                pasDuToutPreCodifie.Dispatcher.BeginInvoke((Action)(() =>
                            pasDuToutPreCodifie.Points.Add(new SeriesPoint("A7/Occupation", Utilities.getPourcentage(_aCodifierTotal.A7NeSaitPas, nbreTotal)))));

           }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message);
            }
            decorator.Dispatcher.BeginInvoke((Action)(() => decorator.IsSplashScreenShown = false));
        }

        private void tabCompteurFlag_GotFocus(object sender, RoutedEventArgs e)
        {
            decorator.Dispatcher.BeginInvoke((Action)(() => decorator.IsSplashScreenShown = true));
            tabFlagCounterFocus = true;
            initializeChartControls();
            try
            {
                //ouvrir le decorateur
                //decoratorTab.Dispatcher.BeginInvoke((Action)(() => decoratorTab.IsSplashScreenShown = true));
                IConfigurationService configuration = new ConfigurationService();
                int nbreTotal = 0;
                Flag flagPopulationParDistrict = new Flag();
                Flag flagAgeDateNaissanceParDistrict = new Flag();
                Flag flagFeconditeParDistrict = new Flag();
                Flag flagEmploiParDistrict = new Flag();
                if (IsAllDistrict == true)
                {
                    foreach (SdeModel sde in configuration.searchAllSdes())
                    {
                        reader = new SqliteReader(Utilities.getConnectionString(MAIN_DATABASE_PATH, sde.SdeId));
                        List<IndividuModel> individus = reader.GetAllIndividus();
                        nbreTotal += individus.Count;
                        Flag flagPopulation = reader.CountTotalFlag(individus);
                        Flag flagAgeDateNaissance = reader.Count2FlagAgeDateNaissance();
                        Flag flagFecondite = reader.CountFlagFecondite();
                        Flag flagEmploi = reader.CountFlagEmploi();
                        flagPopulationParDistrict.Flag0 += flagPopulation.Flag0;
                        flagPopulationParDistrict.Flag1 += flagPopulation.Flag1;
                        flagPopulationParDistrict.Flag2 += flagPopulation.Flag2;
                        flagPopulationParDistrict.Flag3 += flagPopulation.Flag3;
                        flagPopulationParDistrict.Flag4 += flagPopulation.Flag4;
                        flagPopulationParDistrict.Flag5 += flagPopulation.Flag5;
                        flagPopulationParDistrict.Flag6 += flagPopulation.Flag6;
                        flagPopulationParDistrict.Flag7 += flagPopulation.Flag7;
                        flagPopulationParDistrict.Flag8 += flagPopulation.Flag8;
                        flagPopulationParDistrict.Flag9 += flagPopulation.Flag9;
                        flagPopulationParDistrict.Flag10 += flagPopulation.Flag10;
                        flagPopulationParDistrict.Flag11 += flagPopulation.Flag11;
                        flagPopulationParDistrict.Flag12 += flagPopulation.Flag12;

                        flagAgeDateNaissanceParDistrict.Flag0 += flagAgeDateNaissance.Flag0;
                        flagAgeDateNaissanceParDistrict.Flag1 += flagAgeDateNaissance.Flag1;
                        flagAgeDateNaissanceParDistrict.Flag2 += flagAgeDateNaissance.Flag2;
                        flagAgeDateNaissanceParDistrict.Flag3 += flagAgeDateNaissance.Flag3;
                        flagAgeDateNaissanceParDistrict.Flag4 += flagAgeDateNaissance.Flag4;
                        flagAgeDateNaissanceParDistrict.Flag5 += flagAgeDateNaissance.Flag5;
                        flagAgeDateNaissanceParDistrict.Flag6 += flagAgeDateNaissance.Flag6;

                        flagFeconditeParDistrict.Flag0 += flagFecondite.Flag0;
                        flagFeconditeParDistrict.Flag1 += flagFecondite.Flag1;
                        flagFeconditeParDistrict.Flag2 += flagFecondite.Flag2;
                        flagFeconditeParDistrict.Flag3 += flagFecondite.Flag3;
                        flagFeconditeParDistrict.Flag4 += flagFecondite.Flag4;
                        flagFeconditeParDistrict.Flag5 += flagFecondite.Flag5;
                        flagFeconditeParDistrict.Flag6 += flagFecondite.Flag6;

                        flagEmploiParDistrict.Flag0 += flagEmploi.Flag0;
                        flagEmploiParDistrict.Flag1 += flagEmploi.Flag1;
                        flagEmploiParDistrict.Flag2 += flagEmploi.Flag2;
                        flagEmploiParDistrict.Flag3 += flagEmploi.Flag3;
                        flagEmploiParDistrict.Flag4 += flagEmploi.Flag4;
                        flagEmploiParDistrict.Flag5 += flagEmploi.Flag5;
                        flagEmploiParDistrict.Flag6 += flagEmploi.Flag6;
                    }
                }
                else
                {
                    reader = new SqliteReader(Utilities.getConnectionString(MAIN_DATABASE_PATH, sdeSelected.SdeId));
                    List<IndividuModel> individus = reader.GetAllIndividus();
                    nbreTotal += individus.Count;
                    flagPopulationParDistrict = reader.CountTotalFlag(individus);
                    flagAgeDateNaissanceParDistrict = reader.Count2FlagAgeDateNaissance();
                    flagFeconditeParDistrict = reader.CountFlagFecondite();
                    flagEmploiParDistrict = reader.CountFlagEmploi();
                    //Fill the grid
                    gridFlag.Dispatcher.BeginInvoke((Action)(() => gridFlag.ItemsSource = Utilities.getListOfIndividuWithFlag(MAIN_DATABASE_PATH, sdeSelected.SdeId)));
                }

                chartFlag0.Dispatcher.BeginInvoke((Action)(() =>
                            chartFlag0.Points.Add(new SeriesPoint("Population Totale (13 Flags au total)", Utilities.getPourcentage(flagPopulationParDistrict.Flag0, nbreTotal)))));
                chartFlag0.Dispatcher.BeginInvoke((Action)(() =>
                             chartFlag0.Points.Add(new SeriesPoint("Population Totale (2 Flags au total)", Utilities.getPourcentage(flagAgeDateNaissanceParDistrict.Flag0, nbreTotal)))));
                chartFlag0.Dispatcher.BeginInvoke((Action)(() =>
                             chartFlag0.Points.Add(new SeriesPoint("Femmes de 13 ans et plus", Utilities.getPourcentage(flagFeconditeParDistrict.Flag0, nbreTotal)))));
                chartFlag0.Dispatcher.BeginInvoke((Action)(() =>
                             chartFlag0.Points.Add(new SeriesPoint("Population de 10 ans et plus avec un emploi", Utilities.getPourcentage(flagEmploiParDistrict.Flag0, nbreTotal)))));

                chartFlag1.Dispatcher.BeginInvoke((Action)(() =>
                            chartFlag1.Points.Add(new SeriesPoint("Population Totale (13 Flags au total)", Utilities.getPourcentage(flagPopulationParDistrict.Flag1, nbreTotal)))));
                chartFlag1.Dispatcher.BeginInvoke((Action)(() =>
                             chartFlag1.Points.Add(new SeriesPoint("Population Totale (2 Flags au total)", Utilities.getPourcentage(flagAgeDateNaissanceParDistrict.Flag1, nbreTotal)))));
                chartFlag1.Dispatcher.BeginInvoke((Action)(() =>
                             chartFlag1.Points.Add(new SeriesPoint("Femmes de 13 ans et plus", Utilities.getPourcentage(flagFeconditeParDistrict.Flag1, nbreTotal)))));
                chartFlag1.Dispatcher.BeginInvoke((Action)(() =>
                             chartFlag1.Points.Add(new SeriesPoint("Population de 10 ans et plus avec un emploi", Utilities.getPourcentage(flagEmploiParDistrict.Flag1, nbreTotal)))));

                chartFlag2.Dispatcher.BeginInvoke((Action)(() =>
                            chartFlag2.Points.Add(new SeriesPoint("Population Totale (13 Flags au total)", Utilities.getPourcentage(flagPopulationParDistrict.Flag2, nbreTotal)))));
                chartFlag2.Dispatcher.BeginInvoke((Action)(() =>
                             chartFlag2.Points.Add(new SeriesPoint("Population Totale (2 Flags au total)", Utilities.getPourcentage(flagAgeDateNaissanceParDistrict.Flag2, nbreTotal)))));
                chartFlag2.Dispatcher.BeginInvoke((Action)(() =>
                             chartFlag2.Points.Add(new SeriesPoint("Femmes de 13 ans et plus", Utilities.getPourcentage(flagFeconditeParDistrict.Flag2, nbreTotal)))));
                chartFlag2.Dispatcher.BeginInvoke((Action)(() =>
                             chartFlag2.Points.Add(new SeriesPoint("Population de 10 ans et plus avec un emploi", Utilities.getPourcentage(flagEmploiParDistrict.Flag2, nbreTotal)))));

                chartFlag3.Dispatcher.BeginInvoke((Action)(() =>
                            chartFlag3.Points.Add(new SeriesPoint("Population Totale (13 Flags au total)", Utilities.getPourcentage(flagPopulationParDistrict.Flag3, nbreTotal)))));
                chartFlag3.Dispatcher.BeginInvoke((Action)(() =>
                             chartFlag3.Points.Add(new SeriesPoint("Population Totale (2 Flags au total)", Utilities.getPourcentage(flagAgeDateNaissanceParDistrict.Flag3, nbreTotal)))));
                chartFlag3.Dispatcher.BeginInvoke((Action)(() =>
                             chartFlag3.Points.Add(new SeriesPoint("Femmes de 13 ans et plus", Utilities.getPourcentage(flagFeconditeParDistrict.Flag3, nbreTotal)))));
                chartFlag3.Dispatcher.BeginInvoke((Action)(() =>
                             chartFlag3.Points.Add(new SeriesPoint("Population de 10 ans et plus avec un emploi", Utilities.getPourcentage(flagEmploiParDistrict.Flag3, nbreTotal)))));

                chartFlag4.Dispatcher.BeginInvoke((Action)(() =>
                            chartFlag4.Points.Add(new SeriesPoint("Population Totale (13 Flags au total)", Utilities.getPourcentage(flagPopulationParDistrict.Flag4, nbreTotal)))));
                chartFlag4.Dispatcher.BeginInvoke((Action)(() =>
                             chartFlag4.Points.Add(new SeriesPoint("Population Totale (2 Flags au total)", Utilities.getPourcentage(flagAgeDateNaissanceParDistrict.Flag4, nbreTotal)))));
                chartFlag4.Dispatcher.BeginInvoke((Action)(() =>
                             chartFlag4.Points.Add(new SeriesPoint("Femmes de 13 ans et plus", Utilities.getPourcentage(flagFeconditeParDistrict.Flag4, nbreTotal)))));
                chartFlag4.Dispatcher.BeginInvoke((Action)(() =>
                             chartFlag4.Points.Add(new SeriesPoint("Population de 10 ans et plus avec un emploi", Utilities.getPourcentage(flagEmploiParDistrict.Flag4, nbreTotal)))));

                chartFlag5.Dispatcher.BeginInvoke((Action)(() =>
                            chartFlag5.Points.Add(new SeriesPoint("Population Totale (13 Flags au total)", Utilities.getPourcentage(flagPopulationParDistrict.Flag5, nbreTotal)))));
                chartFlag5.Dispatcher.BeginInvoke((Action)(() =>
                             chartFlag5.Points.Add(new SeriesPoint("Population Totale (2 Flags au total)", Utilities.getPourcentage(flagAgeDateNaissanceParDistrict.Flag5, nbreTotal)))));
                chartFlag5.Dispatcher.BeginInvoke((Action)(() =>
                             chartFlag5.Points.Add(new SeriesPoint("Femmes de 13 ans et plus", Utilities.getPourcentage(flagFeconditeParDistrict.Flag5, nbreTotal)))));
                chartFlag5.Dispatcher.BeginInvoke((Action)(() =>
                             chartFlag5.Points.Add(new SeriesPoint("Population de 10 ans et plus avec un emploi", Utilities.getPourcentage(flagEmploiParDistrict.Flag5, nbreTotal)))));

                chartFlag6.Dispatcher.BeginInvoke((Action)(() =>
                            chartFlag6.Points.Add(new SeriesPoint("Population Totale (13 Flags au total)", Utilities.getPourcentage(flagPopulationParDistrict.Flag6, nbreTotal)))));
                chartFlag6.Dispatcher.BeginInvoke((Action)(() =>
                             chartFlag6.Points.Add(new SeriesPoint("Population Totale (2 Flags au total)", Utilities.getPourcentage(flagAgeDateNaissanceParDistrict.Flag6, nbreTotal)))));
                chartFlag6.Dispatcher.BeginInvoke((Action)(() =>
                             chartFlag6.Points.Add(new SeriesPoint("Femmes de 13 ans et plus", Utilities.getPourcentage(flagFeconditeParDistrict.Flag6, nbreTotal)))));
                chartFlag6.Dispatcher.BeginInvoke((Action)(() =>
                             chartFlag6.Points.Add(new SeriesPoint("Population de 10 ans et plus avec un emploi", Utilities.getPourcentage(flagEmploiParDistrict.Flag6, nbreTotal)))));
                chartFlag7.Dispatcher.BeginInvoke((Action)(() =>
                           chartFlag7.Points.Add(new SeriesPoint("Population Totale (13 Flags au total)", Utilities.getPourcentage(flagPopulationParDistrict.Flag7, nbreTotal)))));
                chartFlag8.Dispatcher.BeginInvoke((Action)(() =>
                           chartFlag8.Points.Add(new SeriesPoint("Population Totale (13 Flags au total)", Utilities.getPourcentage(flagPopulationParDistrict.Flag8, nbreTotal)))));
                chartFlag9.Dispatcher.BeginInvoke((Action)(() =>
                           chartFlag9.Points.Add(new SeriesPoint("Population Totale (13 Flags au total)", Utilities.getPourcentage(flagPopulationParDistrict.Flag9, nbreTotal)))));
                chartFlag10.Dispatcher.BeginInvoke((Action)(() =>
                           chartFlag10.Points.Add(new SeriesPoint("Population Totale (13 Flags au total)", Utilities.getPourcentage(flagPopulationParDistrict.Flag10, nbreTotal)))));
                chartFlag11.Dispatcher.BeginInvoke((Action)(() =>
                           chartFlag11.Points.Add(new SeriesPoint("Population Totale (13 Flags au total)", Utilities.getPourcentage(flagPopulationParDistrict.Flag11, nbreTotal)))));
                chartFlag12.Dispatcher.BeginInvoke((Action)(() =>
                           chartFlag12.Points.Add(new SeriesPoint("Population Totale (13 Flags au total)", Utilities.getPourcentage(flagPopulationParDistrict.Flag12, nbreTotal)))));

                
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message);
            }
            //fermer le decorateur
            decorator.Dispatcher.BeginInvoke((Action)(() => decorator.IsSplashScreenShown = false));
        }
        private void chartControlCompteur_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Point point=e.GetPosition(this);
            ChartHitInfo info = chartControlCompteur.CalcHitInfo(point);
            if (info.Series != null)
            {
                MessageBox.Show("" + info.Series.Name);
            }

         }
        private void Grid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(this);
            ChartHitInfo info = chartControlCompteur.CalcHitInfo(point);
            if (info.Series != null)
            {
                MessageBox.Show("" + info.Series.Name);
            }
        }
        private void gridFlag_AutoGeneratingColumn(object sender, AutoGeneratingColumnEventArgs e)
        {
            if (e.Column.FieldName == "Individu")
                e.Column.Visible = false;
           
        }
        private void treeListView2_RowDoubleClick(object sender, RowDoubleClickEventArgs e)
        {
            TreeListView listView = sender as TreeListView;
            if (listView != null)
            {
                TreeListNode node = listView.FocusedNode;
                if (node != null)
                {
                    if (node.HasChildren == false)
                    {
                        RapportFlagModel flag = node.Content as RapportFlagModel;
                        frm_view_ce frm = new frm_view_ce(flag.Individu,flag.Individu.SdeId);
                        main.listBox_sde.SelectedIndex = -1;
                        Utilities.showControl(frm, main.grd_details);
                    }
                }
            }
            
        }
        private void tabFlagMenage_GotFocus(object sender, RoutedEventArgs e)
        {
            decorator.Dispatcher.BeginInvoke((Action)(() => decorator.IsSplashScreenShown = true));
            initializeChartControls();
            int nbreTotal = 0;
            try
            {
                reader = new SqliteReader(Utilities.getConnectionString(MAIN_DATABASE_PATH, sdeSelected.SdeId));
                nbreTotal = reader.GetAllMenages().Count;
                Flag menage_1_Personne = reader.compteurFlagParMenages(reader.GetAllMenage_1_Personne());
                Flag menage_2_3_Personne = reader.compteurFlagParMenages(reader.GetAllMenage_2_3_Personnes());
                Flag menage_4_5_Personne = reader.compteurFlagParMenages(reader.GetAllMenage_4_5_Personnes());
                Flag menage_6_Personne = reader.compteurFlagParMenages(reader.GetAllMenage_6_Personnes());
                Flag all_menage = reader.compteurFlagParMenages(reader.GetAllMenages());
                
                //Creation des barres
                aucunFlag.Dispatcher.BeginInvoke((Action)(() =>
                            aucunFlag.Points.Add(new SeriesPoint("Menage Unipersonnel (1)", Utilities.getPourcentage(menage_1_Personne.Flag_Aucun, nbreTotal)))));
                aucunFlag.Dispatcher.BeginInvoke((Action)(() =>
                            aucunFlag.Points.Add(new SeriesPoint("Ménage de deux (2) à trois (3) individus", Utilities.getPourcentage(menage_2_3_Personne.Flag_Aucun, nbreTotal)))));
                aucunFlag.Dispatcher.BeginInvoke((Action)(() =>
                            aucunFlag.Points.Add(new SeriesPoint("Ménage de quatre (4) à cinq (5) individus", Utilities.getPourcentage(menage_4_5_Personne.Flag_Aucun, nbreTotal)))));
                aucunFlag.Dispatcher.BeginInvoke((Action)(() =>
                            aucunFlag.Points.Add(new SeriesPoint("Ménage de six (6) individus ou plus", Utilities.getPourcentage(menage_6_Personne.Flag_Aucun, nbreTotal)))));
                aucunFlag.Dispatcher.BeginInvoke((Action)(() =>
                            aucunFlag.Points.Add(new SeriesPoint("Tous les menages", Utilities.getPourcentage(all_menage.Flag_Aucun, nbreTotal)))));

                flag_1_4.Dispatcher.BeginInvoke((Action)(() =>
                            flag_1_4.Points.Add(new SeriesPoint("Menage Unipersonnel (1)", Utilities.getPourcentage(menage_1_Personne.Flag_1_4, nbreTotal)))));
                flag_1_4.Dispatcher.BeginInvoke((Action)(() =>
                            flag_1_4.Points.Add(new SeriesPoint("Ménage de deux (2) à trois (3) individus", Utilities.getPourcentage(menage_2_3_Personne.Flag_1_4, nbreTotal)))));
                flag_1_4.Dispatcher.BeginInvoke((Action)(() =>
                            flag_1_4.Points.Add(new SeriesPoint("Ménage de quatre (4) à cinq (5) individus", Utilities.getPourcentage(menage_4_5_Personne.Flag_1_4, nbreTotal)))));
                flag_1_4.Dispatcher.BeginInvoke((Action)(() =>
                            flag_1_4.Points.Add(new SeriesPoint("Ménage de six (6) individus ou plus", Utilities.getPourcentage(menage_6_Personne.Flag_1_4, nbreTotal)))));
                flag_1_4.Dispatcher.BeginInvoke((Action)(() =>
                            flag_1_4.Points.Add(new SeriesPoint("Tous les menages", Utilities.getPourcentage(all_menage.Flag_1_4, nbreTotal)))));

                flag_5_14.Dispatcher.BeginInvoke((Action)(() =>
                            flag_5_14.Points.Add(new SeriesPoint("Menage Unipersonnel (1)", Utilities.getPourcentage(menage_1_Personne.Flag_5_14, nbreTotal)))));
                flag_5_14.Dispatcher.BeginInvoke((Action)(() =>
                            flag_5_14.Points.Add(new SeriesPoint("Ménage de deux (2) à trois (3) individus", Utilities.getPourcentage(menage_2_3_Personne.Flag_5_14, nbreTotal)))));
                flag_5_14.Dispatcher.BeginInvoke((Action)(() =>
                            flag_5_14.Points.Add(new SeriesPoint("Ménage de quatre (4) à cinq (5) individus", Utilities.getPourcentage(menage_4_5_Personne.Flag_5_14, nbreTotal)))));
                flag_5_14.Dispatcher.BeginInvoke((Action)(() =>
                            flag_5_14.Points.Add(new SeriesPoint("Ménage de six (6) individus ou plus", Utilities.getPourcentage(menage_6_Personne.Flag_5_14, nbreTotal)))));
                flag_5_14.Dispatcher.BeginInvoke((Action)(() =>
                            flag_5_14.Points.Add(new SeriesPoint("Tous les menages", Utilities.getPourcentage(all_menage.Flag_5_14, nbreTotal)))));

                flag_15_26.Dispatcher.BeginInvoke((Action)(() =>
                            flag_15_26.Points.Add(new SeriesPoint("Menage Unipersonnel (1)", Utilities.getPourcentage(menage_1_Personne.Flag_15_26, nbreTotal)))));
                flag_15_26.Dispatcher.BeginInvoke((Action)(() =>
                            flag_15_26.Points.Add(new SeriesPoint("Ménage de deux (2) à trois (3) individus", Utilities.getPourcentage(menage_2_3_Personne.Flag_15_26, nbreTotal)))));
                flag_15_26.Dispatcher.BeginInvoke((Action)(() =>
                            flag_15_26.Points.Add(new SeriesPoint("Ménage de quatre (4) à cinq (5) individus", Utilities.getPourcentage(menage_4_5_Personne.Flag_15_26, nbreTotal)))));
                flag_15_26.Dispatcher.BeginInvoke((Action)(() =>
                            flag_15_26.Points.Add(new SeriesPoint("Ménage de six (6) individus ou plus", Utilities.getPourcentage(menage_6_Personne.Flag_15_26, nbreTotal)))));
                flag_15_26.Dispatcher.BeginInvoke((Action)(() =>
                            flag_15_26.Points.Add(new SeriesPoint("Tous les menages", Utilities.getPourcentage(all_menage.Flag_15_26, nbreTotal)))));

                flag_27_47.Dispatcher.BeginInvoke((Action)(() =>
                            flag_27_47.Points.Add(new SeriesPoint("Menage Unipersonnel (1)", Utilities.getPourcentage(menage_1_Personne.Flag_27_47, nbreTotal)))));
                flag_27_47.Dispatcher.BeginInvoke((Action)(() =>
                            flag_27_47.Points.Add(new SeriesPoint("Ménage de deux (2) à trois (3) individus", Utilities.getPourcentage(menage_2_3_Personne.Flag_27_47, nbreTotal)))));
                flag_27_47.Dispatcher.BeginInvoke((Action)(() =>
                            flag_27_47.Points.Add(new SeriesPoint("Ménage de quatre (4) à cinq (5) individus", Utilities.getPourcentage(menage_4_5_Personne.Flag_27_47, nbreTotal)))));
                flag_27_47.Dispatcher.BeginInvoke((Action)(() =>
                            flag_27_47.Points.Add(new SeriesPoint("Ménage de six (6) individus ou plus", Utilities.getPourcentage(menage_6_Personne.Flag_27_47, nbreTotal)))));
                flag_27_47.Dispatcher.BeginInvoke((Action)(() =>
                            flag_27_47.Points.Add(new SeriesPoint("Tous les menages", Utilities.getPourcentage(all_menage.Flag_27_47, nbreTotal)))));

                flag_48_70.Dispatcher.BeginInvoke((Action)(() =>
                            flag_48_70.Points.Add(new SeriesPoint("Menage Unipersonnel (1)", Utilities.getPourcentage(menage_1_Personne.Flag_48_70, nbreTotal)))));
                flag_48_70.Dispatcher.BeginInvoke((Action)(() =>
                            flag_48_70.Points.Add(new SeriesPoint("Ménage de deux (2) à trois (3) individus", Utilities.getPourcentage(menage_2_3_Personne.Flag_48_70, nbreTotal)))));
                flag_48_70.Dispatcher.BeginInvoke((Action)(() =>
                            flag_48_70.Points.Add(new SeriesPoint("Ménage de quatre (4) à cinq (5) individus", Utilities.getPourcentage(menage_4_5_Personne.Flag_48_70, nbreTotal)))));
                flag_48_70.Dispatcher.BeginInvoke((Action)(() =>
                            flag_48_70.Points.Add(new SeriesPoint("Ménage de six (6) individus ou plus", Utilities.getPourcentage(menage_6_Personne.Flag_48_70, nbreTotal)))));
                flag_48_70.Dispatcher.BeginInvoke((Action)(() =>
                            flag_48_70.Points.Add(new SeriesPoint("Tous les menages", Utilities.getPourcentage(all_menage.Flag_48_70, nbreTotal)))));

                flag_71_130.Dispatcher.BeginInvoke((Action)(() =>
                            flag_48_70.Points.Add(new SeriesPoint("Menage Unipersonnel (1)", Utilities.getPourcentage(menage_1_Personne.Flag_48_70, nbreTotal)))));
                flag_48_70.Dispatcher.BeginInvoke((Action)(() =>
                            flag_48_70.Points.Add(new SeriesPoint("Ménage de deux (2) à trois (3) individus", Utilities.getPourcentage(menage_2_3_Personne.Flag_48_70, nbreTotal)))));
                flag_48_70.Dispatcher.BeginInvoke((Action)(() =>
                            flag_48_70.Points.Add(new SeriesPoint("Ménage de quatre (4) à cinq (5) individus", Utilities.getPourcentage(menage_4_5_Personne.Flag_48_70, nbreTotal)))));
                flag_48_70.Dispatcher.BeginInvoke((Action)(() =>
                            flag_48_70.Points.Add(new SeriesPoint("Ménage de six (6) individus ou plus", Utilities.getPourcentage(menage_6_Personne.Flag_48_70, nbreTotal)))));
                flag_48_70.Dispatcher.BeginInvoke((Action)(() =>
                            flag_48_70.Points.Add(new SeriesPoint("Tous les menages", Utilities.getPourcentage(all_menage.Flag_48_70, nbreTotal)))));

                flag_71_130.Dispatcher.BeginInvoke((Action)(() =>
                            flag_71_130.Points.Add(new SeriesPoint("Menage Unipersonnel (1)", Utilities.getPourcentage(menage_1_Personne.Flag_71_130, nbreTotal)))));
                flag_71_130.Dispatcher.BeginInvoke((Action)(() =>
                            flag_71_130.Points.Add(new SeriesPoint("Ménage de deux (2) à trois (3) individus", Utilities.getPourcentage(menage_2_3_Personne.Flag_71_130, nbreTotal)))));
                flag_71_130.Dispatcher.BeginInvoke((Action)(() =>
                            flag_71_130.Points.Add(new SeriesPoint("Ménage de quatre (4) à cinq (5) individus", Utilities.getPourcentage(menage_4_5_Personne.Flag_71_130, nbreTotal)))));
                flag_71_130.Dispatcher.BeginInvoke((Action)(() =>
                            flag_71_130.Points.Add(new SeriesPoint("Ménage de six (6) individus ou plus", Utilities.getPourcentage(menage_6_Personne.Flag_71_130, nbreTotal)))));
                flag_71_130.Dispatcher.BeginInvoke((Action)(() =>
                            flag_71_130.Points.Add(new SeriesPoint("Tous les menages", Utilities.getPourcentage(all_menage.Flag_71_130, nbreTotal)))));


            }
            catch (Exception ex)
            {

            }
            decorator.Dispatcher.BeginInvoke((Action)(() => decorator.IsSplashScreenShown = false));
        }
    
    }
}
